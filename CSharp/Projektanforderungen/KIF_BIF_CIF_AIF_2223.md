# Anforderungen Projekt POS SS 22/23

## Midenstanforderungen für ein Genügend (4):

### DB:
* Simpel, aber mind. 5 Tabellen inkl. Lookup-Tables

### Service:
* mind. 2 Services
* Get-Methode(n) min. eine Methode, zur Auflistung der Daten aus einer Tabelle
* Create/Update/Delete-Methoden
  * Create: mind. 5 Bedingungen (LinQ) prüfen (Bedingungen sollen prüfen, ob ein Datensatz angelegt werden darf)
  * Update: mind. 2 Bedingungen (LinQ) prüfen (Bedingungen sollen prüfen, ob ein Datensatz geändert werden darf)
* Filtering / Sorting / Paging implementieren
* Umsetzung mittels C.R.U.D.

### Unit Tests:
* mind. 5 Entities
* mind. 1 Value-Object
* mind. 2 1..n-Relationen
* Domain Model: pro Entity mind. 1 Success_Test
* Services: Alle Create/Update/Delete-Method Test Driven (TDD) implementieren (100%ige Code-Abdeckung), ohne Mocking

### Repository:
* generisches Repo, und/oder spezialisierte Repos

### Controller:
* mind. 2
* Alle Service Methoden auch in (mehreren) Controllern abbilden

### UI:
(comming soon)

### GIT:
Abgabe auf GIT + Präsentation. Möglichst viele pushes über den Zeitraum verteilt

---

## Implementierungsvorschläge für 3 bis 1:

* Sauberes und durchgängiges Exception Handling
* Ein simpler, rudimentärer LogIn/LogOut
* Filterung nach einem Freitextfeld, einem Datumsraum UND einer Drop-Down-Box. Jedes davon optional
* Volltextsuche mit Fehlerkorrektur in einer Tabelle, z.B. Namen
* Erhöhung der Anzahl der Prüfungen in den Service-Methoden (Add/Update/Delete)
* DTO Mapping
* Ein Service benutzt einen anderen Sevice zur Datenvalidierung (z.B. StockService prüft Lagerbestände)
* Über die Angabe hinaus, eine weiterer Service mit Add- Update- Delete-Methode
* Umsetzung mittekls C.Q.R.S. und MediatR (C.Q.R.S.- und Mediator-Pattern)
* Mocking: MoQ wird für TDD verwendet
* GIT: Arbeiten mit Feature-Branches, idealerweise 1 Branch pro Feature + Merge auf den Main-Branch

Ein Punktesoiegel für ene Note besser als 4, +/- ein Notengrad. Code Quality (Optik und Sauberkeit) spielt ebenfalls eine Rolle, ist aber schwer monetär messbar.

| Note | Punkteanzahl |
|---|---|
| 3 | > 3 |
| 2 | > 6 |
| 1 | > 9 |