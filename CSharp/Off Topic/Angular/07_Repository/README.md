# Der Http Client
Eine zatrale Funktion ist es Daten zu beschaffen, die in der Applikation dargestellt werden können.

Der HTTP-Client sendet Request an den Server (in der Regeleine API) und erhält im Response die serialisierten Daten. Nicht das gesam,te HTML, sondern nur die Daten (in der Regel als JSON-Stream). Diee Daten werden Clientweitig deserialisiert.

das ganze benötigt nun folgende Vorbereitungen:

## Environment Files

Das Template hat im Ordner "environments" zwei Files erstellt. `environment.ts` und `environment.prod.ts`. Die Namen sind sprechend. Das eine für die Dev-Umgebung, das andere für die Produktions-Umgebung. Wir verwenden die beiden Files um die Root-URL zur API festzulegen. Zum einen auf die Dev-URL (z.B.: http://localhost/5001) und zum anderen auf die Produktion-URL (z.B.: http://www.school2000.at)

## environment.prod.ts:

```typescript
export const environment = {
  production: false,
  urlAddress: 'http://www.school2000.at'
};
```

## environment.prod.ts:

```typescript
export const environment = {
  production: false,
  urlAddress: 'http://localhost:5000'
};
```

Wir benötigen nun einen weiteren Service:

## Services in Angular
Mit dem CLI-Kommando einen neuen Service `environment`erstellen. Nun um folgende Code-Teil erweitern:

Import nicht vergessen!


```typescript
import { environment } from './../../../environments/environment';
```

Im Service eine public-Variable erstellen, die auf die URL im Environment-File gesetzt wird:

```typescript
public urlAddress: string = environment.urlAddress;
```

Natürlich mus der Service im App-Modul registriert werden:

```typescript
import { EnvironmentUrlService } from './shared/services/environment-url.service'
```

Damit der Service verfügbar wird, muss er im providers-Array von app-module hinzugefügt werden. Jetzt steht er über Dependency Inection der gesamten Applikation zur verfügung.

## Ein Repository erstellen

Wir wollen auf eine API zugreifen, welche Daten liefern wird, die wir anzeigen möchten. Das funktioniert natürlich über http. Dafür benötigen wir ein Modul, das uns http-Actions zur Verfügung stellt. Der folgende import wird ebenfalls im App-Module durchgeführt:

```typescript
import { HttpClientModule } from '@angular/common/http';
```

Der Import muss im imports-Array ebenfalls hinzugefügt werden. Hier werden alle Module, die unterhalb der App-Component laufen hinzugefügt.

```typescript
imports: [
    BrowserModule,
    HttpClientModule,
    ...
```

Nun kann ein weiterer, generischer Service erstellt werden, den wir auch in der gesamten Applikation verwenden wollen um http-Actions durchzuführen. Der Source-Code dafür kann in den neu erstellten Service, einfach kompiert werden. Die Implementierung ist generisch.

```powershell
ng g service shared/services/repository
```

```typescript
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EnvironmentUrlService } from './environment-url.service';
 
@Injectable()
export class RepositoryService {
 
  constructor(private http: HttpClient, private envUrl: EnvironmentUrlService) { }
 
  public getData(route: string) {
    return this.http.get(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }
 
  public create(route: string, body) {
    return this.http.post(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  }
 
  public update(route: string, body){
    return this.http.put(this.createCompleteRoute(route, this.envUrl.urlAddress), body, this.generateHeaders());
  }
 
  public delete(route: string){
    return this.http.delete(this.createCompleteRoute(route, this.envUrl.urlAddress));
  }
 
  private createCompleteRoute(route: string, envAddress: string) {
    return `${envAddress}/${route}`;
  }
 
  private generateHeaders() {
    return {
      headers: new HttpHeaders({'Content-Type': 'application/json'})
    }
  }
}
```

`HttpClient` und `EnvironmentUrlService` werden über Dependency-Injection instanziert und stehen uns so direkt zur Verfügung. Anschließend die Methoden für die 4 wichtigsten http-Methoden (GET, POST, PUT, DELETE), eine private-Method die uns die finale URL zusammenstringt und zuöetzt eine private-Method, die uns den http-Header mit Daten befüllt (z.B. ContentType oder Authorize).

## Models, Subscriptions und Darstellung der Daten

Mit dem generierten Repsoitory-Service können wir bereits Daten von einer API abholen. Jetzt müssen diese noch Client-seitig verarbeitet weden. Dazu benötigen wir ein Model. Das von der API erhaltene JSON muss ja letztlich in ein Objekt umgewandelt werden könne, mit dem wir arbeiten können:

```typescript
export class SchuelerDto {
    id: string;
    name: string;
    vorname: string;
    adresse: string;
    klasse: string;
}
```

Die vorhandenen Felsder müssen denen des DTO*s der API entsprechen und kompartible Datentypen haben. Die Syntax ist alelrdings Cammel-Cased.

Wir beschäftigen uns mit der `schueler-list`-Komponente:
Die Komponente muss nun die Daten von der API abholen und der View (dem html) zur Verfügung stellen:

Imports nicht vergessen: (das Model und dern Repository-Service)

```typescript
import { SchuelerDto } from './../../models/schuelerDto';
import { RepositoryService } from './../../shared/services/repository.service';
```

Nun eine Methode, welche die Daten lädt:

```typescript
public getAllSchuelers() {
let apiAddress: string = 'api/schueler/all';
this.repository.getData(apiAddress)
    .subscribe(res => 
    { 
        this.schuelers = res as SchuelerDto[] 
    })
}
```

Hierbei handelt es sich um eine Subscription. Der Datenzugriff auf die API erfolgt asnchron. Daher müssen wir uns auf den Methodenaufruf vom Repository-Service "subscriben". Der Request (GET, POST, PUT, DELETE, ...) wird abgesetzt und anschließend das Progemm fortgesetzt ohne auf den Resppnse zu warten. Der Response könnte ja einige Zeit dauern. Der Aufrufer subscribt sich also auf den Request und wird informiert, wenn die Daten empfangen werden. (siehe Observer-Pattern)

Die eben erstellte Methode wird einfach in der `ngOnInit()`-Methode aufgerufen:

```typescript
ngOnInit() {
    this.getAllSchuelers();
}
```

Jetztmuss noch die View angepasst werden: (Eine eifache html-Table mit bootstrap formatiert tut es bereits)

```html
<div class="row"> 
  <div class="col-md-offset-10 col-md-2">
    <a href="#">Neuer Schüler</a>
  </div>
</div>
<br>
<div class="row">
  <div class="col-md-12">
    <div class="table-responsive">
      <table class="table table-striped">
        <thead>
          <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Vorname</th>
            <th>Adresse</th>
            <th>Klasse</th>
            <th>Details</th>
            <th>Ändern</th>
            <th>Löschen</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let schueler of schuelers">
            <td>{{schueler.id}}</td>
            <td>{{schueler.name}}</td>
            <td>{{schueler.vorname}}</td>
            <td>{{schueler.adresse}}<td>
            <td>{{schueler.klasse}}</td>
            <td><button type="button" id="details" class="btn btn-default" (click)="redirectToDetailsPage(schueler.id)">Details</button></td>
            <td><button type="button" id="update" class="btn btn-success" (click)="onSelect(schueler)">Ändern</button></td>
            <td><button type="button" id="delete" class="btn btn-danger" (click)="onDelete(schueler)">Löschen</button></td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</div>
```

## Kommunikation zwischen Komponenten

Wir haben nun die Liste aller Schüler. Jetzt wollen wir die Daten eines bereits geladenen Schülers an eine Details-Kompüonente weitergeben und dort anzeigen.

Dazu erstellen wir die Komponente `schueler-details-component`. Also definieren wir im ts-File dieser Komponente eine Variable vom Typ SchuelerDto. Darin werden die Daten jenes Schülers gespeichert, der angezeigt werden soll.

Komponenten kommunizieren mit den beiden Direktiven @Input() (Information wird von der aufrufenden Parent-Komponente an die Komponenten übermittelt) und @Output() (Information verlässt die Komponente an die aufrufende Parent-Komponente. Das kann natürlich nur über Events erfolgen Z.B. ein Button/Link wird gedrück oder ein Request empfängt asynchron Daten). Also deklarieren wir die Varianble mit dem Decorator `Input()`:

```typescript
@Input()
public schueler: SchuelerDto;
```

Natürlich benötigen wir in dieser Komponente (`schueler-details-component.html`) auch etwas html:

```html
<h3><strong>Schüler Details</strong></h3>

<div>
  <h2>{{schueler?.vorname}} {{schueler?.name}}</h2>
  <dl>
    <dt>ID:</dt>
    <dd>{{schueler?.id}}</dd>

    <dt>Name:</dt>
    <dd>{{schueler?.name}}</dd>

    <dt>Vorname:</dt>
    <dd>{{schueler?.vorname}}</dd>
  </dl>
</div>
```

Dann noch in der aufrufenden Komponente (`schueler-list-component.html`) den html-Code, damit die Sub-Komponente gerendert wird. Der datensatz wird durch Binding an die Komponente übermittelt.

```html
<app-schueler-details [schueler]="selectedSchueler"></app-schueler-details>
```

Ich habe das mal ganz an den Anfang der Komponente gesetzt. Der Platz ist natürlich nicht frei wählbar. Das Unter-html wird ja in die Komponente gerendert.

## Eine Detail-Seite über Routing erreichen

Um den Unterschied kennenzulernen, wollen wir nun die gleichen Detail-Infos auf eine andere, eigene Seite mit eigener Route auslagern. Denn oft haben wir ja in der Liste nicht alle Infos zur Verfügung und mÜssen die Informationen nachladen, wenn auf einen Datensatz geklickt wird. Natürlich würde das auch in einer Unterkomponente (siehe vorheriger Punkt) funktionieren, aber so können wir gleich das Child-Routing kennen lernen.

Eine Neue Komponente erstellen:

```powershell
ng g component schueler/schueler-details-page
```

Im Modul muss eine neue Route hinzugefügt werden:

```typescript
{ path: 'details/:id', component: SchuelerDetailsPageComponent }
```

im ts-File benötigen wir eine neue Methode, die auf die Detailseite routet und aus dem html aufgerufen wird:

```typescript
public redirectToDetailsPage(id) {
  let redirectUrl: string = `schueler/details/${id}`;
  this.router.navigate([redirectUrl]);
}
```

Das ts-File der Komponente `schueler-details-page.component.ts` könnte nun die über die Route übergebene ID auslesen und weiter verarbeiten. Z.B. in der onNgInit() Methode.

```typescript
export class SchuelerDetailsPageComponent implements OnInit {
  public id: number = -1;

  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    let id = this.activatedRoute.snapshot.params["id"];
    this.id = id;
  }
}
```

Und natürlich das html:

```html
<h3><strong>Schüler Details</strong></h3>

<div>
  <h2>{{schueler?.vorname}} {{schueler?.name}}</h2>
  <dl>
    <dt>ID:</dt>
    <dd>{{id}}</dd>

    <dt>Name:</dt>
    <dd>{{schueler?.name}}</dd>

    <dt>Vorname:</dt>
    <dd>{{schueler?.vorname}}</dd>
  </dl>
</div>
```

## Die Detail-Seite dynamisch mit Daten befüllen

Wir erhalten also aus der Parent-Komponente nur eine ID. Damit können wir  die API nach den restlichen Daten befragen. Folgende URL würde das erledigen, z.B. für ID=1: `http://localhost/api/details/1`.

Dazu benötigen wir nun eine Methode in der Detais-Page-Komponente, welche diese Url zur API absetzt:

```typescript
public getSchuelerDetails(id: number) {
  let apiUrl: string = `api/schueler/${id}`;
  this.repository.getData(apiUrl)
  .subscribe(res => 
    { 
      this.schueler = res as SchuelerDto
    });
}
```

und in onNgInit() wir diese Methode natürlich aufgerufen:

```typescript
ngOnInit() {
  let id = this.activatedRoute.snapshot.params["id"];
  this.id = id;

  this.getSchuelerDetails(this.id );
}
```
