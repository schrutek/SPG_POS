# ASP.net Core MVC (DB-Zugriffe)

Wir wollen mit der Applikation natürlich Daten aus einer DB lesen und darstellen, bzw. diese manipulieren, also CRUD-Operations. Wie ja bekannt sind das die gängigsten Anwendungsfälle bei Datenbankapplikationen.

## Model erstellen

<https://github.com/schrutek/SPG_POS/blob/master/CSharp/MVC/03_DB-Zugriff/Scuffolding.md>

## Scuffolding des Models

Wir Scuffolden nun ein Model um daraus Viewas und Controller erstellen zu können.

Dazu:
Im Contextmenü: Add, New Scuffolded Item... ; MVC Controller with Views using EF
DB Context angeben, usw.

![Scuffolding](Scuffolding.png)

Wir erhalten jede Menge autogenerirten Code. Im Prinzip ist es aber recht einfach zu verstehen.

Es wird eine Controller-Klasse erstellt und in einem eigenen Unterverzeichnis die dazugehörigen Views (Model-View-Controller)

![GeneratedCode](GeneratedCode.PNG)

### Beispiele für Routen

* GET   Schoolclasses – Liste aller Klassen
* GET   Schoolclasses/Details/{id} – Details einer Klasse
* GET   Schoolclasses/Create – Zeigt die View zur Dateneinagabe an
* POST  Schoolclasses/Create – Erstellt einen neuen Datensatz
* GET   Schoolclasses/Edit/{id} – Zeigt die View zur Dateneinagabe an
* POST  Schoolclasses/Edit/{id} – Aktualisiert den Datensatz
* GET   Schoolclasses/Delete/{id} – Zeigt die Bestätigungsseite zum Löshen an
* POST  Schoolclasses/Delete/{id} – Löscht den Datensatz

## Konfiguration der Datenbank

Nach dem Erstellen der Kontext-Klasse steht dort der Connection String zu DB drin. Das darf natütrlich nicht sein, wenn man die Solution auf GIT einchecken möchte (Security Issue). Daher verlagern wir das in eine Extension Klasse.

```C#
public static class SqlExtensions
{
    public static void ConfigureMsSql(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TicketShopContext>(optionsBuilder =>
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(connectionString);
            }
        });
    }
}
```

In der ``startup.cs```brauchen wir folgende Erweiterung

```C#
services.ConfigureMsSql($"{Configuration["AppSettings:Database"]}");
```

Der Connection-String kommt in die ``appconfig.json``, die wir natürich nicht auf GIT einchecken würden (``gitignore.json``).

```C#
{
  "AppSettings": {
    "Database": "Data Source=.\\SQLEXPRESS;Initial Catalog=TestsAdministrator;Integrated Security=True;",
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

## Die erste Liste

Im Prinzip funktioniert der generierte Code bereits und liefert ein schönes Ergebnis. Aber man kann ihn natürlich erweitern:

```C#
@if (Model != null
    && Model.Count > 0)
{
    <table class="table">
        <thead>
            <tr>
                ...
           </tr>
        </thead>
        <tbody>
            @foreach (var item in Model)
            {
                <tr>
                    ...
                </tr>
            }
        </tbody>
    </table>
}
else
{
    <label>Keine Ergebnisse gefunden!</label>
}
```

### Tag-Helper

Details hier: <https://docs.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-3.1#the-form-tag-helper>

Tag-Helper werden mit den Prefix `asp-` versehen. Das ist aber lediglich Konvention. DAhinter steht, wi auch bei den alten Tag-Helpern eine C# Klasse, die HTML erzeugt.

`asp-for` extrahiert den Namen des Properties und stellt diesen dar:

```HTML
<label asp-for="@firstEvent.Name"></label>
```

Eine andere Variante als die autogenerierte um die Items in der Tabelle darzustellen ist mittels dem HTML-Tag `label`:

```HTML
<label>@item.Name</label>
```

 z.B:

 ```HTML
<th>
    <a asp-action="Index" asp-route-sortOrder="@ViewData["NameSortParm"]">
        <label asp-for="@firstEvent.Name"></label>
    </a>
</th>
```

Einen Action-Link erzeugt man z.B. so:

```HTML
 <a asp-action="Edit" asp-route-id="@item.C_ID">Edit</a>
 ```

 `asp-route` ist dabei der Route-Parameter, welcher nachher zur  GET-URL hinzugefügt wird: (siehe oben, "Beispiele für Routen")

oder:

 ```HTML
 <a asp-controller="Lessons" asp-action="IndexByLessonId" asp-route-schoolclassId="@item.C_ID">Lessons</a>
 ```

 Der Parameter in der GET-Controllermethode würde hier `schoolclassId` heißen. Man kann belibig viele `asp-route-` angeben. Gut ist es aber nicht (**Security-Isues**). Wenn möglich auf ID's beschr#nken. Keinesfalls Kontonummern, Kreditkartennummern oder Kennwörter in die URL schreiben!! (Achtung wegen Cross Site Scripting). Sogar Namen sollten vermieden werden.

## Edit Page

Der generierte Code muss im Normalfall nicht großertig verändert werden, aber die clientseitige validierung wäre erwähnenswert.

```HTML
<form asp-controller="Lessons" asp-action="Edit">
    <div asp-validation-summary="ModelOnly" class="text-danger"></div>
    <input type="hidden" asp-for="L_ID" />
    <div class="form-group">
        <label asp-for="L_Untis_ID" class="control-label"></label>
        <input asp-for="L_Untis_ID" class="form-control" />
        <span asp-validation-for="L_Untis_ID" class="text-danger"></span>
    </div>
    ...
</form>
```

## Create Page

Die View gleicht im Prinzip jener der Edit-Page. Die Controller-Methode ist natürlich etwas anders.

## Delete Page

Hie gibt es eine View für die GET-Methode und eine für die POST-Methode. Die Get-Methode zeigt den zu löschenden Datensatz nochmal an (im Prinzip genau so, wie die Details-Page) die POST-Methode löscht den Datensatz dann endgültig. Das ist quasi eine "Wollen Sie diesen Datensatz wirklich löschen"-Frage.
