# Scuffolding

## Ein Model erstellen

Das Domain Object Model erstellen wir wie gewohnt mittels DB-First über das Scuffold Commando im Pckage Manager:

```Powersehll
Scaffold-DbContext "Data Source=WE20W8WS2000501\SQLEXPRESS;Initial Catalog=TestsAdministrator;Integrated Security=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -UseDatabaseNames -Force -DataAnnotations
```

Alternativ kann natürlich auch ein Code-First Ansatz verwendet werden.

<https://github.com/schrutek/SPG_POS/tree/master/CSharp/REST/07_CodeFirst>
