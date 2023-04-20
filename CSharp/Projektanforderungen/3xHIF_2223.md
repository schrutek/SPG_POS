# Projetanforderungen

## Mindestanforderungen für 4

### Domain Model

* mind. 4 Entities Achtung: Denke an richtige Konstruktoren, 
* mind. 2 1..n Relationen (List) Achtung: Denke an sichere Listen, backing Fields
* mind. eine Enumeration
* mind. 1 Value Object
* vollständiiger DB-Context
* Datenbank muss nach dem Code First Ansatz erstellt werden können
* DB-Seeding mit Dummy-Daten

### ViewModel

* Ein ViewModel muss implementiert werden (Voraussetzung für 3 oder besser)
* IPropertyNotifyChanged muss implementiert werden
* Clickt man auf einen Eintrag in einer Liste, soll ein anderes beliebiges Anzeigeelement mit Daten befülllt werden.

### XAML

* mind. 2 Listen. Klickt man auf eine Liste, werden wetere Detaildaten in der nächsten Liste angezeigt (wie im Unterricxht besprochen)
* Die Detaildaten eines selektierten Datensatzes einer Tabelle in Form eines Eingabeformulares anzeigen.
* Die Daten im Eingabeformular sollen verändert werden können
* Eine *Update*-Funktion zum Aktualisieren der geänderten Eingaben muss implementiert werden. (Command-Pattern)

## Weitere Funktionen 3 - 1:

* größere Datenbank
* mehr Entit#ten
* n..n Relation
* Listen sind "hübsch" formatiert (Icons, mehrere Infos pro Eintrag, Fett, Kursiv, Schriftgröße, ...)
* Sauneres Excpetion Handlicg in den Methoden im View Model
* Validierung der EIngabedaten wenn man einen Datenstz ändert oder neu anlegt + entsprechende Fehlermeldung(en) im UI.
* rudimentäres aber sauberes State-Management. (z.B. wird ein Datensatz hinzugefügt, werden alle beteiligten Elemente (Listen) aktualisiert und die Anzhal der Datensätze im UI angezeigt)
* Filter- / Sortier- / oder Suchfunktion um eines der Listenelemente einzuschränken
* Eine *Create*-Funktion zum Erstellen eines neuen Datensatzes muss implementiert werden (Command-Pattern)
* Eine *Delete*-Funktion zum Löschen eines  Datensatzes muss implementiert werden (Command-Pattern)
* Verwendung weiterer Anzeigelemente neben der Liste, Drop-DownBox, Button, TextBox (z.B. RadioButton, CheckBox, ...)
* Clickt man auf **Neu**, gehtz eine weitere Form (ein eigenes Windows-Fenster) mit entsprechenden Eingafeldern (z:B. TextBox) auf.
* Verwendung eines klassischen Hauptmenüs am oberen Fensterrand.
* ...