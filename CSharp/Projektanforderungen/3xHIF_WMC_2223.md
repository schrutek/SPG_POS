# Projetanforderungen

## Mindestanforderungen für 4

### Components

* mind. 4 Components
* Jede Komponente sollte über Inhalt im HTML-File verfügen. CSS ist nicht notwendig.
* Im Type-Script File soll wenigstens eine Methode mit simpler Logik implementiert werden. (Etwas wird berechnet, oder ein Icon wird verändert, ...)

### Routing

* mind. 2 Routen zu unterschiedlichen Komponenten müssen implementiert werden
* Eine 404-Route muss vorhanden sein + Komponente
* Eine **-Route inkl. Redirect muss implementiert werden.

### Services

* Es muss mind. ein Service implementiert werden, der Daten zur Verfügung stellt, die in einer oder mehreren Komponenten angezeigt werden
* Die Daten müssen änderbar sein (min. eine Funktion (Create/Update/Delete)) Wir ein Spiel oder etwas ähnliches implementiert, kann dafür z.B. eine High-Score-Funktion herangezogen werden.

### Binding

* Alle 3 Binding-Varianten müssen in mind. einer Komponente umgesetzt werden (Moustache-Binding, Data-Binding, Event-Binding)

### Observables

* Mind. eine Information muss in aus einem Service ausgelesen und in einer Komponente dargestellt werden. Es ist dabei ein Observer zu verwenden, damit die Information automatsch aktualisiert wird.

### Forms

* Es ist mind. ein Formular zu erstellen (wahlweise **Template Forms** oder **reactive Forms**)

### Data

* 

## Weitere Funktionen 3 - 1:

* Datenvalidierungen der Eingaben im Formular
* Mehrere Formulare
* mehrere Komponenten
* sauberes Excpetion Handling
* z.B. Toastr (Node-Package) oder Alert zur Anzeige von Fehlermeldungen.
* "hübsches" Layout
* ein einfaches Eingabe-Modal mit Bootstrap realisiert
