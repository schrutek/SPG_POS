# ASP.net Core MVC (Views, Partial Views)

Wie bereits kennen gelernt, Views und Partial Views enthalten das für die darstellung nötige HTML, CSS und Razor.

## Die wichtigsten Views

### Layout.cshtml

Diese View stellt den "Rahmen" der Webseite dar. Bei jedem Seitenaufruf wird sie geladen. In den Razor-Tag `@RenderBody()` wird der Inhalt der jeweiligen View platziert.

### Die Views eines Controllers

Die Aufteilung per Konvention ist so, dass im Verzeichnis `Views` ein Unterverzeichnis mit gleichem Namen wie der Controller existiert. z.B.: `SchoolclassesController` => `Schoolclasses`. Darunter existieren dann die einzelnen Views (Index, Create, Edit, Delete, ...). Die Namen der Views entsprechen der Action-Methode im Conrtroller.

![GeneratedCode](GeneratedCode.PNG)

## Razor

Kurz gesagt, Razor ist eine Markup-Syntax die in den Views verwendet werden kann. Details dazu auf MS-Docs:

[https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-3.1]

### Tag Helper

[https://docs.microsoft.com/en-us/aspnet/core/mvc/views/working-with-forms?view=aspnetcore-3.1#the-form-tag-helper]

Ein Besipiel dazu für Routing:

```C#
<a asp-controller="Lessons" asp-action="IndexByLessonId" asp-route-schoolclassId="@item.C_ID">Lessons</a>
```

```C#
// GET: Schoolclasses/Details/5
[HttpGet("{id}")]
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

    return View("Details", model);
}
```

[https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/built-in/anchor-tag-helper?view=aspnetcore-3.1]

## Partial Views

Anders als Views sind Partial Views üblicherweise dazu da um nicht eine ganze Seite zu endern, sondern einen kleinen Teil einer anderen View wiederzuverwenden.

Beispiel: Ein Klasse hat mehrere Stunden. Es gibt eine View die alle Klassen anzeigt und eine View, die die Stunden einer Klasse anzeigt. Nun sollen aber die Stunden einer Klasse zusätzlich auf der Detailseite der Klasse angezeigt werden. Dafür eignet sich eine PartialView, die man für beides einsetzen kann.

![PartialView](PartialView.PNG)

Gemäß der Namenskonvention beginnt eine PartialBiew immer mit einem Unterstrich `_partial.cshtml`.

Der Controller muss in einer Action-Methode eine PartialView zurück geben:

```C#
public async Task<IActionResult> FilteredByIdPartial(Guid eventId)
{
    throw new NotImplementedException("Test-Error");
    if (eventId == null)
    {
        return NotFound();
    }

    var model = await _showService.GetAllByEventAsync(eventId);

    if (model == null)
    {
        return NotFound();
    }

    return PartialView("_Shows", model);
}
```

die Partial View: Achtung, diese enthält ein Model!

```HTML
@model IEnumerable<Spg.MvcTestsAdmin.Service.Models.Lesson>

@{
    var schoolclassId = ViewData["schoolclassId"];
}

<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.L_Untis_ID)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.L_Subject)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.L_Room)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.L_Day)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.L_ClassNavigation)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.L_HourNavigation)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.L_TeacherNavigation)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model)
        {
            <tr>
                <td>
                    @Html.DisplayFor(modelItem => item.L_Untis_ID)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.L_Subject)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.L_Room)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.L_Day)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.L_ClassNavigation.C_ID)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.L_HourNavigation.P_Nr)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.L_TeacherNavigation.T_ID)
                </td>
                <td>
                    <a asp-controller="Lessons" asp-action="Edit" asp-route-id="@item.L_ID">Edit</a> |
                    <a asp-controller="Lessons" asp-action="Details" asp-route-id="@item.L_ID" asp-route-schoolclassId="@schoolclassId">Details</a> |
                    <a asp-controller="Lessons" asp-action="Delete" asp-route-id="@item.L_ID">Delete</a>
                </td>
            </tr>
        }
    </tbody>
</table>
```

Der Aufruf der Partial View erfolgt dann auf der jeweiligen BView (in diesem Beispiel die `Details.cshtml` von `Schoolclasses`) durch einbetten dieses Codes:

```HTML
<h1>Lessons</h1>
<partial name="../Lessons/_Lessons" model="Model.Lesson" />
```
