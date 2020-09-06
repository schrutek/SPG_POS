# ASP.net Core MVC (Rounting)

Das Routing wird in der `Startup.cs` festgelegt und konfiguriert.

Folgende Routing-Varianten stehen zur Verfügung:

## Endpoint Routing

Seit Core 3.0 wird Endpoint-Routing unterstützt. Es ist die klassische und daher kompatibleste Variante des Routings.

```C#
/app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync("Hello World!");
    });
    endpoints.MapGet("/about", async context =>
    {
        await context.Response.WriteAsync("<h1>Hello World - About Page!</h1>");
    });
});

```

## Konventionelles Rounting

```C#
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
```

Dabei handelt es sich um das Konventionelle MVC-Routing. Das Pattern gibt die Routen an. `Controllername/Methode/ID` Die ID ist dabei Optional. Weitere Parameter (z.B.: State, Filter, ...) können alsURL-Parameter an die URL angefügt werden.

```HTTP
https://localhost:5001/Events/Index?filter=danc&state=465604e4-21f3-4092-b09a-54744860b78e
```

## Attribute Routing

Die beste Form ist das attribute-Routing, da es dem Konzept von REST sehr nahe kommt. Wir werden also gleich mit dieser variante loslegen:

In der `Startup.cs`:

```C#
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
```

Das aktiviert das Routing welches in den neinzelnen Controllern angegeben ist. Die Controller weden dann it den Route-Annotations bzw. den HTTP-Verben versehen:

* Route-DataAnnotation (`[Route()]`)
* Http-Verb (`[HttpGet()]`, `[HttpPost()]`)

```C#
[Route("[controller]/[action]")]
public class EventsController : Controller
{
    private readonly IEventService _eventService;

    public EventsController(IEventService eventService)
    {
        _eventService = eventService;
    }
}
```

bzw. das Verb:

```C#
// GET: Events
[HttpGet()]
public async Task<IActionResult> Index()
{
    PaginatedList<Events> model = (PaginatedList<Events>)await _eventService.GetAllAsync();
    return View(model);
}
```

oder Post:

```C#
// POST: Events/Create
// To protect from overposting attacks, enable the specific properties you want to bind to, for
// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("EventId,LastChangeDate,LaseChangeUserIdId,Name,Description,OnlineFrom,OnlineTo")] Events events)
{
    if (ModelState.IsValid)
    {
        await _eventService.CreateAsync(events);
        return RedirectToAction(nameof(Index));
    }
    return View(events);
}
```

## View Injection

Eine weitere interessante Sache ist View Injection. Ähnlich wei beim Kapitel Dependency-Injection kann man einen Service direkt in eine View injecten.

Das Schlüsselwort hoerfür ist die Direktive `@inject`:

```HTML
@model Spg.MvcTicketShop.Services.Helper.PaginatedList<Spg.MvcTicketShop.Services.Models.Events>
@inject Spg.MvcTicketShop.Services.Services.LookupService LookupService

@{
    ViewData["Title"] = "Index";
    Spg.MvcTicketShop.Services.Models.Events firstEvent = Model.ToList().FirstOrDefault();
    var validCatEventStates = LookupService.GetAllValidCatEventStates();
}

<h1>Index</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>
```

Der Lookup-Service retuniert eine simple Liste bestehend aus Objekten. Diese sollen dnnn in einer Combobox dargestellt werden:

```C#
public class LookupService
{
    private readonly TicketShopContext _context;

    public LookupService(TicketShopContext context)
    {
        _context = context;
    }

    public List<CatEventStates> GetAllValidCatEventStates()
    {
        return _context.CatEventStates.ToList();
    }
}
```

Der Service kann nun direkt verwemdet werden:

```HTML
<form>
    <div class="form-row align-items-center">
        <div class="col-sm-3 my-1">
            <input type="text" class="form-control" name="filter" value="@ViewData["currentFilter"]" placeholder="Suche...">
        </div>
        <div class="col-sm-3 my-1">
            <select class="form-control" name="state">
                <option value="-1">Status wählen...</option>
                @foreach (var item in validCatEventStates)
                {
                    <option value="@item.CatEventStateId" selected="@(((ViewData["CurrentState"] != null)
                        && (Guid)ViewData["CurrentState"] == item.CatEventStateId))">
                        @item.ShortName
                    </option>
                }
            </select>
        </div>
        <div class="col-auto my-1">
            <button type="submit" class="btn btn-primary">GO</button>
        </div>
    </div>
</form>
```
