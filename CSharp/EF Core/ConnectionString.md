# ConnectionString in die Config bringen

Wichtig!

Der Connection-String wird im der Context-Klasse erstellt. Er muss von dort entdernt werden und im Config-File eingetragen werden. Keinesfalls darf ein Connection-String, der Username und Kennwort enthält auf GIT hochgeladen werden.

Diesen Abschnitt löschen

```C#
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    if (!optionsBuilder.IsConfigured)
    {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        optionsBuilder.UseSqlServer("Data Source=WE20W8WS2000501\\SQLEXPRESS;Initial Catalog=TicketShop;Integrated Security=True;");
    }
}
```

Nun eine Extension-Klasse mit folgenden Inhalt erstellen:

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

Anschließend das Config-File erweitern ``appsettings.json``.

```PowerShell
{
  "AppSettings": {
    "Database": "Data Source=.\\SQLEXPRESS;Initial Catalog=TicketShop;Integrated Security=True;"
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

Zuletzt die Extension-Method in der ``startup.cs`` verwenden:

```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    services.ConfigureMsSql($"{Configuration["AppSettings:Database"]}");
}
```
