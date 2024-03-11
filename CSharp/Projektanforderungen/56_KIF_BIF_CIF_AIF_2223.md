# Anforderungen Projekt POS SS 22/23

## Midenstanforderungen für ein Genügend (4):

### Database:

* Simpel, aber mind. 5 Tabellen inkl. Lookup-Tables

### Domain Model (WS):

* Entities
  * sichere Listen
  * Konstruktoren
  * Relations / Navigations
  * Teileweise Logik (TDD) (Produkt in den Warenkorb legen, prüft: Produkt-Stückzahl, ob Produkt schon im Warenkorb ist , dekrementiert Produkt-Stückzahl, ...)
  * Unite Tests (TDD)
    * Create_Success_Test (Eine Entity adden)
    * Relations / Navigations
* Enums
* Lookup-Tables
* Value Objects

### Repository (WS):

* generisches Repo, und/oder spezialisierte Repos

### Service (SS):

* mind. 2 Services
* Get-Methode(n) min. eine Methode, zur Auflistung der Daten aus einer Tabelle
* Create/Update/Delete-Methoden
  * Create: mind. 5 Bedingungen (LinQ) prüfen (Bedingungen sollen prüfen, ob ein Datensatz angelegt werden darf)
  * Update: mind. 2 Bedingungen (LinQ) prüfen (Bedingungen sollen prüfen, ob ein Datensatz geändert werden darf)
* Filtering / Sorting / Paging implementieren
* C.R.U.D. + Fluent-API oder Mediator + C.Q.S. oder etwas anderes

### Presentation (API-Controller) (SS):

* mind. 2 Controller
* Alle Service Methoden auch in (mehreren) Controllern abbilden
* In beiden Controllern C.R.U.D. inkl. sauberes und korrektes API-Design
* Alle Methoden verfügen über sauberes Exception Handling

### Presentation (Server Side Rendering) (SS):

* --

### GIT (immer):

* Abgabe auf GIT + Präsentation. Möglichst viele pushes über den Zeitraum verteilt.

### Unit Tests (immer):

* Domain Model: pro Entity mind. 1 Success_Test
* Repository: pro Methode mind. 1 Success_Test
* Services: Alle Create/Update/Delete-Method Test Driven (TDD) implementieren (100%ige Code-Abdeckung), ohne Mocking

---

## Implementierungsvorschläge für 3 bis 1:

* Sauberes und durchgängiges Exception Handling
* Implementierung von Builder-Pattern für das Repository (Lesend).
* Implementierung von Builder-Pattern für das Repository (Schreibend).
* Implementierung von Fluent-API-Pattern für das Repository.
* Implementierung von Fluent-API-Pattern für das Domain Model.
* Implementierung von Datenvalidierung für das Domain Model.
* Ein simpler, rudimentärer LogIn/LogOut
* Filterung nach einem Freitextfeld, einem Datumsraum. Jedes davon optional.
* Volltextsuche mit Fehlerkorrektur in einer Tabelle, z.B. Namen
* Erhöhung der Anzahl der Prüfungen in den Service-Methoden (Add/Update/Delete)
* DTO Mapping
* Ein Service benutzt einen anderen Service zur Datenvalidierung (z.B. StockService prüft Lagerbestände)
* Über die Angabe hinaus, eine weiterer Service mit Add- Update- Delete-Methode
* Umsetzung mittels C.Q.R.S. und MediatR (C.Q.R.S.- und Mediator-Pattern)
* Mocking: MoQ wird für TDD verwendet
* TDD für alle Service Methoden
* Alle Layer außer (EF Core) werden durcj Mocks ersetzt.
* GIT: Arbeiten mit Feature-Branches, 1 Branch pro Feature + Merge auf den Main-Branch

Ein Punktespiegel für ene Note besser als 4, +/- ein Notengrad. Code Quality (Optik und Sauberkeit) spielt ebenfalls eine Rolle, ist aber schwer monetär messbar.

## Bewertungskriterien:

* Domain Model inkl. Infrastructure (siehe Anforderungen oben)
* Domain Model Tests
  * Codeabdeckung
* Repositories (siehe Anforderungen oben)
* Repositories Tests
  * TDD Codeabdeckung
* Application (siehe Anforderungen oben)
* Application Tests
  * TDD Codeabdeckung
* Presentation (siehe Anforderungen oben)
* Presentation Tests
  * Integration Tests, idealerweise TDD, Codeabdeckung
* Git-Performance
  * Häufigkeit und Abstand der einzelnen Pushes
  * Anzahl der Branches
  * Verwendung von Merge, Rebase, Squash, ...
* Code Cquality:
  * Keine auskommentierten Codeteile
  * Einhaltung der Coding Conventions
  * Einrückungen, unnötige Leerzeilen, ...
  * *How smells your code*


| Note | Punkteanzahl |
|---|---|
| 3 | > 3 |
| 2 | > 6 |
| 1 | > 9 |
