# ASP.net Core MVC

Das Prinzip von Inversion of Control ist ja bereits bekannt. Wir wollen es nun hier weiter vertiefen.

Das wesentlichste dabei ist, eine Entkoppelung  der Komponenten zu bewirken. Je feiner granular Komponenten gestaltet werden und besser sie entkoppelt sind umso flexibler wird unser Softwaregerüst.

## Services

* Wir legen ein neues Projekt an (.Net Core Class Library)
* Eine Namespace `Services` erstellen

### Einen Event-Service anlegen

Dazu im generiten namespace eine neue Klasse `SchoolclassService` erstellen. Code kann nun aus dem Controller in diese Klasse transferiert werden. Wir wollen alle Abhänmgigkeiten zum DB-Context aus dem Controller entfernen.

Im einfachsten Fall kann man den LinQ-Query aus dem Controller in eine neue Methode `GetAllAsync()` ohne Parameter kopieren.

Später werden wir die Methode nach und nach um weitere Features erweitern, die wir ebenfalls implementieren wollen.

## DB-Context injecten

Der Service benötigt den DB-Context:

```C#
private readonly TestsAdministratorContext _context;

public SchoolclassService(TestsAdministratorContext context)
{
    _context = context;
}
```

```C#
public async Task<IEnumerable<Schoolclass>> GetAllAsync()
{
    return await _context.Schoolclass
        .Include(s => s.C_ClassTeacherNavigation)
        .ToListAsync();
}
```

### Create-Methode

```C#
public async Task<Schoolclass> CreateAsync(Schoolclass newModel)
{
    try
    {
        _context.Add(newModel);
        await _context.SaveChangesAsync();
    }
    catch (InvalidOperationException ex)
    {
        _logger.LogError(ex, "Methode CreateAsync(Schoolclass) ist fehlgeschlagen!");
        throw new ServiceLayerException("Methode CreateAsync(Schoolclass) ist fehlgeschlagen!", ex);
    }
    return newModel;
}
```

### Delete.-Methode

```C#
public async Task DeleteAsync(string id)
{
    try
    {
        var schoolclass = await _context.Schoolclass.FindAsync(id);
        _context.Schoolclass.Remove(schoolclass);
        await _context.SaveChangesAsync();
    }
    catch (InvalidOperationException ex)
    {
        _logger.LogError(ex, "Methode DeleteAsync(long) ist fehlgeschlagen!");
        throw new ServiceLayerException("Methode DeleteAsync(long) ist fehlgeschlagen!", ex);
    }
}
```

### Edit-Methode

```C#
public async Task<Schoolclass> EditAsync(string id, Schoolclass newModel)
{
    if (id != newModel.C_ID)
    {
        throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
    }

    try
    {
        Schoolclass existingSchoolclass = _context.Schoolclass.Find(id);
        if (existingSchoolclass == null)
        {
            throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
        }
        else
        {
            existingSchoolclass.C_ClassTeacher = newModel.C_ClassTeacher;
            existingSchoolclass.C_Department = newModel.C_Department;

            _context.Update(existingSchoolclass);
            await _context.SaveChangesAsync();
        }
    }
    catch (InvalidOperationException ex)
    {
        _logger.LogError(ex, "Methode EditAsync(long, Schoolclass) ist fehlgeschlagen!");
        throw new ServiceLayerException("Methode EditAsync(long, Schoolclass) ist fehlgeschlagen!", ex);
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!EntityExists(newModel.C_ID))
        {
            throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
        }
        throw;
    }
    return newModel;
}

private bool EntityExists(string id)
{
    return _context.Schoolclass.Any(e => e.C_ID == id);
}
```

Im Service wird nicht zwischen GET und POST unterschieden. Diese Methoden manipulieren nur die Datenbank. Wir arbeiten generisch. D.h. diese Methoden sollen für Applikationen, die aber ein anders Front End haben ebenfalls verwendbar sein. Das könnte sogar eine WPF oder Konslen-Applikation sein. Hier gäbe es keine HTTP-Requests.

## Controller

Im Controller werden die Methoden dann noch aufgerufen. Auch hier wird eine Instanz vom Service benötigt:

```C#
private readonly TestsAdministratorContext _context;
private readonly ILogger<SchoolclassService> _logger;

public SchoolclassService(TestsAdministratorContext context, ILogger<SchoolclassService> logger)
{
    _context = context;
    _logger = logger;
}
```

### Details

Hierfür habe ich eine Methode `GetSingleOrDefaultAsync` erstellt. Die Namensgebung entspricht der üblichen Konvetion.

```C#
public async Task<IActionResult> Details(string id)
{
    if (id == null)
    {
        return NotFound();
    }

    var model = await _schoolclassService.GetSingleOrDefaultAsync(id);

    if (model == null)
    {
        return NotFound();
    }

    return View(model);
}
```

### Create-POST

```C#
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("C_ID,C_Department,C_ClassTeacher")] Schoolclass schoolclass)
{
    if (ModelState.IsValid)
    {
        try
        {
            await _schoolclassService.CreateAsync(schoolclass);
            return RedirectToAction(nameof(Index));
        }
        catch (ServiceLayerException)
        {
            return StatusCode(500);
        }
    }
    ViewData["C_ClassTeacher"] = new SelectList(await _techerService.GetAllAsync(), "T_ID", "T_ID", schoolclass.C_ClassTeacher);
    return View(schoolclass);
}
```

### Edit-GET

```C#
public async Task<IActionResult> Edit(string id)
{
    if (id == null)
    {
        return NotFound();
    }

    var schoolclass = await _schoolclassService.GetSingleOrDefaultAsync(id);

    if (schoolclass == null)
    {
        return NotFound();
    }
    ViewData["C_ClassTeacher"] = new SelectList(await _techerService.GetAllAsync(), "T_ID", "T_ID", schoolclass.C_ClassTeacher);
    return View(schoolclass);
}
```

### Edit-POST

```C#
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(string id, [Bind("C_ID,C_Department,C_ClassTeacher")] Schoolclass schoolclass)
{
    if (id != schoolclass.C_ID)
    {
        return Conflict();
    }

    if (ModelState.IsValid)
    {
        try
        {
            await _schoolclassService.EditAsync(id, schoolclass);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (ServiceLayerException)
        {
            return StatusCode(500);
        }
        return RedirectToAction(nameof(Index));
    }
    ViewData["C_ClassTeacher"] = new SelectList(await _techerService.GetAllAsync(), "T_ID", "T_ID", schoolclass.C_ClassTeacher);
    return View(schoolclass);
}
```

### Delete GET

```C#
public async Task<IActionResult> Delete(Guid? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var events = await _eventService.GetSingleOrDefaultAsync(id.Value);

    if (events == null)
    {
        return NotFound();
    }

    return View(events);
}
```

### Delete POST

```C#
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteConfirmed(string id)
{
    try
    {
        await _schoolclassService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
    catch (ServiceLayerException)
    {
        return StatusCode(500);
    }
}
```

## Interfaces

Hier wurden bereits Interfaces als Typen für die jeweiligen Instanzen verwednet. Es sollte immer eine Trennung durch die Erstellung von Interfaces vorgenommen werden.

Registrieren der Services in der `Startup.cs`:

```C#
services.AddTransient<ISchoolclassService, SchoolclassService>();
services.AddTransient<ILessonService, LessonService>();
services.AddTransient<IPeriodService, PeriodService>();
services.AddTransient<ITeacherService, TeacherService>();
```
