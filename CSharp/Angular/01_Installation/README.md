# Installationen

Wichtig!! Hier geht es um Angular, nicht um Angular.js. Angular ist der Nachfolger von Angular.js. Angular ist eine komplette Neuentwicklung des Frameworks.

## Benötigte Tools installieren

Als erstes müssen 3 Softwareprodukte installiert werden:

1. IDE: Visual Studio Code (https://code.visualstudio.com/download) (Empfehlung! natürlich ist auch jeder andere Editor/IDE geeignet)
2. node.js (https://nodejs.org/en/)

## VS Code

Visual Studio Code ist ein besserer Texteditor. Es hat also mehr mit Notepad zu tun, als mit Visual Studio, trotz der Namensähnlichkeit. Dennoch ist es ein sehr mächtiger Texteditor. Er verfügt über eine Vielzahl an Extensions, mit denen man VS Code erweitern kann. Man kann in VS Code genauso gut C# schreiben, wie Java, Python oder Mark Down, wenn man die entsprechenden Extensions installiert. Das hat jedoch den Preis, das man sich von gewohnten "Visual"-Features, also der Bedienung über Menüs und Buttons und Icons, allerdings verabschieden muss. Es wird hauptsächlich über Shortcuts und der Power Shell gesteuert. Jede Extension hat ihre eigenen Shortcuts und Funktionen. Man muss die jeweiligen Dokumentationen gut durchlesen. Das ist zugegebenermaßen, etwas gewöhnungsbedürftig.

Die Funktionen im einzelnen werde ich hier nicht erläutern, dazu gibt es massenhaft Tutorials im Netz.

## Angular selbst

Angular ist eine Script-Library, die als Sprache Type Script verwendet. Java Script ist aber ebenfalls möglich, da ja Tpe Script zu Java Script transpiliert wird.

Man legt sein Projekt in einem Verzeichnis auf der Festplatt an. Z.B.: `C:\Projects\Angular\Test_1`. In dem angelegten Verzeichnis die Power Shell öffnen. Es eignet sich parallel zur IDE die Power Shell zu öffnen, da Angular über die Node.js-CLI gesteuert wird. Die Kommandos beginnen mit `ng ...` (z.B.: `ng new MyProject` um ein neues Projekt anzulegen, oder `ng serve` um es ausführen).

## Angular installieren

Node.js ist ein Package Manager für Java Script. Damit können wir nun Angular installieren:

```powershell
npm install -g @angular/cli
```

Hat man die Angular bereits installiert, sollte man auf die aktuelle Version prüfen (Details dazu:  https://github.com/angular/angular-cli). Seit Beginn von Angular hat sich darin sehr viel verändert.

## Grundlegendes zu diesem Beispiel

Unser Projekt wird aus einem Angular Front End und einer REST-Api bestehen. In diesem Tutorial wird erklärt, wie man eine Angular Applikation aufbaut. Die REST-Api wird nicht beleuchtet.

### Projekt anlegen

```powershell
ng new School2000
```

Man bekommt nun 2 Fragen gestellt. Angular Routing wollen wir nicht, also die erste Frage mit NO quittieren und als Styling wählen wir CSS.

## Projekt ausführen

Möchte man eine Angular-Applikation ausführen muss man diese erst kompilieren. Das Kommando dafür ist `ng serve`. Um sie auch ansehen zu können, im Browser die URL `http://localhost:4200` eingeben. Jede Angular Applikation ist standardmäßig im developement-Modus auf dem Port 4200 erreichbar.

Das Kommando `ng build` kompiliert die App als Release.
