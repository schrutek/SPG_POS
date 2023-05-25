# Projetanforderungen

## Mindestanforderungen für 4

### Domain Model

* mind. 4 Entities Achtung: Denke an richtige Konstruktoren, 
* mind. 2 1..n Relationen (List) Achtung: Denke an sichere Listen, Backing Fields, ...
* mind. eine Enumeration
* mind. ein Value Object
* vollständiger DB-Context
* Datenbank muss nach dem ``Code First`` Ansatz erstellt werden können.
* DB-Seeding mit Dummy-Daten

### ViewModel

* Ein ViewModel muss implementiert werden (Voraussetzung für 3 oder besser)
* ``IPropertyNotifyChanged`` muss implementiert werden
* Clickt man auf einen Eintrag in einer Liste, soll ein anderes beliebiges Anzeigeelement mit Daten befülllt werden.

### XAML

* mind. 2 Listen. Klickt man auf eine Liste, werden weitere Detaildaten in der nächsten Liste angezeigt (wie im Unterricht besprochen)
* Die Detaildaten eines selektierten Datensatzes einer Tabelle, in Form eines Eingabeformulares anzeigen.
* Die Daten im Eingabeformular/Eingabefeldern sollen verändert werden können.
* Eine *Update*-Funktion zum Aktualisieren der geänderten Eingaben (siehe oben) muss implementiert werden. (Command-Pattern)

## Weitere Funktionen 3 - 1:

* größere Datenbank
* mehr Entitäten
* n..n Relation im Domain Model
* Vererbung im Domain Model
* Listen sind "hübsch" formatiert (Icons, mehrere Infos pro Eintrag, Fett, Kursiv, Schriftgröße, Farben, Formen, ...)
* Sauberes Excpetion Handling in den Methoden im View Model.
* Validierung der Eingabedaten wenn man einen Datenstz ändert oder neu anlegt + entsprechende Fehlermeldung(en) im UI (Info-Box, Label, Toastr, ...).
* Rudimentäres aber sauberes State-Management. (z.B. wird ein Datensatz hinzugefügt, werden alle beteiligten Elemente (Listen) aktualisiert und die Anzhal der Datensätze im UI angezeigt)
* Filter- / Sortier- / oder Suchfunktion um eines der Listenelemente einzuschränken.
* Eine *Create*-Funktion zum Erstellen eines neuen Datensatzes muss implementiert werden (Command-Pattern).
* Eine *Delete*-Funktion zum Löschen eines Datensatzes muss implementiert werden (Command-Pattern).
* Verwendung weiterer Anzeigelemente neben der Liste, Drop-DownBox, Button, TextBox (z.B. RadioButton, CheckBox, ...).
* Clickt man auf **Neu**, geht eine weitere Form (ein eigenes Windows-Fenster) mit entsprechenden Eingafeldern (z.B. TextBox) auf.
* Verwendung eines klassischen Hauptmenüs am oberen Fensterrand.
* Side Menu
* ...

## Bewertungskriterien:

* Domain Model inkl. Infrastructure (siehe Anforderungen oben)
* WPF (XAML, ViewModel, oder Alternativen nach Vereinbarung) (siehe Anforderungen oben)
* Git-Performance
  * Häufigkeit und Abstand der einzelnen Pushes
  * Anzahl der Branches
* Code Cquality:
  * Keine auskommentierten Codeteile
  * Einhaltung der Coding Conventions
  * Einrückungen, unnötige Leerzeilen, ...
  * *How smells your code*
