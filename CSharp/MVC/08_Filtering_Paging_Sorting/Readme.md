# ASP.net Core MVC (Filtering, Paging, Sorting)

## Änderungen am Service

Ausgangssituation ohne Paging, mit Filtering, Sorting, Skip, Take

**Achtung!** Datentyp von `result` von `IEnumerable<T>` auf `IQueryable<T>` ändern!

```C#
IQueryable<Events> result = _context.Events;

if (!String.IsNullOrEmpty(filter))
{
    result = result.Where(c => c.Name.Contains(filter) && c.Description.Contains(filter));
}

if (state.HasValue)
{
    result = result.Where(c => c.CatEventStateId == state.Value);
}

if (!String.IsNullOrEmpty(sortOrder))
{
    switch (sortOrder)
    {
        case "onlinefrom":
            result = result.OrderBy(c => c.OnlineFrom);
            break;
        case "onlinefrom_desc":
            result = result.OrderByDescending(c => c.OnlineFrom);
            break;
        case "onlineto":
            result = result.OrderBy(s => s.OnlineTo);
            break;
        case "onlineto_desc":
            result = result.OrderByDescending(s => s.OnlineTo);
            break;
        case "description":
            result = result.OrderBy(s => s.Description);
            break;
        case "description_desc":
            result = result.OrderByDescending(s => s.Description);
            break;
        case "name_desc":
            result = result.OrderByDescending(s => s.Name);
            break;
        default:
            result = result.OrderBy(s => s.Name);
            break;
    }
}

if (skip.HasValue)
{
    result = result.Skip(skip.Value);
}

if (take.HasValue)
{
    result = result.Take(take.Value);
}

return result;
```

Danach das generisch machen und in eine eigene Klasse bringen `EFRepository`.

Methode im `EFRepository`:

```C#
public IQueryable<Events> GetQueriable(
    Expression<Func<Events, bool>> filter,
    string currentFilter,
    Func<IQueryable<Events>, IOrderedQueryable<Events>> sortOrder,
    string includeNavigationProperty = null,
    int? skip = null,
    int? take = null)
{
    IQueryable<Events> result = _context.Events;

    if (filter != null)
    {
        result = result.Where(filter);
    }

    if (sortOrder != null)
    {
        result = sortOrder(result);
    }

    if (!string.IsNullOrEmpty(includeNavigationProperty))
    {
        result = result.Include(includeNavigationProperty);
    }

    if (skip.HasValue)
    {
        result = result.Skip(skip.Value);
    }

    if (take.HasValue)
    {
        result = result.Take(take.Value);
    }

    return result;
}

```

Generische Methode im `EFRepository`:

```C#
public IQueryable<TEntity> GetQueryable(
    Expression<Func<TEntity, bool>> filter,
    string currentFilter,
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

    if (!string.IsNullOrEmpty(includeNavigationProperty))
    {
        result = result.Include(includeNavigationProperty);
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
```

dazu muss in der Klassensignatur eine Bedingung hinzugefügt werden:

```C#
public class EFRepository<TEntity>
    where TEntity : class, new()
{
    ...
}
```

Änderungen an der unrsprünglichen Methode im Service:

* Die Expression für die Filterung muss erstellt werden
* Das Delegate für die Sortierung muss erstellt werden
* Die neue Methode im `EFRepository` muss mit den Pareametern aufgerufen werden
* Die Where-Klausel für die State-Filterung bleibt erhalten

Die Expression für die Filterung:

```C#
Expression<Func<Events, bool>> filterQuery = null;
if (!String.IsNullOrEmpty(filter))
{
    filterQuery = c => c.Name.Contains(filter) || c.Description.Contains(filter);
}
```

Das Delegate für die Sortierung

```C#
Func<IQueryable<Events>, IOrderedQueryable<Events>> orderBy = null;
if (!String.IsNullOrEmpty(sortOrder))
{
    switch (sortOrder)
    {
        case "onlinefrom":
            orderBy = c => c.OrderBy(s => s.OnlineFrom);
            break;
        case "onlinefrom_desc":
            orderBy = c => c.OrderByDescending(s => s.OnlineFrom);
            break;
        case "onlineto":
            orderBy = c => c.OrderBy(s => s.OnlineTo);
            break;
        case "onlineto_desc":
            orderBy = c => c.OrderByDescending(s => s.OnlineTo);
            break;
        case "description":
            orderBy = c => c.OrderBy(s => s.Description);
            break;
        case "description_desc":
            orderBy = c => c.OrderByDescending(s => s.Description);
            break;
        case "name_desc":
            orderBy = c => c.OrderByDescending(s => s.Name);
            break;
        default:
            orderBy = c => c.OrderBy(s => s.Name);
            break;
    }
}
```

Die neue Methode im `EFRepository` muss mit den Pareametern aufgerufen werden:

```C#
IQueryable<Events> result = _repository.GetQueryable(filterQuery, currentFilter, orderBy, out hasMore, 0, 100);
```

Die Where-Klausel für die State-Filterung

```C#
if (state.HasValue)
{
    result = result.Where(c => c.CatEventStateId == state);
}
```

und zuletrzt das Ergebnis retunieren.

## Änderungen im Controller

Die neue Service-Methdoe wird mit den notwendigen Parametern aufgerufen

```C#
PaginatedList<Events> model = (PaginatedList<Events>)await _eventService.GetAllAsync(filter, currentFilter, state, sortOrder);
```

Die parameter müssen natürlich irgendwoher kommen. Das übernimmt die View.

## Filtering (Änderungen in der View)

x

## Sorting  (Änderungen in der View)

x

## Paging

x
