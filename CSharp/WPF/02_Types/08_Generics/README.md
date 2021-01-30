# Generics und Contraints

Generics sind die generelle Form, also nicht die spezifische Form eines Datentypes. C# erlaubt es uns classes, interfaces, abstract classes, fields, methods, static methods, properties, events, delegates, und operators geneerisch zu definieren. Dafür wird der Type-Parameter verwendet. Er ist ein Platzhalter für den konkreten Typ, der beim Instanzieren der Klasse bekannt wird.

```C#
class DataStore<T>
{
    public T Data { get; set; }
}
```

Oder wenn mehrere Type-Parameter benötigt werden:

```C#
class KeyValuePair<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
}
```

## Ein Beispiel

Wir erstellen für den Anfang 2 Klassen:

```C#
public class Pupil : EntityBase
{
    public string FirstName { get; set; }

    public string LastName { get; set; }
}
```

```C#
public class SchoolClass : EntityBase
{
    public string RoomNumber { get; set; }
}
```

Wie man sieht leiten beide Klassen von einer Basisklasse ab. Die Bassklasse beinhaltet eine Id und diese Id soll in den Klassen Person und Event zugreifbar sein, daher diese Vererbungshierarchie.

Zusätzlich wird ein Typ an die Basisklasse übergeben. Die Basisklasse ist also generisch. Jenachdem welchen Typ wir in die Basisklasse übergeben, ist dieser Typ mit allen Properties und Methoden in der Basisklasse verfügbar.

```C#
public class EntityBase
{
    public int Id { get; set; }
}
```

Wir wollen mit den Entitäten Daten aus einer Datenquelle (z.B. List) lesen. Dazu benötigen wir eine Datenquelle:

```C#
public class MockDatabase
{
    private List<SchoolClass> _schoolclasses = new List<SchoolClass>()
    {
        new SchoolClass(){Id=1, RoomNumber="C.3.07" },
        new SchoolClass(){Id=2, RoomNumber="C.4.08" },
        new SchoolClass(){Id=3, RoomNumber="C.3.12" },
    };

    private List<Pupil> _pupils = new List<Pupil>()
    {
        new Pupil(){ Id=1, FirstName="Vorname 1", LastName="Nachname 1" },
        new Pupil(){ Id=2, FirstName="Vorname 2", LastName="Nachname 2" },
        new Pupil(){ Id=3, FirstName="Vorname 3", LastName="Nachname 3" },
        new Pupil(){ Id=4, FirstName="Vorname 4", LastName="Nachname 4" },
        new Pupil(){ Id=5, FirstName="Vorname 5", LastName="Nachname 5" },
    };

    public List<SchoolClass> ListSchoolClasses()
    {
        return _schoolclasses;
    }

    public List<Pupil> ListPupils()
    {
        return _pupils;
    }
}
```

Nun erstellen wir für die Entitäten je ein Repository:

```C#
public class PupilRepository
{
    private List<Pupil> _dbSet;

    public PupilRepository()
    {
        _dbSet = new MockDatabase().ListPupils();
    }

    public Pupil GetById(int id)
    {
        foreach (Pupil item in _dbSet)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        throw new KeyNotFoundException("Entität nicht gefunden!");
    }
}
```

```C#
public class SchoolClassRepository
{
    private List<SchoolClass> _dbSet;

    public SchoolClassRepository()
    {
        _dbSet = new MockDatabase().ListSchoolClasses();
    }

    public SchoolClass GetById(int id)
    {
        foreach (SchoolClass item in _dbSet)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        throw new KeyNotFoundException("Entität nicht gefunden!");
    }
}
```

Besteht eine Applikation aus 250 Entitäten müssten wir auch 250 Repositories implementieren.
Besser ist es ein generisches Repository zu programmieren. Dazu müssen wir dem Repository mitteilen, dass es für mehrere Datentypen verwendet werden kann.

```C#
    public class RepositoryBase<T>
        where T : EntityBase, new()
    {
        private List<T> _dbSet;

        public RepositoryBase()
        {
            _dbSet = (List<T>)new MockDatabase().GetDbSet<T>();
        }

        public T GetById(int id)
        {
            foreach (T item in _dbSet)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            throw new KeyNotFoundException("Entität nicht gefunden!");
        }

        public T Create()
        {
            return new T() { Id = 99 };
        }
    }
```

Die Klasse `RepositoryBase` erhlt nun in spitzen Klammern eine Typangabe. Der Typ ist hier unbekannt. Es kann jeder belibige Referenztyp sein. Weil wir den Typ an dieser Stelle nicht kennen, könnten wir in der Methode `GetById` auch nicht auf das Id-Property des Types zugreifen, weil object den ja nicht zur Verfügung stellt.

## Constraint

Wir erstellen also direkt darunter ein Constraint `where` mit dem wir festlegen, dass der Typ ``T`` unbedingt von `EntityBase`  erben muss. Jetzt kennt der Typ `T` das property `Id`, weil es in `EntityBase` ja vorhanden ist. Wir können das Repository nun nur noch auf jene Typen festlegen, die von `EntityBase` ableiten. Das mag eine Einschränkung sein, aber sie erlaubt uns sicher zu sein, dass der Typ `T` das Property ``Id`` auf jeden fall kennt.

Eine Verwendung der Klassen zum Beispiel in einer Liste würde dann also so aussehen:

```C#
RepositoryBase<SchoolClass> schoolClassRepositoryGeneric = new RepositoryBase<SchoolClass>();
SchoolClass schoolClassGeneric = schoolClassRepositoryGeneric.GetById(2);
Console.WriteLine($"Klasse!  ID: {schoolClassGeneric.Id}, Raum: {schoolClassGeneric.RoomNumber}");

RepositoryBase<Pupil> pupilRepositoryGeneric = new RepositoryBase<Pupil>();
Pupil pupilGeneric = pupilRepositoryGeneric.GetById(3);
Console.WriteLine($"Schüler! ID: {pupilGeneric.Id}, Name: {pupilGeneric.FirstName} {pupilGeneric.LastName}");
```

## Übung

* Erstelle in der vorhandenen Solution 2 neue Klassen ``Ticket`` mit einem Property ``Price`` (Wähle den richtigen Datentyp selbst). Und erstelle ein Property ``ShoppingCartNavigation``. Es soll den ShoppingCart aggregieren (wähle den richtigen Datentyp selbst).
* Erstelle in der vorhandenen Solution 2 neue Klassen ``ShoppingCart`` mit einem Property ``Tickets`` (Wähle den richtigen Datentyp selbst).
* Erstelle eine Basisklasse ``ShopBase``. Beide Klassen sollen von dieser Klasse erben.
* Erstelle ein Interface ``IShopBase`` von dem ``Ticket`` und ``ShoppingCart`` erben.
* Ergänze die Klasse ``ShopBase`` durch das richtige Contraint.
