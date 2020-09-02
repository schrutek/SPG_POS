# Aufbau einer Solution

Es hat sich folgender Aufbau einer Solution als sinnvoll und übersichtlich erwiesen. Dabei gibt es eine root-Folder in dem das Solution-File steht. darunter ein Verzeichnis ``src`` in dem die einzelnen Projkete einer Soltuion platz finden. Eine Solution besteht in der Regel aus mehreren Projekten (z.B. einer WPF-Anwendung für das User Interface und mehreren Class Libraries in denen die Business-Logik verbaut ist).

![Directories](Directories.png)

Visual Studio begrüßt uns gleich nach start mit der Option ein neues Projekt anzulegen. Wir entscheiden uns erstmal dagegen.

![VS01](VS01.png)

Aber nun legen wir doch ein neues Projekt an.

![VS02](VS02.png)

Zuerst eine leere Solution erstellen

![VS03](VS03.png)

Nach Konvention ist die Benennung einer Solution ``[Firmenname].[Projektname]``

![VS04](VS04.png)

Anschlie0end  ein Projekt in die Solution erstellen

![VS05](VS05.png)

![VS06](VS06.png)

Nach Konvention ist die Benennung eines Projketes ``[Firmenname].[Projektname].[Projektteilname]``. Ein Projektteilname ist z.B. FrontEnd, Services, BusinessLayer, DataLayer, ...

![VS07](VS07.png)

Das ist nun das Ergebnis

![VS08](VS08.png)
