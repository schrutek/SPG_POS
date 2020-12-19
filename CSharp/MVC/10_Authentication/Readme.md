# ASP.net Core MVC (Authentication)

## Cookie Based

<https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-3.1>

bzw. Sources:

<https://github.com/dotnet/AspNetCore.Docs/tree/master/aspnetcore/security/authentication/cookie/samples>

## Erläuterungen zum Projekt

### Startup.cs

In der `ConfigureServices` Methode benötigen wir:

```C#
/// *** Authentication, Authorization hinzufügen
services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();
/// ***
```

**Zusatz**
Hier wird das Default-Scheme angegeben. Die Settings sind vorgegeben und man kann das so verwenden. Soll z.B. die URL  zur Login-View anders heißen, kann man das mit einer Lanbda Expression konfigurieren:

```C#
services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
        {
            config.Cookie.Name = "UserLoginCookie";
            config.LoginPath = "/Account/MyLogin";
        });
```

In der `Configure` Methode benötigen wir (am besten nach `UseRouting()`):

```C#
/// *** Authentication, Authorization aktivieren
app.UseAuthentication();
app.UseAuthorization();
var cookiePolicyOptions = new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax,
};
app.UseCookiePolicy(cookiePolicyOptions);
/// ***
```

Eine kleine Extension-Klasse ist beim Redirecten ganz hilfreich. Natürlich kann man diese Logik auch in den Controller schreiben, aber es hilft wenn wir das vielleicht öfter benötgen.

```C#
public static class UrlHelperExtensions
{
    public static string GetLocalUrl(this IUrlHelper urlHelper, string localUrl)
    {
        if (!urlHelper.IsLocalUrl(localUrl))
        {
            return urlHelper.Page("/Home/Index");
        }
        return localUrl;
    }
}
```

### Controller

Natürlich möchten wir uns mittels Login-Formular anmelden können. (Username/EMail, Password). Dafür benötigen wir natürlich einen Controller (z.B. `AccountController`) mit einigen Methoden.

Als erstes die Get-Methode, die uns die Login-View rendert:

```C#
[HttpGet()]
public async Task<IActionResult> Login(string returnUrl = null)
{
    // Löscht das existierende Cookie
    await HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);
    return View();
}
```

Anschließend eine Post-Methode, die das Model befüllt entgegen nimmt, validiert und den Login ausführt:

Allerdings vorher, zum besseren Verständnis, jene Methode in dieser Controller-Klasse, welche die eigentliche validierung der eingegebenen Daten durchfürt und das dafür notwendige Model:

```C#
private async Task<ApplicationUser> AuthenticateUser(string email, string password)
{
    // Lediglich zu Demonstrationszwecken wird hier der User nur 
    // mittels E-Mail-Adresse (schrutek@spengergasse.at") validiert. 
    // Das Kennwort wird ignoriert
    //
    // Das Delay simuliert einen Datenbankzugriff der ja einige Zeit dauern kann
    await Task.Delay(500);

    if (email == "schrutek@spengergasse.at")
    {
        return new ApplicationUser()
        {
            Email = "schrutek@spengergasse.at",
            FullName = "Martin Schrutek"
        };
    }
    else
    {
        return null;
    }
}
```

Das Model wird idealerweise im dafür vorgesehenen Namespace platziert.

```C#
public class ApplicationUser
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string FullName { get; set; }
}
```

Nun die Post-Methode:

```C#
[HttpPost]
public async Task<IActionResult> Login(ApplicationUser model, string returnUrl = null)
{
    if (ModelState.IsValid)
    {
        // Lediglich zu Demonstrationszwecken wird hier der User nur 
        // mittels E-Mail-Adresse (schrutek@spengergasse.at") validiert. 
        // Das Kennwort wird ignoriert
        var user = await AuthenticateUser(model.Email, model.FullName);

        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim("FullName", user.FullName),
            new Claim(ClaimTypes.Role, "Administrator"),
        };

        var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            //AllowRefresh = <bool>,
            // Refreshing the authentication session should be allowed.

            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
            // CookieAuthenticationOptions set with AddCookie.

            //IsPersistent = true,
            // Whether the authentication session is persisted across 
            // multiple requests. When used with cookies, controls
            // whether the cookie's lifetime is absolute (matching the
            // lifetime of the authentication ticket) or session-based.

            //IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.

            //RedirectUri = <string>
            // The full path or absolute URI to be used as an http 
            // redirect response value.
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        _logger.LogInformation("User {Email} logged in at {Time}.",
            user.Email, DateTime.UtcNow);

        if (!String.IsNullOrEmpty(returnUrl))
        {
            // Senden von 302 Redirect (nicht RedirectToPage, denn hier würde kein neuer Request
            // des Browsers gestartet werden).
            return LocalRedirect(Url.GetLocalUrl(returnUrl));
        }
        else
        {
            return LocalRedirect("/Home/Index");
        }
    }
    // Im Fehlerfall wird wieder die View  mit leeren Textfeldern retuniert.
    return View();
}
```

Was passiet hier alles:

* Die eigentliche Validierung der Benutzereingaben (Username/E-Mail, Password) werden überprüft. Idealerweise findet hier natürlich ein Abgleich mit den vorhandenen Daten in der Datenbank statt.
* Danach wird eine Liste mit Claims erstellt. Claims enthalten zusätzliche Daten, die ebenfalls im Cookie abgelegt werden sollen. Mehr zum Thema Claims: <https://docs.microsoft.com/en-us/dotnet/api/system.security.claims.claim?view=net-5.0#remarks>
* ClaimsIdentity setzen
* Authentication Properties erstellen. Das sind Optionen, das Cookie betreffend.
* Nun wird die Methode `SignInAsync` aufgerufen, mit den erstellten Optionen und Claims als Parameter. `CookieAuthenticationDefaults.AuthenticationScheme` ist der Default Wert für Cookie Authentication. Wir können das so lassen. Der Http Context ist Bestandteil der Basisklasse.
* Als letztes wird auf eine View umgeroutet. Wenn eine Return-Url angegeben wurde, wird diese verwendet. War das nicht der Fall wählt man eine Route aus. Ist etwas schief gegangen, wird die View wieder zum Client gerendert. Auch in diesem Fall ohne Daten. An dieser Stelle wird auch die Extension Method verwendet.

Eine weitere Post-Methode wird für den Log Out benötigt:

```C#
[HttpPost]
public async Task<IActionResult> Logout()
{
    _logger.LogInformation("User {Name} logged out at {Time}.", User.Identity.Name, DateTime.UtcNow);

    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

    return LocalRedirect("/Account/SignedOut");
}
```

Hier passiert nicht viel:

* Es wird wieder der HTTP Context (Basisklasse) verwendet um sich abzumelden.
* Es wird auf eine Seite umgeroutet. In unserem Fall wird die View `SignedOut` gerendert. Diese benötigt natürlich eine Get-Methode im Controller.

```C#
[HttpGet()]
public IActionResult SignedOut()
{
    if (User.Identity.IsAuthenticated)
    {
        // Redirect zu Home wenn der User nicht authentifiziert ist
        return RedirectToPage("/Index");
    }
    return View();
}
```

### Die Views

Natürlich benötigen wird auch einige Views. Da der Controller `AccountController` heißt, sind alle Views (bis auf eine) im Verzeichnis `Views\Account` zu erstellen.

**`Login.cshtml`**

```html
@model Spg.CoocieAuthentication.Mvc.Models.ApplicationUser

<h2>@ViewData["Title"]</h2>
<div class="row">
    <div class="col-md-3">
        <form method="post">
            <p>Sign into the app with the <b>Email</b> address <code>maria.rodriguez@contoso.com</code> and any password.</p>
            <hr>
            <div asp-validation-summary="All" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="Email"></label>
                <input asp-for="Email" class="form-control">
                <span asp-validation-for="Email" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Password"></label>
                <input asp-for="Password" class="form-control">
                <span asp-validation-for="Password" class="text-danger"></span>
            </div>
            <div class="form-group">
                <button type="submit" class="btn btn-default">Log in</button>
            </div>
        </form>
    </div>
</div>

@section Scripts {
    @await Html.PartialAsync("_ValidationScriptsPartial")
}
```

**`SignedOut.cshtml`**

```html
<h2>@ViewData["Title"]</h2>
<p>
    You have successfully signed out.
</p>
```

Wir benötigen nun noch eine Parrtial View, welche den Login-Button im Menü bereitstellt, oder wenn man authetfiziert ist, Informationen zum User anzeigt. Das hilft dem User jederzeit optisch zu verifizieren, ob er authentifiziert ist oder nicht. PCs werden manchmal von mehreren Menschen verwendet (Familie).

**`_LoginPartial.cshtml`**

```html
@inject Microsoft.AspNetCore.Http.IHttpContextAccessor HttpContextAccessor;

@if (HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
{
    <form asp-controller="Account" asp-action="Logout" method="post" id="logoutForm">
        <ul class="navbar-nav flex-grow-1">
            <li class="nav-item">
                <span class="nav-link">Welcome @User.Identity.Name!</span>
            </li>
            <li class="nav-item">
                <button type="submit" class="nav-link btn btn-light">Sign out</button>
            </li>
        </ul>
    </form>
}
else
{
    <ul class="navbar-nav flex-grow-1">
        <li class="nav-item">
            <a class="nav-link btn-light" asp-controller="Account" asp-action="Login">Sign in</a>
        </li>
    </ul>
}
```

Das besondere an dieser View ist die erste Zeile. Hier wird der HTTP-Context injected. Deshalb musste er in der `Startup.cs` registriert werden. Auch in Views ist es möglich, Services mittels Dependency Injection zu injizieren. Eine simple If-Bedingung unterscheidet zwischen den beiden Anzeigen (eingeloggt oder nicht).

**Änderungen an der `_Layout.cshtml`**

```html
<div class="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
    <partial name="_LoginPartial" />
    <ul class="navbar-nav flex-grow-1">
        <li class="nav-item">
            <a class="nav-link btn-light" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
        </li>
        <li class="nav-item">
            <a class="nav-link btn-light" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
        </li>
    </ul>
</div>
```

### Die Authentifizierung im Controller überprüfen

Jetzte ist ein Benutzer im HTTP-Context aurhentifiziert. Diese Authentifizierung müssen wir nun auch im Controller überprüfen. Dazu verwendet man das Attribute `Authorize`.

Am Beispiel der  Methode `Privacy` im Home Controller ist die verdeutlicht:

```C#
[Authorize(Roles = "User,Administrator")]
public IActionResult Privacy()
{
    return View();
}
```

Durch das Attribut `Authorize` dürfen nun nur noch Benutzer diese Methode aufrufen, die den Rollen User oder Administrator angehören. Ist man nicht authentifiziert, wird man automatiwsch auf die Login-Seite umgeleitet.
