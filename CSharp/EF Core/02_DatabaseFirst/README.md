# Database First mit EF Core

## Erzeugen der Modelklassen mit den EF Core Tools

Um eine neue Applikation zu erstellen, die EF Core verwendet, gibt es zwei Wege.

### Möglichkeit 1: Über die dotnet CLI Tools (für VS Code Anwender)

Dieser Weg funktioniert auf allen unterstützten Betriebssystemen (Windows, Linux, macOS). Zuerst
muss in der Konsole (CMD oder Powershell) das CLI Tool für das Entity Framework installiert
werden:

```powershell
dotnet tool update --global dotnet-ef
```

> **Hinweis:** Nach der Installation der ef Tools muss die Konsole neu geöffnet werden, da die PATH
> Variable geändert wurde.

Bei zukünftigen Projekten muss dies nicht mehr durchgeführt werden, bei einem Upgrade der .NET Version
ist das Update allerdings wichtig, um die neuesten Pakete zu laden.

Nun wird im Ordner des Projektes mit *dotnet new* eine neue Konsolenapplikation mit dem Namespace
*DatabaseFirst* im aktuellen Verzeichnis erzeugt. Wird kein Parameter *-n* angegeben, so wird der Verzeichnisname
als Projektname verwendet, was oft der Fall sein wird. Mit den ef Tools steht uns
der Befehl *dotnet ef dbcontext scaffold* zur Verfügung, der die Modelklassen aus einer Datenbank
generiert.

```powershell
dotnet new console -n DatabaseFirst -o .
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet ef dbcontext scaffold "DataSource=Tests.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Model --use-database-names --force --data-annotations
```

Details zum Tool *dotnet ef* (Parameter, Connectionstrings) können auf [Microsoft Docs](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)
nachgelesen werden.

### Möglichkeit 2: Über die Packet Manager Console in Visual Studio

Natürlich kann auch in Visual Studio eine neue Konsolenanwendung erstellt werden. 
Unter *Tools - NuGet Packet Manager - Packet Manager Console* wird danach die Konsole des
Packet Managers eingeblendet. Hier können die NuGet Commandlets der Powershell verwendet werden.
Leider weichen die Parameter hinsichtlich ihrer Großschreibung ab, achte deshalb auf die Schreibweise.
Mit folgenden Befehlen können dort die Pakete für das Entity Framework geladen sowie das
*Scaffold-DbContext* Skript gestartet werden:

```powershell
dotnet new console -n DatabaseFirst -o .
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Microsoft.EntityFrameworkCore.Sqlite
Scaffold-DbContext "DataSource=Tests.db" Microsoft.EntityFrameworkCore.Sqlite -OutputDir Model -UseDatabaseNames -Force -DataAnnotations
```

Nun kann die *csproj* Datei (Jedoch nicht der Ordner!) in Visual Studio geöffnet werden. Beim Speichern
wird zusätzlich eine *sln* Datei von Visual Studio erzeugt und kann im selben Ordner gespeichert werden.

## Die Modelklassen der Tests.db

Wir betrachten nun die Modelklasse der Tabelle *Schoolclass*, die aus 3 Spalten (*C_ID*, *C_Department*
und *C_ClassTeacher*) besteht:

```c#
public partial class Schoolclass
{
    public Schoolclass()
    {
        Lesson = new HashSet<Lesson>();
        Pupil = new HashSet<Pupil>();
        Test = new HashSet<Test>();
    }

    [Key]
    [Column(TypeName = "VARCHAR(8)")]
    public string C_ID { get; set; }
    [Required]
    [Column(TypeName = "VARCHAR(8)")]
    public string C_Department { get; set; }
    [Column(TypeName = "VARCHAR(8)")]
    public string C_ClassTeacher { get; set; }

    [ForeignKey(nameof(C_ClassTeacher))]
    [InverseProperty(nameof(Teacher.Schoolclass))]
    public virtual Teacher C_ClassTeacherNavigation { get; set; }
    [InverseProperty("L_ClassNavigation")]
    public virtual ICollection<Lesson> Lesson { get; set; }
    [InverseProperty("P_ClassNavigation")]
    public virtual ICollection<Pupil> Pupil { get; set; }
    [InverseProperty("TE_ClassNavigation")]
    public virtual ICollection<Test> Test { get; set; }
}
```

Folgende Punkte fallen uns auf:

- Es handelt sich um eine *partial class*. Das bedeutet, dass in einer anderen Datei diese Klasse
  unter dem selben Namen erweitert werden kann. Das ist sinnvoll, da die Datei nach einer Änderung 
  der Datenbank eventuell ersetzt wird.
- Über den Properties befinden sich Annotations. Da Constraints und manche Datentypen (*VARCHAR* mit
  Stringlänge) nicht 1:1 in C# Syntax abgebildet werden kann, werden diese Informationen als
  Annotation hinzugefügt.
- (Nicht sichtbar) Nullable Felder in den Tabellen werden auf einen nullable Type in C# abgebildet.
  Strings können den Wert null haben, bei Wertedatentypen erfolgt die Abbildung auf *int?, long?, ...*
- Neben *C_ClassTeacher* wird *C_ClassTeacherNavigation* erzeugt. Durch die Annotation weiß das
  Entity Framework, dass es mit der Tabelle Teacher über Property *Schoolclass* verknüpft wird.
- Zu *Lesson*, *Pupil* und *Test* besteht eine 1:n Beziehung. Die Navigation ist daher als Collection
  abgebildet. Auch hier wird über eine Annotation die Spalte des Fremdschlüssels bekannt gegeben.

## Der Context von Tests.db

Es wird auch eine Datei mit dem Namen *TestsContext.cs* mit einer Klasse *TestsContext* generiert. 
Sie teilt sich in 3 Teile auf

### 1: DbSet als Abbildung der ganzen Tabelle

Der erste Teil besteht aus Proeprties vom Typ *DbSet*. Sie bilden die ganze Tabelle ab und sind der
Startpunkt für unsere LINQ Abfragen.

### 2: Die Methode *OnConfiguring()*

Beim erstellen der Modelklassen haben wir den Namen der Datenbank bzw. den Verbindungsstring angegeben.
Dieser muss natürlich gespeichert werden, schließlich muss das Entity Framework auf die richtige
Datenbank zugreifen.

Die Warnmeldung besagt, dass Connection Strings mit dem Datenbankpasswort, wie sie für MySQL benötigt
werden, im Quelltext gespeichert werden. Wird das Projekt nun auf Github hochgeladen, gelangt das
Datenbankpasswort unbeabsichtigt an die Öffentlichkeit.

Später werden wir diese Information in einer Datei *appsettings.json* hinterlegen, bei SQLite können wir
diese Warnung vorerst einmal auskommentieren.

```c#
if (!optionsBuilder.IsConfigured)
{
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
    optionsBuilder.UseSqlite("DataSource=Tests.db");
}
```

### 3: Die Methode *OnModelCreating()*

Nicht alle Informationen über die Datenbank können als Annotation abgebildet werden. Diese Methode
speichert noch erweiterte Eigenschaften der einzelnen Datenbankfelder (Autowerte, Navigations, ...).
Sie muss in der Regel nicht editiert werden, bei SQLite gibt es jedoch Handlungsbedarf.

#### Anpassungen für SQLite: Autoincrement und Datentypen

Wird eine Spalte als *AUTOINCREMENT* Wert deklariert, wird dies nicht korrekt erkannt. Um das zu
beheben wird beim entsprechenden Entity statt der Methode *ValueGeneratedNever()* die Methode
*ValueGeneratedOnAdd()* verwendet. Achte darauf, dass die richtige Tabelle und nur das Property
für den Primärschlüssel editiert wird. Natürlich dürfen nur Autoincrement Werte so nachbearbeitet
werden.

```c#
modelBuilder.Entity<Pupil>(entity =>                      // Tabelle Pupil
{
    // ...
    entity.Property(e => e.P_ID).ValueGeneratedOnAdd();  // Statt ValueGeneratedNever()
    // ...
});
```

Außerdem werden Datumswerte und *DECIMAL(p, n)* Typen in den einzelnen Modelklassen als Bytearray
generiert. Hier muss *byte[]* durch *DateTime* bzw. einen entsprechenden Typ für die *DECIMAL(p, n)*
Spalte ersetzt werden.

## Übung

Erstelle danach auf deiner Festplatte einen Ordner *Projects* und erzeuge eine neue Konsolenapplikation mit dem Namen *Spg.EfCore.Tests*.
Füge danach die NuGet Pakete hinzu und generiere aus der Datenbank die Modelklassen in einen Unterordner *Model*

Kopiere danach folgenden Inhalt in die `Program.cs`, ergänze den Code und lasse das Programm laufen. Es
sollte folgende Ausgabe erscheinen:

```text

RESULT1
[{"DisplayMonth":"AH","Tests":1},{"DisplayMonth":"BH","Tests":2},{"DisplayMonth":"GC","Tests":3},{"DisplayMonth":"GRJ","Tests":2},{"DisplayMonth":"HAF","Tests":1},{"DisplayMonth":"HIK","Tests":1},{"DisplayMonth":"HW","Tests":1},{"DisplayMonth":"KSR","Tests":1},{"DisplayMonth":"KUE","Tests":1},{"DisplayMonth":"NAI","Tests":2},{"DisplayMonth":"PC","Tests":2},{"DisplayMonth":"PUA","Tests":1},{"DisplayMonth":"RA","Tests":1},{"DisplayMonth":"SH","Tests":1},{"DisplayMonth":"SK","Tests":1},{"DisplayMonth":"SO","Tests":2},{"DisplayMonth":"SWH","Tests":1},{"DisplayMonth":"SZ","Tests":1},{"DisplayMonth":"TT","Tests":1}]

RESULT2
[{"Day":4,"Hour":3,"ClassCount":69},{"Day":2,"Hour":4,"ClassCount":68},{"Day":3,"Hour":4,"ClassCount":67},{"Day":2,"Hour":3,"ClassCount":67},{"Day":3,"Hour":3,"ClassCount":67}]
RESULT3
[{"Department":"AIF","KlassenCount":12},{"Department":"BIF","KlassenCount":9},{"Department":"BKU","KlassenCount":2},{"Department":"CIF","KlassenCount":6},{"Department":"CMN","KlassenCount":6},{"Department":"FIT","KlassenCount":7},{"Department":"HBG","KlassenCount":9},{"Department":"HIF","KlassenCount":23},{"Department":"HKU","KlassenCount":5},{"Department":"HMN","KlassenCount":10},{"Department":"HWI","KlassenCount":11},{"Department":"KIF","KlassenCount":6},{"Department":"KKU","KlassenCount":4},{"Department":"O","KlassenCount":1},{"Department":"VIF","KlassenCount":5}]

RESULT4
[{"Department":"AIF","KlassenCount":12},{"Department":"HIF","KlassenCount":23},{"Department":"HWI","KlassenCount":11}]

RESULT5
[{"TE_Teacher":"PC","TE_Subject":"AMx","LastTest":"2020-04-01T00:00:00"},{"TE_Teacher":"PC","TE_Subject":"AMy","LastTest":"2020-04-01T00:00:00"},{"TE_Teacher":"GRJ","TE_Subject":"BAP","LastTest":"2019-11-11T00:00:00"},{"TE_Teacher":"HAF","TE_Subject":"BAP","LastTest":"2020-06-24T00:00:00"},{"TE_Teacher":"GC","TE_Subject":"BWM1","LastTest":"2020-01-08T00:00:00"},{"TE_Teacher":"GC","TE_Subject":"BWM2","LastTest":"2019-09-05T00:00:00"},{"TE_Teacher":"GC","TE_Subject":"BWM3","LastTest":"2020-06-14T00:00:00"},{"TE_Teacher":"HIK","TE_Subject":"DBI1","LastTest":"2020-02-15T00:00:00"},{"TE_Teacher":"RA","TE_Subject":"DBI1","LastTest":"2019-12-26T00:00:00"},{"TE_Teacher":"NAI","TE_Subject":"Dx","LastTest":"2019-11-24T00:00:00"},{"TE_Teacher":"NAI","TE_Subject":"Dy","LastTest":"2020-04-24T00:00:00"},{"TE_Teacher":"SO","TE_Subject":"E1x","LastTest":"2020-01-01T00:00:00"},{"TE_Teacher":"SO","TE_Subject":"E1y","LastTest":"2020-04-08T00:00:00"},{"TE_Teacher":"KSR","TE_Subject":"GAD","LastTest":"2019-09-29T00:00:00"},{"TE_Teacher":"SK","TE_Subject":"GAD","LastTest":"2019-12-04T00:00:00"},{"TE_Teacher":"HW","TE_Subject":"IOT","LastTest":"2019-12-13T00:00:00"},{"TE_Teacher":"PUA","TE_Subject":"IOT","LastTest":"2019-11-03T00:00:00"},{"TE_Teacher":"BH","TE_Subject":"NVS1","LastTest":"2020-07-02T00:00:00"},{"TE_Teacher":"SWH","TE_Subject":"NVS1","LastTest":"2020-01-16T00:00:00"},{"TE_Teacher":"AH","TE_Subject":"OPS","LastTest":"2020-04-30T00:00:00"},{"TE_Teacher":"BH","TE_Subject":"OPS","LastTest":"2020-05-26T00:00:00"},{"TE_Teacher":"GRJ","TE_Subject":"POS1","LastTest":"2020-02-15T00:00:00"},{"TE_Teacher":"SZ","TE_Subject":"POS1","LastTest":"2020-02-22T00:00:00"},{"TE_Teacher":"KUE","TE_Subject":"PRE","LastTest":"2019-09-30T00:00:00"},{"TE_Teacher":"SH","TE_Subject":"PRE","LastTest":"2020-04-07T00:00:00"},{"TE_Teacher":"TT","TE_Subject":"PRE","LastTest":"2019-12-21T00:00:00"}]

RESULT6
[{"TeacherId":"AF","Subjects":[{"L_Subject":"RISL"}]},{"TeacherId":"AGU","Subjects":[{"L_Subject":"AP4"},{"L_Subject":"WPT_4"}]},{"TeacherId":"AH","Subjects":[{"L_Subject":"NVS1"},{"L_Subject":"OPS"}]},{"TeacherId":"AMA","Subjects":[{"L_Subject":"NWT1"},{"L_Subject":"NVS1"},{"L_Subject":"NVS1x"},{"L_Subject":"NWT_1x"},{"L_Subject":"AP4"},{"L_Subject":"NWT_1y"},{"L_Subject":"NVS1y"},{"L_Subject":"ITPR"},{"L_Subject":"NWT_4A"}]},{"TeacherId":"AT","Subjects":[{"L_Subject":"Dx"},{"L_Subject":"RE"},{"L_Subject":"D"}]},{"TeacherId":"BAE","Subjects":[{"L_Subject":"KGKP"},{"L_Subject":"MTKG"},{"L_Subject":"EWD"}]},{"TeacherId":"BAM","Subjects":[{"L_Subject":"POS1"},{"L_Subject":"DBI1"}]},{"TeacherId":"BAN","Subjects":[{"L_Subject":"DBI1y"},{"L_Subject":"DBI2x"},{"L_Subject":"DBI1"}]},{"TeacherId":"BAR","Subjects":[{"L_Subject":"BMG2"},{"L_Subject":"SOPK"},{"L_Subject":"FMGTK"}]},{"TeacherId":"BEC","Subjects":[{"L_Subject":"MPAN"},{"L_Subject":"MGAN"},{"L_Subject":"SOPK"}]}]
```
