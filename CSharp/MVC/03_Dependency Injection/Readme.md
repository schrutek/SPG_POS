# ASP.net Core MVC

Das Prinzip von Inversion of Control ist ja bereits bekannt. Wir wollen es nun hier weiter vertiefen.

Das wesentlichste dabei ist, eine Entkoppelung  der Komponenten zu bewirken. Je feiner granular Komponenten gestaltet werden und besser sie entkoppelt sind umso flexibler wird unser Softwaregerüst.

## Services

* Wir legen ein neues Projekt an (.Net Core Class Library)
* Eine Namespace `Services` erstellen

### Einen Event-Service anlegen

Dazu im generiten namespace eine neue Klasse `EventService` erstellen. Code kann nun aus dem Controller in diese Klasse transferiert werden. Wir wollen alle Abhänmgigkeiten zum DB-Context aus dem Controller entfernen.

Im einfachsten Fall kann man den LinQ-Qiery aus dem Controller in eine neue Methode `GetAllAsync()` ohne Parameter kopieren.

Später werden wir die Methode nach und nach um weitere Features erweitern, die wir ebenfalls implementieren wollen.

## DB-Context injecten

Der Service benötigt den DB-Context:

```C#
private readonly TicketShopContext _context;

public EventService(TicketShopContext context)
{
    _context = context;
}
```

```C#
public async Task<IEnumerable<Events>> GetAllAsync()
{
    var result = await _context.Events
        .Include(c => c.Shows)
        .ToListAsync();
    return result;
}
```

### Create-Methode

```C#
public async Task<Events> CreateAsync(Events newModel)
{
    newModel.EventId = Guid.NewGuid();
    _context.Add(newModel);
    await _context.SaveChangesAsync();

    return newModel;
}
```

### Delete.-Methode

```C#
public async Task<Events> DeleteAsync(Guid? id)
{
    var result = await _context.Events.FindAsync(id);
    _context.Events.Remove(result);
    await _context.SaveChangesAsync();

    return result;
}
```

### Edit-Methode

```C#
public async Task<Events> EditAsync(Guid id, Events newModel)
{
    try
    {
        newModel.LastChangeDate = DateTime.Now;

        _context.Update(newModel);
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateConcurrencyException)
    {
        if (!EventsExists(newModel.EventId))
        {
            throw new KeyNotFoundException("Datensatz wurde nicht gefunden!");
        }
        else
        {
            throw;
        }
    }

    return newModel;
}

private bool EventsExists(Guid id)
{
    return _context.Events.Any(e => e.EventId == id);
}
```

Im Service wird nicht zwischen GET und POST unterschieden. Diese Methoden manipulieren nur die Datenbank. Wir arbeiten generisch. D.h. diese Methoden sollen  für Applikationen, die aber ein anders Front End haben ebenfalls verwendbar sein. Das könnte sogar eine WPF oder Konslen-Applikation sein. Hier gäbe es keine HTTP-Requests.

## Controller

Im Controller werden die Metghoden dann noch aufgerufen. Auch hier wird eine Instanz vom Service benötigt:

```C#
private readonly IEventService _eventService;

public EventsController(IEventService eventService)
{
    _eventService = eventService;
}
```

```C#
PaginatedList<Events> model = (PaginatedList<Events>)await _eventService.GetAllAsync();
return View(model);
```

### Details

Hierfür habe ich eine Methode `GetSingleOrDefaultAsync` erstellt. Die Anmensgebung ebntspricht der üblichen Konvetion.

```C#
var events = await _eventService.GetSingleOrDefaultAsync(id.Value);
```

### Create-POST

```C#
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

### Edit-GET

```C#
[HttpGet()]
public async Task<IActionResult> Edit(Guid? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var result = await _eventService.GetSingleOrDefaultAsync(id.Value);

    if (result == null)
    {
        return NotFound();
    }
    return View(result);
}
```

### Edit-POST

```C#
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(Guid id, [Bind("EventId,LastChangeDate,LaseChangeUserIdId,Name,Description,OnlineFrom,OnlineTo")] Events events)
{
    if (id != events.EventId)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            await _eventService.EditAsync(id, events);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }
    return View(events);
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
public async Task<IActionResult> DeleteConfirmed(Guid id)
{
    await _eventService.DeleteAsync(id);

    return RedirectToAction(nameof(Index));
}
```

## Interfaces

Hier wurden bereits Interfaces aös Typen für die jeweiligen Instanzen verwednet. Es 
sollte immer eine Trennung durch die Erstellung von Interfaces vorgenommen werden.

Registrieren der Services in der `Startup.cs`:

```C#
services.AddScoped<IEventService, EventService>();
```
