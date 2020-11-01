# ASP.net Core MVC (Filtering, Paging, Sorting)

## Ein generisches Repository erstellen

Wir erstellen nun eine Repository-Klasse die wir erweitern werden. Kümmern wir uns zuerst nur um den Teil mit dem wir nur lesend auf die DB zugreifen. Also nur das R (Read) von C.R.U.D.

### Read Only Repository

Der folgende Code ist bereits der Endausbau der Klasse `ReadOnlyRepositoryBase`.

```C#
public class ReadOnlyRepositoryBase<TEntity> : IReadOnlyRepositoryBase<TEntity>
    where TEntity : class, new()
{
    public TestsAdministratorContext Context { get; }

    public ReadOnlyRepositoryBase(TestsAdministratorContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> GetQueryable(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortOrder,
        out bool hasMore,
        string includeNavigationProperty = null,
        int? skip = null,
        int? take = null)
    {
        hasMore = false;

        IQueryable<TEntity> result = Context.Set<TEntity>();

        if (filter != null)
        {
            result = result.Where(filter);
        }
        if (sortOrder != null)
        {
            result = sortOrder(result);
        }

        includeNavigationProperty = includeNavigationProperty ?? String.Empty;
        foreach (var item in includeNavigationProperty.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
        {
            result = result.Include(item);
        }

        int count = result.Count();
        if (skip.HasValue)
        {
            result = result.Skip(skip.Value);
        }
        if (take.HasValue)
        {
            result = result.Take(take.Value);
        }

        if (result.Count() < count)
        {
            hasMore = true;
        }
        return result;
    }

    public IQueryable<TEntity> GetAll(
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeNavigationProperty = "", 
        int? skip = null, 
        int? take = null)
    {
        bool hasMore;
        return GetQueryable(
            null, 
            orderBy, 
            out hasMore, 
            includeNavigationProperty, 
            skip, 
            take
        );
    }

    public IQueryable<TEntity> Get(
        Expression<Func<TEntity, bool>> filter = null, 
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
        string includeNavigationProperty = "", 
        int? skip = null, 
        int? take = null)
    {
        bool hasMore;

        return GetQueryable(
            filter, 
            orderBy, 
            out hasMore, 
            includeNavigationProperty, 
            skip, 
            take
        );
    }

    public TEntity GetSingle(
        Expression<Func<TEntity, bool>> filter = null,
        string includeNavigationProperty = "")
    {
        bool hasMore;
        return GetQueryable(
            filter,
            null,
            out hasMore,
            includeNavigationProperty: includeNavigationProperty
        ).SingleOrDefault();
    }

    public async Task<TEntity> GetSingleAsync(
    Expression<Func<TEntity, bool>> filter = null, 
    string includeNavigationProperty = "")
    {
        bool hasMore;
        return await GetQueryable(
            filter, 
            null, 
            out hasMore, 
            includeNavigationProperty: includeNavigationProperty
        ).SingleOrDefaultAsync();
    }
}
```

Betrachten wir den Code im detail. Wir möchten die Klasse streng typisieren, daher benötigen wir in der Klassensignatur eine `where`-Klausel.

```C#
public class ReadOnlyRepositoryBase<TEntity> : IReadOnlyRepositoryBase<TEntity>
    where TEntity : class, new()
{
    ...
}
```

Wie gewohnt injecten wir den DB-Context, diesmal allerdings in einem Public Property, damit wir auch von außwen auf den Context zugreifen können.

```C#
public TestsAdministratorContext Context { get; }

public ReadOnlyRepositoryBase(TestsAdministratorContext context)
{
    Context = context;
}
```

Die Methode `GetQueryable(...)` akzeptiert eine ganze Menge Parameter. Die meisten davon sind optional. Jenachdem welche Parameter gesetzt werden, wird in dieser Methode mittels LinQ ein Query aufgebaut, der dann zur DB gesendet wird. Das Interface ``IQueryable`` hilft uns dabei, dynamisch einen belibigen LinQ-Query zusammenzusetzen, der anschließend zu einem SQL-Satement kompiliert wird.

Die restlichen Methoden darunter erleichtern dem Aufrufer (Services) nur den Umgang mit der "fast alles könnenden" Methode `GetQueryable(...)`.

Selbstverständlich wird diese Klasse zu den aufrufern über ein Interface abgegrenzt.

```C#
public interface IReadOnlyRepositoryBase<TEntity>
    where TEntity : class, new()
{
    TestsAdministratorContext Context { get; }

    IQueryable<TEntity> GetQueryable(
        Expression<Func<TEntity, bool>> filter,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> sortOrder,
        out bool hasMore,
        string includeNavigationProperty = null,
        int? skip = null,
        int? take = null);

    IQueryable<TEntity> GetAll(
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeNavigationProperty = "",
                int? skip = null,
                int? take = null);

    IQueryable<TEntity> Get(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeNavigationProperty = "",
                int? skip = null,
                int? take = null);

    TEntity GetSingle(
                Expression<Func<TEntity, bool>> filter = null,
                string includeNavigationProperty = "");

    Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeNavigationProperty = "");
}
```

### Erweiterung zum Write-Repoasitory

Die restlichen C.R.U.D. Operationen implementieren wir in einer anderen Klasse. Später können wir uns aussuchen welches Repository wir instanzieren wollen. Nur Read-Funktionalität oder Read und Write-Funktion.

Wieder zuerst das Interface definieren:

```C#
public interface IRepositoryBase<TEntity, TId> : IReadOnlyRepositoryBase<TEntity>
    where TEntity : class, new()
{
    Task<TEntity> CreateAsync(TEntity newModel);

    Task<TEntity> EditAsync(TEntity newModel, 
        Func<TEntity, bool> existsPredicate, 
        Func<TEntity, TEntity> setEntityData);

    Task DeleteAsync(TId id);
}
```

Dieses Repository soll auch Read-Funktionen anbieten können also lassen wir es `ReadOnlyRepositoryBase` ableiten.

```C#
public class RepositoryBase<TEntity, TId> : ReadOnlyRepositoryBase<TEntity>, IRepositoryBase<TEntity, TId>
    where TEntity : class, new()
{
    public RepositoryBase(TestsAdministratorContext context)
        : base(context)
    { }

    public async Task<TEntity> CreateAsync(TEntity newModel)
    {
        try
        {
            Context.Add(newModel);
            await Context.SaveChangesAsync();
        }
        catch (InvalidOperationException ex)
        {
            throw ex;
        }
        return newModel;
    }

    public async Task<TEntity> EditAsync(TEntity newModel, 
        Func<TEntity, bool> entityExistsPredicate, 
        Func<TEntity, TEntity> setEntityData)
    {
        TEntity model = setEntityData(newModel);

        try
        {
            Context.Update(newModel);
            await Context.SaveChangesAsync();
            return newModel;
        }
        catch (InvalidOperationException ex)
        {
            if (entityExistsPredicate(newModel))
            {
                throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
            }
            throw ex;
        }
    }

    public async Task DeleteAsync(TId id)
    {
        object lesson = await Context.Lesson.FindAsync(id);
        if (lesson != null)
        {
            try
            {
                Context.Remove(lesson);
                await Context.SaveChangesAsync();
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
        }
        else
        {
            throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
        }
    }
}
```

Kleiner Tipp!
Hier können die Inhalte der Methoden aus dem Service kopiert und angepasst werden. Die Tipparbeit hält sich also in durchaus Grenzen.

## Änderungen am Service

Natürlich ändern sich jetzt unsere Service-Methoden drastisch.

Zuerst muss das Repository injected werden:

```C#
private readonly IRepositoryBase<Lesson, long> _repository;

public LessonService(IRepositoryBase<Lesson, long> repository)
{
    _repository = repository;
}
```

Änderungen an der unrsprünglichen Methode im Service:

* Die Expression für die Filterung erstellen
* Ein Delegate für die Sortierung erstellen
* Die neue Methode im `ReadOnlyRepositoryBase` muss mit den Parametern aufgerufen werden

#### Die Expression für die Filterung:

```C#
public async Task<PagenatedList<Lesson>> GetAllAsync(int pageIndex, string filter)
{
    Expression<Func<Lesson, bool>> filterExpression = null;
    if (!string.IsNullOrEmpty(filter))
    {
        filterExpression = l => l.L_Teacher.Contains(filter);
    }

    IQueryable<Lesson> result = _repository.Get(
        filter: filterExpression,
        includeNavigationProperty: "L_ClassNavigation;L_TeacherNavigation"
    );
    PagenatedList<Lesson> page = await PagenatedList<Lesson>.CreateAsync(result, pageIndex, 10);
    return page;
}
```

bzw.:

```C#
public async Task<PagenatedList<Lesson>> GetAllBySchoolclassAsync(string schoolclassId, int pageIndex, string filter)
{
    Expression<Func<Lesson, bool>> filterExpression = null;
    if (!string.IsNullOrEmpty(filter))
    {
        filterExpression = l => l.L_ClassNavigation.C_ID == schoolclassId
            && l.L_Teacher.Contains(filter);
    }
    else
    {
        filterExpression = l => l.L_ClassNavigation.C_ID == schoolclassId;
    }

    IQueryable<Lesson> result = _repository.Get(
        filter: filterExpression,
        includeNavigationProperty: "L_ClassNavigation;L_TeacherNavigation"
    );
    PagenatedList<Lesson> page = await PagenatedList<Lesson>.CreateAsync(result, pageIndex, 10);
    return page;
}
```

```C#
public async Task<Lesson> GetSingleOrDefaultAsync(long id)
{
    return await _repository.GetSingleAsync(
        filter: l => l.L_ID == id,
        includeNavigationProperty: "L_ClassNavigation;L_TeacherNavigation");
}
```

#### Create

```C#
public async Task<Lesson> CreateAsync(Lesson newModel)
{
    try
    {
        return await _repository.CreateAsync(newModel);
    }
    catch (KeyNotFoundException ex)
    {
        throw new ServiceLayerException("Methode CreateAsync(Lesson) ist fehlgeschlagen!", ex);
    }
    catch (InvalidOperationException ex)
    {
        throw new ServiceLayerException("Methode CreateAsync(Lesson) ist fehlgeschlagen!", ex);
    }
}
```

#### Update
Hier ist vdas besondere, dass die Daten aus dem model der View nicht direkt in die DB geschrieben werden. Das wäre durchaus gefährlich, da dadurch die Daten manipuliert werden könnten. Z.B. ein Time Stamp oder eine ID könnte überschrieben werden. Daher nehmen wir aus dem Model nur jene Daten, die SICHER (Save, Secure) aktualisiert werden dürfen und in der DB nichts anstellen.

Gelöst wird das z.B. mit einem Delegate.

```C#
public async Task<Lesson> EditAsync(long id, Lesson newModel)
{
    try
    {
        return await _repository.EditAsync(newModel, l => !EntityExists(newModel.L_ID), l =>
        {
            return new Lesson()
            {
                L_ID = newModel.L_ID,
                L_Class = newModel.L_Class,
                L_Day = newModel.L_Day,
                L_Hour = newModel.L_Hour,
                L_Room = newModel.L_Room,
                L_Subject = newModel.L_Subject,
                L_Teacher = newModel.L_Teacher,
                L_Untis_ID = newModel.L_Untis_ID
            };
        });
    }
    catch (KeyNotFoundException ex)
    {
        throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
    }
    catch (DbUpdateConcurrencyException ex)
    {
        throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
    }
    catch (InvalidOperationException ex)
    {
        throw new ServiceLayerException("Methode EditAsync(long, Lesson) ist fehlgeschlagen!", ex);
    }
}
```

#### Delete

```C#
public async Task DeleteAsync(long id)
{
    try
    {
        await _repository.DeleteAsync(id);
    }
    catch (KeyNotFoundException ex)
    {
        throw new ServiceLayerException("Methode DeleteAsync(long) ist fehlgeschlagen!", ex);
    }
    catch (InvalidOperationException ex)
    {
        throw new ServiceLayerException("Methode DeleteAsync(long) ist fehlgeschlagen!", ex);
    }
}
```

### Zuletzt nicht vergessen

**Achtung!** Datentyp von `result` von `IEnumerable<T>` auf `IQueryable<T>` ändern!

## Änderungen in der View

Ein HTML-Formular für die Filterung serstellen:

```HTML
<form>
    <div class="form-row align-items-center">
        <div class="col-sm-3 my-1">
            <input type="text" class="form-control" name="filter" value="@ViewData["CurrentFilter"]" placeholder="Suche...">
        </div>
        <div class="col-auto my-1">
            <button type="submit" class="btn btn-primary">GO</button>
        </div>
    </div>
</form>
```

## Pagination

Hierfür erstellen wir eine Klasse, die uns bei der Paginationhifreich sein wird:

```C#
public class PagenatedList<T> : List<T>
{
    public int PageIndex { get; private set; }

    public int TotalPages { get; private set; }

    public PagenatedList(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        this.AddRange(items);
    }

    public bool HasPreviousPage
    {
        get { return (PageIndex > 1); }
    }

    public bool HasNextPage
    {
        get { return (PageIndex < TotalPages); }
    }

    public static async Task<PagenatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();

        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagenatedList<T>(items, count, pageIndex, pageSize);
    }
}
```
Der `LessonService` gibt nun nicht mehr ein `IEnumerable` zurück, sondern die erstellte Liste:

```C#
public async Task<PagenatedList<Lesson>> GetAllAsync(int pageIndex, string filter)
{
    ...
}
```

Wir instanzieren die PaginatedList mittels der Factory, mit den vorhandenen Parametern. Diese tut nun den Rest für uns:

```C#
public async Task<PagenatedList<Lesson>> GetAllAsync(int pageIndex, string filter)
{
    Expression<Func<Lesson, bool>> filterExpression = null;
    if (!string.IsNullOrEmpty(filter))
    {
        filterExpression = l => l.L_Teacher.Contains(filter);
    }

    IQueryable<Lesson> result = _repository.Get(
        filter: filterExpression,
        includeNavigationProperty: "L_ClassNavigation;L_TeacherNavigation"
    );

    PagenatedList<Lesson> page = await PagenatedList<Lesson>.CreateAsync(result, pageIndex, 10);
    return page;
}
```

```C#
[HttpGet()]
public async Task<IActionResult> Index(int pageIndex, string filter, string currentFilter)
{
    ViewData["CurrentFilter"] = currentFilter;
    if (filter != null)
    {
        ViewData["CurrentFilter"] = filter;
        pageIndex = 1;
    }
    else
    {
        filter = currentFilter;
    }

    pageIndex = pageIndex == 0 ? 1: pageIndex;

    var testsAdministratorContext = await _lessonService.GetAllAsync(pageIndex, filter);
    return View(testsAdministratorContext);
}
```

### Die View

Die Buttons für das Paging:

```HTML
@if (schoolclassId == null)
{
    <a asp-action="Index"
       asp-route-pageIndex="@(Model.PageIndex - 1)"
       asp-route-currentFilter="@ViewData["CurrentFilter"]"
       class="btn btn-default @prevDisabled">
        Previous
    </a>
    <a asp-action="Index"
       asp-route-pageIndex="@(Model.PageIndex + 1)"
       asp-route-currentFilter="@ViewData["CurrentFilter"]"
       class="btn btn-default @nextDisabled">
        Next
    </a>
    <label>(@Model.TotalPages Seiten)</label>
}
else
{
    <a asp-action="IndexByLessonId"
       asp-route-pageIndex="@(Model.PageIndex - 1)"
       asp-route-schoolclassId="@schoolclassId"
       asp-route-currentFilter="@ViewData["CurrentFilter"]"
       class="btn btn-default @prevDisabled">
        Previous
    </a>
    <a asp-action="IndexByLessonId"
       asp-route-pageIndex="@(Model.PageIndex + 1)"
       asp-route-schoolclassId="@schoolclassId"
       asp-route-currentFilter="@ViewData["CurrentFilter"]"
       class="btn btn-default @nextDisabled">
        Next
    </a>
    <label>(@Model.TotalPages Seiten)</label>
}
```

Natürlich müssen die in der View verwendeten Variablen auch erstmal aus dem Model ausgelesen werden

```C#
@model Spg.MvcTestsAdmin.Service.Services.PagenatedList<Spg.MvcTestsAdmin.Service.Models.Lesson>

@{
    var schoolclassId = ViewData["schoolclassId"];
    var prevDisabled = !Model.HasPreviousPage ? "disabled" : "";
    var nextDisabled = !Model.HasNextPage ? "disabled" : "";
}
```
