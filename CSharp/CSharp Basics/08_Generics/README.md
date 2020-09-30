# Generics und Contraints

Folgender Inhalt von ``Program.cs`` ist vorgegeben:

```C#
TeacherList techers = new TeacherList()
{
    new Teacher(){ FirstName="Martin", LastName="Bauer", Income=1230.45M, Hours=12 },
    new Teacher(){ FirstName="Bob", LastName="Meier", Income=789.15M, Hours=26 },
    new Teacher(){ FirstName="Thomas", LastName="Müller", Income=1952.14M, Hours=10 },
    new Teacher(){ FirstName="Michi", LastName="Mayer", Income=93.25M, Hours=6 },
    new Teacher(){ FirstName="Doris", LastName="Hofer", Income=257.45M, Hours=29 },
    new Teacher(){ FirstName="Irene", LastName="Bauer", Income=126.96M, Hours=21 },
    new Teacher(){ FirstName="Sandra", LastName="Wolf", Income=6541.55M, Hours=23 },
    new Teacher(){ FirstName="Sonja", LastName="Simon", Income=364.70M, Hours=22 },
    new Teacher(){ FirstName="Gerhard", LastName="Berger", Income=1347.69M, Hours=14 }
};

PupilList pupils = new PupilList()
{
    new Pupil(){ Name="Name 1", Hours=36 },
    new Pupil(){ Name="Name 2", Hours=34 },
    new Pupil(){ Name="Name 3", Hours=38 },
    new Pupil(){ Name="Name 4", Hours=32 },
};

Console.WriteLine($"Summe aller Gehälter: {techers.Sum()}");
Console.WriteLine($"Summe aller Tickets:  {pupils.Sum()}");
```

Erstellen wir nun 2 Klassen

* ``Teacher`` (Properties: FirstName, LastName, Income, Hours)
* ``Pupil`` (Name, Hours)

## Anforderung

Wir wollen eine List-Klasse implementieren, die eine Methode zur Verfügung stellt, die uns die Arbeitsstunden (Hours) aufsummiert. Die List-Klasse aus dem Framework kann das nicht. Also müssen wir eine neue List-Klasse implementieren, die von ``List`` erbt.

Arbeiten wir nicht generisch, müssen wir 2 List-Klassen erstellen. Eine für ``Teacher`` und eine für ``Pupil``.

```C#
    public class TeacherList : List<Teacher>
    {
        public decimal Sum()
        {
            decimal sum = 0;
            foreach (Teacher item in this)
            {
                sum = sum + item.Hours;
            }
            return sum;
        }
    }
```

```C#
    public class PupilList : List<Pupil>
    {
        public int Sum()
        {
            int sum = 0;
            foreach (Pupil item in this)
            {
                sum = sum + item.Hours;
            }
            return sum;
        }
    }
```

Das erzeugt recht viel "Boiler Plate Code". Das geht mittels Generics besser.

## Genersiche Lösung mit Constraint

Wenn wir generisch arbeiten, benötigen wir nur eine Liste, die auf den jeweiligen Inhalt typisiert wird. Genau so wie ``List<...>`` selbst aus dem Framework.

```C#
    public class ImprovedList<T> : List<T>
        where T : IPerson, new()
    {
        public decimal Sum()
        {
            decimal sum = 0;
            foreach (T item in this)
            {
                sum = sum + item.Hours;
            }
            return sum;
        }
    }
```

Hier ist zu beachten. Damit man auf Hours zugreifen kann muss der Typ ``T`` von einer Klasse oder einem Interface erben, die/das ``Hours`` zur Verfügung stellt. So kann der Compiler bereits zur Entwurfszeit sicherstellen, dass das Property ``Hours`` im noch unbekannten Typ ``T`` vorhanden sein wird. Das nennt man **Constraint**.

Die ``where``-Signatur enthält also folgende Funktion:

* Der Typ ``T`` leitet vom Interface ``IPerson`` ab. (``T : IPErson``)
* Der Typ ``T`` muss ein konkreter Typ sein (``new()``) Darf also kein Interface oder eine abstrakte Klasse sein. Dieser Konstraint ist in diesem Beispiel nicht unbedingt nötig. Er würde nötig sein, wenn man in der Sum-Methode eine neue Instanz von ``T`` machen wollte, denn Instanzen kann man ja nur von konkreten Typen (Klassen) machen.

Der Compiler kann sich also durch das Constraint darauf verlassen, dass der Typ ``T`` vom Interface ``IPerson`` ableiten wird und das es eine konkrete Instanz sein wird.
