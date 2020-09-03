# ASP.net Core MVC (State Management)

Das Web st Stateless, wir müssen uns daher um den State seöbtst kümmern. Der State stellt Informationen dar, die im Verlauf einer Webseite nicht in "Vergessenheit geraten" sollten um die Usability zu verbessern.

Beispiel:

Es wäre sehr unpraktosch wenn man sich in einem Web-Shop jedes mal neu anmelden müsste, wenn man einen neuen Artikel ansurft. Lösung: Das System merkt sich die Anmeldung.

## Cookies

Cookies sind keine Dateien die likal gespeichert werden. Sie können Daten enthaltn. Max. Größe der Dateien: 4096 bytes.

Folgednes Beispiel legt im POST-REquest einen Benutezrnamen in einem Cookie ab und ließt ihn im GET-REquest wieder aus.

```c#
public class HomeController : Controller
{
    public IActionResult Index()
    {
        //read cookie from Request object  
        string userName = Request.Cookies["UserName"];
        return View("Index", userName);
    }

    [HttpPost]
    public IActionResult Index(IFormCollection form)
    {
        string userName = form["userName"].ToString();

        //set the key value in Cookie
        CookieOptions option = new CookieOptions();
        option.Expires = DateTime.Now.AddMinutes(10);
        Response.Cookies.Append("UserName", userName, option);

        return RedirectToAction(nameof(Index));
    }

    public IActionResult RemoveCookie()
    {
        //Delete the cookie
        Response.Cookies.Delete("UserName");
        return View("Index");
    }
}
```

Die dazgehörige View:

```c#
@model string

@{
   ViewData["Title"] = "Home Page";
}

@if (!string.IsNullOrWhiteSpace(Model))
{
   @:
   <div>Welcome back, @Model</div>
   @Html.ActionLink("Forget Me", "RemoveCookie")
}
else
{
   @:
   <form asp-action="Index">
       <span>Hey, seems like it's your first time here!</span><br />
       <label>Please provide your name:</label>
       @Html.TextBox("userName")
       <div class="form-group">
           <input type="submit" value="Update" class="btn btn-primary" />
       </div>
   </form>
}
```

### Cookies im Browser anschauen

x

## Session State

Gültig solange man in der App unetrwegs ist
user-kritische daten in die db und nur in performance optimierungsfällen cachen

Bei der Arbeit mit dem Session States sollten wir Folgendes berücksichtigen:

* Ein Sitzungscookie ist spezifisch für die Browsersitzung.
* Wenn eine Browsersitzung endet, wird das Sitzungscookie gelöscht
* Wenn die Anwendung ein Cookie für eine abgelaufene Sitzung empfängt, erstellt sie eine neue Sitzung, die dasselbe Sitzungscookie verwendet
* Eine Anwendung behält keine leeren Sitzungen bei
* Die Anwendung behält eine Sitzung für eine begrenzte Zeit nach der letzten Anforderung bei. Die App legt entweder das Sitzungszeitlimit fest oder verwendet den Standardwert von 20 Minuten
* Der Sitzungsstatus ist ideal zum Speichern von Benutzerdaten, die für eine bestimmte Sitzung spezifisch sind, jedoch keinen dauerhaften Speicher über mehrere Sitzungen hinweg erfordern
* Eine Anwendung löscht die in der Sitzung gespeicherten Daten entweder beim Aufruf der ISession.Clear-Implementierung oder beim Ablauf der Sitzung
* Es gibt keinen Standardmechanismus, um die Anwendung darüber zu informieren, dass ein Client den Browser geschlossen oder das Sitzungscookie gelöscht hat oder abgelaufen ist

Middleware hinzufügen, bzw. usen `Startup.cs`

Wichtig!! `UseSession()` immer vor `UseMvc()`!

```C#
public class WelcomeController : Controller
{
    public IActionResult Index()
    {
        HttpContext.Session.SetString("Name", "John");
        HttpContext.Session.SetInt32("Age", 32);

        return View();
    }

    public IActionResult Get()
    {
        User newUser = new User()
        {
            Name = HttpContext.Session.GetString("Name"),
            Age = HttpContext.Session.GetInt32("Age").Value
        };

        return View(newUser);
    }
}
```

## Query String Parameter

Eine einfache Variante Informationen von eineer Seite auf die andere mithzugeben, besteht darm, einen oder mehrere Parameter an die URL anzuhängen.

Der erste Parameter wird mit einem vorangefügten Fragezeichen angehängt, alle weiteren mit `&`.

Beispiel:

```URL
https://localhost:5001/Events/Index?filter=danc&state=45a50c5e-e109-470a-a124-1639915748af
```

**Achtung!** Parameter können im Browser manipuliert werden und ein User kann so an unerlaubte Daten gelangen.

## Hidde Fields

```C#
[HttpGet]
public IActionResult SetHiddenFieldValue()
{
    User newUser = new User()
    {
        Id = 101,
        Name = "John",
        Age = 31
    };
    return View(newUser);
}

[HttpPost]
public IActionResult SetHiddenFieldValue(IFormCollection keyValues)
{
    var id = keyValues["Id"];
    return View();
}
```

```C#
@Html.HiddenFor(model => model.Id)
```

**Achtung!** Parameter können im Browser manipuliert werden und ein User kann so an unerlaubte Daten gelangen.

## Temp Data

Hält die Daten nur für einen einzigen Request-Response-Zyklus. Aber mit `TempData.Keep();`, `TempData.Peek();` lässt sich die lebensdauer verlängern.

## Daten an die View geben

x

### Strongly Typed Data

Model mit konkretem Datentp. Daten werden mit einem konkreten Datentyp in einem Model an die View übergeben.

```C#
return View(model);
```

bzw. in der View:

```C#
@model Spg.MvcTicketShop.Services.Helper.PaginatedList<Spg.MvcTicketShop.Services.Models.Events>
```

Zugriff erfolgt mit:

```C#
@foreach (var item in Model)
{
    <label>@item.Description</label>
}
```

### Weakly Typed Data

#### ViewData

```C#
public class ViewDataController : Controller
{
    public IActionResult Index()
    {
        ViewData["UserId"] = 101;
        return View();
    }
}
```

```C#
@{
    ViewData["Title"] = "Index";
    var userId = ViewData["UserId"]?.ToString();
}

<h1>ViewData</h1>

User Id : @userId
```

Oder ein Beispiel aus unserer Applikation:

```C#
ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
```

HTML:

``` C#
<a asp-action="Index" asp-route-sortOrder="@ViewData["NameSortParm"]">
    <label asp-for="@firstEvent.Name"></label>
</a>
```

#### ViewBag

``` C#
public class ViewBagController : Controller
{
    public IActionResult Index()
    {
        ViewBag.UserId = 101;
        ViewBag.Name = "John";
        ViewBag.Age = 31;

        return View();
    }
}
```

und die dazugehörige View:

```C#
{
    ViewData["Title"] = "Index";
    var userId = ViewBag.UserId;
    var name = ViewBag.Name;
    var age = ViewBag.Age;
}

<h1>ViewBag</h1>

User Id : @userId<br />
Name : @name<br />
Age : @age<br />
```
