# Abfragen in Entity Framework Core

## Ausgeben der SQL Statements

In dieser Solution wurde die Ausgabe der SQL Statements aktiviert. Möchte man in anderen Projekten
diese Statements ausgeben, so muss das Paket *Microsoft.Extensions.Logging.Console* installiert
werden:

```powershell
Install-Package Microsoft.Extensions.Logging.Console
```

Danach wird in der Klasse *TestsContext* eine statische Membervariable (kein Property!)
*MyLoggerFactory* angelegt. Static ist wichtig, da sonst bei jeder Abfrage eine neue Instanz erzeugt
wird.

```c#
public static readonly ILoggerFactory MyLoggerFactory
    = LoggerFactory.Create(builder => { builder.AddConsole(); });
```

Zum Schluss wird in der Methode *OnConfiguring()* der Logger registriert:

```c#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        optionsBuilder
            .UseLoggerFactory(MyLoggerFactory)
            .UseSqlite("DataSource=../Tests.db");
    }
}
```

## "Klassische" Abfragebeispiele der Testdatenbank

LINQ Statements werden in SQL umgesetzt. Dabei ist es egal, ob die Query oder Method Syntax
verwendet wird.

### Beispiel 1: Filtern und anonyme Typen

```c#
from c in context.Schoolclass
where c.C_Department == "HIF"
select new
{
    Class = c.C_ID,
    Pupils = c.Pupil.Count()
};
```

```sql
SELECT "s"."C_ID" AS "Class", (
    SELECT COUNT(*)
    FROM "Pupil" AS "p"
    WHERE "s"."C_ID" = "p"."P_Class") AS "Pupils"
```

### Beispiel 2: Where und Having

Es gibt keine Unterscheidung zwischen *WHERE* und *HAVING*, denn der Filter nach der
Anzahl wird als (korrespondierende) Unterabfrage übersetzt. Hier ist der
Optimizer der Datenbank gefragt.

```c#
from c in context.Schoolclass
where c.C_Department == "HIF" && c.Pupil.Count() > 30
select new
{
    Class = c.C_ID,
    PupilsCount = c.Pupil.Count()
};
```

```sql
SELECT "s"."C_ID" AS "Class", (
    SELECT COUNT(*)
    FROM "Pupil" AS "p"
    WHERE "s"."C_ID" = "p"."P_Class") AS "Pupils"
FROM "Schoolclass" AS "s"
WHERE("s"."C_Department" = 'HIF') AND((
    SELECT COUNT(*)
    FROM "Pupil" AS "p0"
    WHERE "s"."C_ID" = "p0"."P_Class") > 30)
```

### Beispiel 3: Aufteilen von Abfragen

Wird die Abfrage in 2 Schritten definiert, liefert sie trotzdem das selbe SELECT.
*result3a* wird nämlich bei der Definition noch nicht ausgeführt.

```c#

    var result3a = from c in context.Schoolclass
                    where c.C_Department == "HIF"
                    select c;
    var result3b = from c in result3a
                    where c.Pupil.Count() > 30
                    select new
                    {
                        Class = c.C_ID,
                        PupilsCount = c.Pupil.Count()
                    };
    Console.WriteLine(JsonSerializer.Serialize(result3b));
```

```sql
SELECT "s"."C_ID" AS "Class", (
    SELECT COUNT(*)
    FROM "Pupil" AS "p"
    WHERE "s"."C_ID" = "p"."P_Class") AS "Pupils"
FROM "Schoolclass" AS "s"
WHERE("s"."C_Department" = 'HIF') AND((
    SELECT COUNT(*)
    FROM "Pupil" AS "p0"
    WHERE "s"."C_ID" = "p0"."P_Class") > 30)
```

### Beispiel 4: Rückgabe von Navigations

Geben wir Navigations explizit zurück, so werden durch einen JOIN die entsprechenden Daten geladen.

```c#
from c in context.Schoolclass
where c.C_Department == "HIF" && c.Pupil.Count() > 30
select new
{
    Class = c.C_ID,
    PupilsCount = c.Pupil.Count(),
    Pupils = c.Pupil
};
```

```sql
SELECT "s"."C_ID", (
    SELECT COUNT(*)
    FROM "Pupil" AS "p"
    WHERE "s"."C_ID" = "p"."P_Class"), 
    "p0"."P_ID", "p0"."P_Account", "p0"."P_Class", "p0"."P_Firstname", "p0"."P_Lastname"
FROM "Schoolclass" AS "s"
LEFT JOIN "Pupil" AS "p0" ON "s"."C_ID" = "p0"."P_Class"
WHERE ("s"."C_Department" = 'HIF') AND ((
    SELECT COUNT(*)
    FROM "Pupil" AS "p1"
    WHERE "s"."C_ID" = "p1"."P_Class") > 30)
ORDER BY "s"."C_ID", "p0"."P_ID"
```

### Beispiel 5: Any()

Abfragen mit Any() erzeugen automatisch eine EXISTS Klausel in SQL.


```c#
from c in context.Schoolclass
where c.Test.Any(t => t.TE_Subject == "BWM1")
select c;

```

```sql
SELECT "s"."C_ID", "s"."C_ClassTeacher", "s"."C_Department"
FROM "Schoolclass" AS "s"
WHERE EXISTS (
    SELECT 1
    FROM "Test" AS "t"
    WHERE ("s"."C_ID" = "t"."TE_Class") AND ("t"."TE_Subject" = 'BWM1'))
```

### Beispiel 6: Gruppierungen

```c#
from te in context.Test
where te.TE_ClassNavigation.C_ClassTeacher == "SZ"
group te by te.TE_Class into g
select new
{
    Class = g.Key,
    Tests = g.Count()
};
```

```sql
SELECT "t"."TE_Class" AS "Class", COUNT(*) AS "Tests"
FROM "Test" AS "t"
INNER JOIN "Schoolclass" AS "s" ON "t"."TE_Class" = "s"."C_ID"
WHERE "s"."C_ClassTeacher" = 'SZ'
GROUP BY "t"."TE_Class"
```

## Navigation Properties und Abfragen

Betrachten wir folgenden Code:

```c#
var result10 = (from c in context.Schoolclass
                where c.C_Department == "HIF"
                select c).FirstOrDefault();
// Im Speicher steht folgendes Ergebnis:
// {"C_ID":"1AHIF","C_Department":"HIF","C_ClassTeacher":"NIJ",
//  "C_ClassTeacherNavigation":null,"Lesson":[],"Pupil":[],"Test":[]}
// Daher wird "Oops, C_ClassTeacherNavigation is null." ausgegeben.
if (result10.C_ClassTeacherNavigation == null)
{
    Console.WriteLine("Oops, C_ClassTeacherNavigation is null.");
}
```

Der Unterschied zu den vorigen Abfragen, bei denen wir die Abfragen nur zur Serialisierung ausgeführt
haben, ist Folgender: Durch *FirstOrDefault()* wird die Ausführung der Abfrage erzwungen und das
Ergebnis in *result10* geschrieben. Sehen wir uns das SQL Statement dazu an:

```sql
SELECT "s"."C_ID", "s"."C_ClassTeacher", "s"."C_Department"
FROM "Schoolclass" AS "s"
WHERE "s"."C_Department" = 'HIF'
```

Wir sehen, dass nur die Tabelle *Schoolclass* selektiert wurde. Daher können die Navigation Properties
nicht befüllt sein, denn diese Daten wurden gar nicht abgefragt. Das ist auch sinnvoll, denn sonst
würden immer mehrere JOIN Operationen ausgeführt werden, obwohl man die Navigations gar
nicht brauchen würde. Dieses Verhalten nennen wir "Lazy Loading".

> **Merke:** Navigations können innerhalb der Abfragen ganz normal verwendet werden. Wird die
> Abfrage jedoch mittels *First(), Find(), ToList(), ...* ausgeführt, sind die Navigations im Ergebnis
> null bzw. leere Listen.

Möchten wir befüllte Navigations im Ergebnis zurückgeben, so können wir mit *Include()* einen JOIN
veranlassen. Dies bezeichnet man als "Explicit Loading".

```c#
var result12 = context.Schoolclass
    .Include(c => c.C_ClassTeacherNavigation)
    .SingleOrDefault(c => c.C_ID == "3BHIF");
Console.WriteLine($"KV der 3BHIF ist {result12.C_ClassTeacherNavigation.T_Lastname}");

// Explizites Laden ist auch möglich, wenn man das Entity schon hat.
var result12a = context.Schoolclass.Find("3BHIF");
context.Entry(result12a)
    .Reference(c => c.C_ClassTeacherNavigation)
    .Load();
Console.WriteLine($"KV der 3BHIF ist {result12a.C_ClassTeacherNavigation.T_Lastname}");
```

Natürlich können auch mehrere Navigationen befüllt werden. Wir unterscheiden zwischen *Include()*
und *ThenInclude()*, welches von der inkludierten Navigation weiter geht. Das sollte aber sparsam
verwendet werden. Mit einer LINQ Abfrage, die  die benötigten Daten über ein select new liefert ist
man besser bedient. Bedenke, dass hier alle Tabellen vollständig geladen werden!

Nähere Informationen gibt es auf Microsoft Docs unter https://docs.microsoft.com/en-us/ef/core/querying/related-data

```c#
// *********************************************************************************
// Hier wird Klasse -> Lehrer -> Lesson
//                  -> Schüler
// geladen
// *********************************************************************************
var result13 = context.Schoolclass
    .Include(c => c.C_ClassTeacherNavigation)
        .ThenInclude(t => t.Lesson)
    .Include(c => c.Pupil)
    .SingleOrDefault(c => c.C_ID == "3BHIF");

Console.WriteLine($"Die 3BHIF hat {result13.Pupil.Count()} Schüler.");
Console.WriteLine($"Der KV ist {result13.C_ClassTeacherNavigation.T_Lastname} und sie unterrichtet " +
    $"{result13.C_ClassTeacherNavigation.Lesson.Count()} Stunden.");
```

Folgende Lösung ist hier natürlich vorzuziehen:

```c#
from c in context.Schoolclass
where c.C_ID == "3BHIF"
select new
{
    PupilCount = c.Pupil.Count(),
    ClassTeacherName = c.C_ClassTeacherNavigation.T_Lastname,
    ClassTeacherHours = c.C_ClassTeacherNavigation.Lesson.Count()
};
```

## Find

Oft möchte man nur nach dem Primärschlüssel suchen. Die schnellste Methode, das zu erledigen, ist
*Find()*. Natürlich sind auch hier die Navigations null bzw. leer und müssen bei Bedarf nachgeladen
werden.

```c#
var result11 = context.Schoolclass.Find("3BHIF");
if (result11.C_ClassTeacherNavigation == null)
{
    Console.WriteLine("Oops, C_ClassTeacherNavigation is null.");
}
```

**Vorsicht vor folgendem Fehler:** Durch das Ausführen der Abfrage in der Methode *Find()* sind
die Navigationen leer. Die erste der folgenden 2 Abfragen funktioniert wie erwartet und liefert 31 
Schüler. Die 2. Abfrage liefert allerdings 0.

```c#
var pupils = (from c in context.Schoolclass
                where c.C_ID == "3BHIF"
                select c.Pupil.Count).SingleOrDefault();    // Liefert 31

pupils = context.Schoolclass.Find("3BHIF").Pupil.Count();   // Liefert 0
```

## Lazy Loading

Es gibt eine Möglichkeit, die Navigation Properties bei Bedarf automatisch nachzuladen. Dafür
wird das Paket *Microsoft.EntityFrameworkCore.Proxies* mit Install-Package installiert. In der
Methode *OnConfiguring()* wird dann *UseLazyLoadingProxies()* eingefügt:

```c#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseLoggerFactory(MyLoggerFactory)
            .UseSqlite("DataSource=Tests.db");
    }
}
```

Nach dieser Einstellung ist folgender Code möglich:

```c#
var result11 = context.Schoolclass.Find("3BHIF");
Console.WriteLine(result11.C_ClassTeacherNavigation.T_Lastname);
```

Wir werden diese Option allerdings nicht aktivieren, da dieses Paket sehr viele Abhängigkeiten
zu anderen .NET Versionen hat und diese dann ebenfalls ins Projekt geladen werden. Außerdem haben
wir ohne diesen Automatismus die volle Kontrolle darüber, wann Abfragen ausgeführt werden.

## Views und EF Core

Wir haben gesehen, dass wir (fast) keinen Einfluss auf die Umsetzung der LINQ Abfragen in SQL Code haben.
Möchte man datenbankspezifische Funktionen (Partitions, ...) oder einfach nur eine bessere Steuerung
der SQL Abfragen haben, so können auch Views erstellt werden.

Wir können z. B. die Klassen über 30 Schüler als View in der Datenbank anlegen:

```sql
CREATE VIEW BigClasses AS
    SELECT s.C_ID AS ClassId, s.C_Department AS Department, COUNT(*) AS PupilCount
    FROM Schoolclass s INNER JOIN Pupil p ON (s.C_ID = p.P_Class)
    GROUP BY s.C_ID
    HAVING COUNT(*) > 30;
```

Erstellen wir nun mit *Scaffold-DbContext* unsere Modelklassen, so wird auch diese View berücksichtigt
und eine normale Modelklasse dafür angelegt:

```c#
public partial class BigClasses
{
    [Column(TypeName = "VARCHAR(8)")]
    public string ClassId { get; set; }
    [Column(TypeName = "VARCHAR(8)")]
    public string Department { get; set; }
    public int PupilCount { get; set; }
}
```

Gerade beim nächsten Punkt (Grenzen von EF Core) kann das ein hilfreicher Workaround sein.

## Grenzen von EF Core

Wir wollen alle Tests gruppiert nach dem Fach zurückgeben. Das ist ein häufiger Anwendungsfall, wenn
aus flachen Tabellen hierarchische JSON Objekte erzeugt werden sollen.

```c#
var tests = from t in context.Test
            group t by t.TE_Subject into g
            select new
            {
                Subject = g.Key,
                Tests = g
            };
Console.WriteLine(JsonSerializer.Serialize(tests));
```

Die Abfrage liefert jedoch einen Laufzeitfehler:

```text
Processing of the LINQ expression '(GroupByShaperExpression:
KeySelector: (t.TE_Subject),
ElementSelector:(EntityShaperExpression:
    EntityType: Test
    ValueBufferExpression:
        (ProjectionBindingExpression: EmptyProjectionMember)
    IsNullable: False
)
)' by 'RelationalProjectionBindingExpressionVisitor' failed. This may indicate either a bug or a
limitation in EF Core. See https://go.microsoft.com/fwlink/?linkid=2101433 for more detailed information.
```

Gut, das zurückgeben der reinen Gruppierungsvariable ist nie eine gute Idee. Also wählen wir mit
*Select()* die Properties aus, die wir pro Fach als JSON Array ausgeben wollen.

```c#
var tests = from t in context.Test
            group t by t.TE_Subject into g
            select new
            {
                Subject = g.Key,
                Tests = g.Select(te => new { te.TE_Date, te.TE_Lesson, te.TE_Teacher})
            };
Console.WriteLine(JsonSerializer.Serialize(tests));
```

Doch auch das hilft uns nicht weiter. Es entsteht folgender Laufzeitfehler:

```text
The LINQ expression '(GroupByShaperExpression:
KeySelector: (t.TE_Subject),
ElementSelector:(EntityShaperExpression:
    EntityType: Test
    ValueBufferExpression:
        (ProjectionBindingExpression: EmptyProjectionMember)
    IsNullable: False
)
)
    .Select(te => new {
        TE_Date = te.TE_Date,
        TE_Lesson = te.TE_Lesson,
        TE_Teacher = te.TE_Teacher
     })' could not be translated. Either rewrite the query in a form that can be translated, or
     switch to client evaluation explicitly by inserting a call to either AsEnumerable(),
     AsAsyncEnumerable(), ToList(), or ToListAsync(). See
     https://go.microsoft.com/fwlink/?linkid=2101038 for more information.

```

Der Hinweis auf *client evaluation* bringt die Lösung. Mit *AsEnumerable()* wird die Abfrage schon
beim Auslesen der Tests ausgeführt. Der Rest geschieht am Client im Speicher. Im Gegensatz zu
*ToList()* wird nur ein IEnumerable Typ zur Verfügung gestellt, welcher schon abgearbeitet werden
kann, bevor noch alle Daten da sind (wenn das möglich ist).

```c#
var tests = from t in context.Test.AsEnumerable()          // Hier wird SELECT * FROM Test gesendet.
            group t by t.TE_Subject into g                 // Alles Weitere im Speicher am Client.
            select new
            {
                Subject = g.Key,
                Tests = g.Select(te => new { te.TE_Date, te.TE_Lesson, te.TE_Teacher })
            };
Console.WriteLine(JsonSerializer.Serialize(tests));
```

Doch jetzt  möchten wir nur die HIF Abteilung ausgeben. Mit *where* kann das leicht durchgeführt
werden:

```c#
var tests = from t in context.Test.AsEnumerable()
            where t.TE_ClassNavigation.C_Department == "HIF"
            group t by t.TE_Subject into g
            select new
            {
                Subject = g.Key,
                Tests = g.Select(te => new { te.TE_Date, te.TE_Lesson, te.TE_Teacher })
            };
Console.WriteLine(JsonSerializer.Serialize(tests));
```

Diese Abfrage liefert jedoch eine Exception (*"Object reference not set to an instance of an object."*),
da durch *AsEnumerable()* die Abfrage ausgeführt wird und somit keine Navigations zurückgegeben werden.
Diesen Fehler können wir mit *Include()* beheben:

```c#
from t in context.Test.Include(t=>t.TE_ClassNavigation).AsEnumerable()
where t.TE_ClassNavigation.C_Department == "HIF"
group t by t.TE_Subject into g
select new
{
    Subject = g.Key,
    Tests = g.Select(te => new { te.TE_Date, te.TE_Lesson, te.TE_Teacher })
};
```

Der Preis ist allerdings sehr hoch: es werden alle Tests samt Klassen geladen:

```sql
SELECT "t"."TE_ID", "t"."TE_Class", "t"."TE_Date", "t"."TE_Lesson", "t"."TE_Subject",
       "t"."TE_Teacher", "s"."C_ID", "s"."C_ClassTeacher", "s"."C_Department"
FROM "Test" AS "t"
INNER JOIN "Schoolclass" AS "s" ON "t"."TE_Class" = "s"."C_ID"
```

Erst das Filtern vor *AsEnumerable()* verhindert das Auslesen ohne WHERE Bedingung:

```c#
from t in context.Test
    .Include(t=>t.TE_ClassNavigation)
    .Where(t=>t.TE_ClassNavigation.C_Department == "HIF")
    .AsEnumerable()
group t by t.TE_Subject into g
select new
{
    Subject = g.Key,
    Tests = g.Select(te => new { te.TE_Date, te.TE_Lesson, te.TE_Teacher })
};
```

```sql
SELECT "t"."TE_ID", "t"."TE_Class", "t"."TE_Date", "t"."TE_Lesson", "t"."TE_Subject", 
       "t"."TE_Teacher", "s"."C_ID", "s"."C_ClassTeacher", "s"."C_Department"
FROM "Test" AS "t"
INNER JOIN "Schoolclass" AS "s" ON "t"."TE_Class" = "s"."C_ID"
WHERE "s"."C_Department" = 'HIF'
```
