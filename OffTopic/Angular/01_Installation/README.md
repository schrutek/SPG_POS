# Installationen

Wichtig!! Hier geht es um Angular, nicht um Angular.js. Angular.js ist etwas anderes.

## Benötigte Tools installieren

Als erstes müssen 3 Produkte installiert werden:

1. Visual Studio Code (https://code.visualstudio.com/download)
2. node.js (https://nodejs.org/en/)
3. MSSQL Management Studio (https://www.microsoft.com/de-at/download/details.aspx?id=8961) (die Werbung einfach ignorieren)

## VS Code

Visual Studio Code ist ein besserer Texteditor. Hat also mehr mit Notepad zu tun, als mit Visual Studio, trotz der Namensgleichheit. Dennoch ist es ein sehtr mächtigrer Texteditor. Er verfügt über eine vielzahl an Extensions, mit denen man VS Code erweitern kann. Man kann in VS Code genauso gut C# schreiben, wie Java, Python oder Mark Down, wenn man die entsprechenden Extensions installiert. Das hat jedoch den Preis, das man sich von gewohnten "Visual"-Features, also der Bedienung über Menüs und Buttons und Icons, allerdings verabschieden muss. Es wird hautpsächlich über Shortcuts und der Power Shell gesteuert. Jede Extension hat ihre eigenen Shortcuts und Funktionen. Man muss die jeweiligen Dokumentationen gut durchlwesen. Das ist zugegebenermaßen, etwas gewöhnungsbedürftig.

Die Funktionen im einzelnenwerde ich hier nicht erläutern, dazu gibt es massenhaft Tutorials im Netz.

## Angular selbst

Angular ist eine Script-Library, die als Sprache Type Script verwendet. das ist eine Mischung aus Java Script und C#, sozusagen, Java Script mit C# Syntax. Es ist etwas gewöhnungsbedürftig, amn kommt da aber schnell rein.

Man lwegt sein Projekt in einem Vedrzeichnis auf der Festplatt ab. Z.B.: `C:\Projets\Angular\Test_1`. Im nächsten Kapitel geht es gleich weiter, was man dann in diesem Verzeichnis tut. Als erstes aber, in dem angelegten Verzeichnis gliech die Power Shell öffnen. Ich sags gleich, Angular-Entwicklung geht nicht ohne Power Shell, da alle Kommandos dort eingegeben werden. Die Kommandos beginnen immer mit `ng ...` (z.B.: `ng new MyProject` um ein neues Projekt anzulegen, oder  `ng serve` zum Ausführen).

## Angular installieren

Node.js ist ein Package Manager für java script. Damit können wirnun die Angular CLI installieren :

```powershell
npm install -g @angular/cli
```

Hat man die Angular CLI bereits installiert, sollte man auf die aktuelle Version prüfen (Details dazu:  https://github.com/angular/angular-cli). Seit Begunn von Angular hat sich darin sehr viel verändert.

Wie bereits erwähnt, im Projektordner, die Power Shell am besten als Administrator öffnen.

```powershell
ng new School2000
```

Man bekommt nun 2 Fragen gestellt. angular routing wollen wir nicht, also NO auf die erste Frage und als Styling wählen wir CSS.

## Grundlegendes zu diesem Beispiel

Dieses Projekt besteht aus einer Angular-Applikation und eienr REST-Api. Das ist durchaus eine Konstellation eines verteilten Systems, wie es in der modernen Softwareentwicklung, aktuell, durchaus üblich ist. in diesem Tutorial wird erklärt, wie man eine Angular Applikation aufbaut. Die REST-Api wird aus zeitgründen nicht beleuchtet.

Die Angular Applikation wird durch das KOmmando `ng serve` zu Java Script kompiliert und bereitgestelt. Mit einem beliebigen Browser  über die URL `http://localhost:4200` kann das Ergebnis dargestellt werden.

Die REST-Api ist mit dot-net-core entwickelt. Sie ist sehr simpel und stellt nur einen Controller mit wenigen Funktionen bereit. Die Solution kann im herkömmlichen, bekannten Visual Studio geöffnet und ausgeführt werden. Wird sie ausgeführt, sieht man nur ein Konsolen-Fenster mit einigen Infos darin.

## Microsoft SQL-Server

Anders als bisher, wird in diesem Projekt in der REST-Api nicht doe Local-DB verwendet, sondern der MS SQL-Server in der Express Version. Diese sollte mit Visual Studio bereits installiert sein. Wenn nicht: https://www.microsoft.com/de-de/sql-server/sql-server-downloads# (ein bisschen nach unten scrollen zu "Oder laden Sie eine kostenlose spezielle Edition herunter" und Express wählen!)

**!!WICHTIG!!**

Die REST-Api benutzt eine MS-SQL-Server Datenbank. Die MSSQL-Express Edition muss also installiert werden. Darin kann man dann die Datenbank einfach importieren.

Mit der rechten maustaste auf `Databases` und im Kontextmenü die Option `Restore Datatabase...` und dann das `.bak`-File auswählen. Bitte auf die aktuelle Version prüfen.
