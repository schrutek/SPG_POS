# Logging für EF-Core aktivieren

In dieser Solution wurde die Ausgabe der SQL Statements aktiviert. Möchte man in anderen Projekten diese Statements ausgeben, so muss das Paket `Microsoft.Extensions.Logging.Console` installiert werden:

Dafür muss folgendes NuGet-Package installiert werden:

```PowerShell
Microsoft.Extensions.Logging.Console
```

Danach wird in der Klasse AirQualityContext eine statische Membervariable (kein Property!) `MyLoggerFactory` angelegt. Static ist wichtig, da sonst bei jeder Abfrage eine neue Instanz erzeugt wird.

```C#
public static readonly ILoggerFactory MyLoggerFactory
    = LoggerFactory.Create(builder => { builder.AddConsole(); });
```

Zum Schluss wird in der Methode OnConfiguring() der Logger registriert

```C#
public static void ConfigureMsSql(this IServiceCollection services, string connectionString)
{
    services.AddDbContext<SchoolContext>(optionsBuilder =>
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlite(connectionString);
        }
    });
}
```