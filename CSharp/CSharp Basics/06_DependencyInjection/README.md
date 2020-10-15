# Dependency Injection

Eine praktische Anwendung von Interfaces findet sich in der Technik der *Dependency Injection*.

Stellen wir uns eine Applikation vor, die Daten aus einer Datenquelle holt. Diese Applikation ist aus Architekturgründen in mehrere Layer unterteilt. Ein Client steht ganz oben. Dieser Instanziert einen Service, der vielleicht etwas Ablauflogik enthält. Der Service wiederum instanziert eine Data Access-Klasse, die dann die Daten aus der Datenquelle ließt.

Diese Konfiguratzion ist zwar sehr übersichtlich und aus technischer Sicht auch gut, aber sehr starr, da in jedem Layer, Klassen des darunterliegenden Layers instanziert werden.

Abhilfe schafft hier Dependency-Injection

Das folgene Beispiel ist eine Applikation, die in 3 Layer unterteilt ist. Sie gibt letztlich nur einen String in der Konsiole aus.

* Program: Der Client der letztlich den String in die Konsole schreibt.
* Service: Ein Service, der normalerweise Businessentscheidungen trifft (in unserem einfachen Fall aber nicht)
* DataAccess: Hier weden die Daten aus einer Datenquelle gelesen. In unserem einfachen Fall wird nur ein String retuniert.

## Starke Kopplung

Zuerst die "klassische" starke Kopplung. Das funktioniert so natürlich, hat aber einen Nachteil. Wollen wir Daten aus einer anderen Datenquelle anfordern, müssen wir im Service eine Instanz der anderen Data Layer-Klasse erstellen. Wir müssen also einen Service ändern, den wir nicht unbedingt ändern wollen.

Interface:

```C#
public interface ICustomerDataAccess
{
    string GetCustomerName(int id);
}
```

Data Access Layer:

```C#
public class CustomerDataAccess : ICustomerDataAccess
{
    public string GetCustomerName(int id)
    {
        return $"Dummy Customer Name with id {id}";
    }
}
```

Service-Layer:

```C#
public class CustomerService
{
    private ICustomerDataAccess _customerDataAccess;

    public CustomerService()
    {
        _customerDataAccess = new CustomerDataAccess();
    }

    public string GetCustomerName(int id)
    {
        return _customerDataAccess.GetCustomerName(id);
    }
}
```

```C#
public class Program
{
    CustomerService _cusomerService;

    public Program()
    {
        _cusomerService = new CustomerService();
    }

    public void ProcessCustomerName()
    {
        Console.WriteLine(_cusomerService.GetCustomerName(12));
    }

    public static void Main(string[] args)
    {
        Program program = new Program();
        program.ProcessCustomerName();
    }
}
```

## Lose Kopplung

Besser ist es dem Service gleich die Instanzen mitzugeben (einzuimpfen) die er letztlich benutzen soll. So kann ausschließlich der CLient entscheiden, welche Data Access-Klasse zu verwenden ist.

Die Lösung wird im Unterricht behandelt. Ich bitte um daher um gute Mitarbeit!

## Übung

* Erstelle eine Blank Solution mit dem Namen `Spg.DependencyInjection`
* Erstelle eine Console-App im Unerverzeichnis `src`
* Erstelle 4 Namespaces innerhalb der Console-App (Interfaces, Services, DataAccess, Model)
* Erstelle im Namespace `Model` eine Klasse `Product`. Sie soll mindestens 4 Properties beinhalten, sie sind frei wählbar.
* Erstelle im Namespace `Interfaces` das Interface `IProductDataAccess`.
* Das Interface soll folgende Methode beinhalten: `Product GetProduct(long id);`
* Erstelle im Namespace DataAccess, die Klassen `ProductDataAccess` und `ProductDataAccessMock`. Beide Klassen sollen das Interface implementieren.
* Erstelle im Namespace `Services` eine Klasse `ProductService`. Sie soll eine Methode enthalten, welche die Methode in der Data Access-Klasse aufruft und ein Prduct zurückgiebt. Die Service-Klasse soll dabei Dependency-Injection verwenden, also es soll keine Instanz der Data Access-Klasse in der Service-Klasse erstellt werden.
* Implementiere in der `Proram.cs` eine Methode, die den Service verwendet um eine Instanz von Product zu erhalten. Die Inhalte von Product sind frei wählbar. Achtung! Verwende hier Dependency Injection. Die Klasse `Program` soll einen KOnstruktor enthalten, der eine Instanz der Service-Klasse enthält. Dabei soll der Service-Klasse die zu verwendende Data Access-Klasse injeziert werden. Ob die data-Access-Klasse, oder die Mock-Data-Access-Klasse verwendet wird ist frei wählbar.

Challange 1:

* Kopiere das Ergebnis und ändere die kopierte Solution folgendermaßen:
* Versuche nun statt der Constructor Injection, Property Injection zu verwenden. Bei der Property Injection wird die Instanz nicht über den Konstruktor, sondern über ein Property in der Service-Klasse, an die Service-Klasse übergeben.

Challange 2:

* Kopiere das erste Ergebnis und ändere die kopierte Solution folgendermaßen:
* Versuche nun statt der Constructor Injection, Method Injection zu verwenden. Bei der Method Injection wird die Instanz nicht über den Konstruktor, sondern über eine Methode in der Service-Klasse, an die Service-Klasse übergeben.

