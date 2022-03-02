# Routing in Angular

Beschäftigen wir uns kurz mit Routing in Angualar. Als erstes werden wir ein Menü auf der Website unterbringen. Das folgende html-Snipet basierend auf bootstrap kann einfach kopiert werden.

## Navigtion erstellen

```html
<nav class="navbar navbar-inverse">
  <div class="container-fluid">
    <div class="navbar-header">
      <a class="navbar-brand" href="#">School2000 Home</a>
    </div>
    <ul class="nav navbar-nav">
      <li><a href="#">Schüler</a></li>
      <li><a href="#">Lehrer</a></li>
    </ul>
  </div>
</nav>
```

## Das Routing erstellen

Wie bereits im letzten Kapitel erwähnt, muss die Komponente nun im html untergebracht werden, damit Angular eine Instanz davon erstellen kann. Da wir das Menü auf allen Seiten sehen wollen, platzieren wir den Selector auf der App-Komponente (`app-component.html`)

```html
<div class="container container-fluid">
  <div class="row">
      <app-menu></app-menu>
  </div>
</div>
```

Jetzt müsste das Menü bereits sichtbar sein, allerdings noch volkommen ohne Funktion

## Das Routing in Angular konfigurieren

Als nächstes werden wir das Menü zum Laufen bringen. Rounting in Angular funktioniert über die Module. Das heißt, Routen zu den jeweiligen Komponenten, werden im darüberliegenden Modul festgelegt. Die dafür verwednete Klasse ist das `RouterModule` (@angular/router).

Dieses Klasse muss im jeweiligen Modul importiert werden, anschließend kann sie verwendet werden. Die Funktion die dafür zu Verfügung steht heißt: `forRoot()`. Ihr wird ein Array aus Objekten übergeben.

`forRoot` kann nur einmal im `app.modul.ts` vwerwendet werden. In allen darunterliegenden Modulen ist es `forChild`. Mehr dazu später.

Wir müssen als erstes das Router-Modul imprtieren und konfigurieren:

`app.module.ts`

```typescript
import { RouterModule} from '@angular/router'
```

```typescript
RouterModule.forRoot([
    { path: "home", component: HomeComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full' }
])
```

## Links im Menü anpassen

Als erstes brauchen wir ein Router-Outlet in der `app-component`. In dieses `Outlet` wird das Ergebnis-html des Rountings gerendert. Wir wollen für den Anfang 2 bis 3 Seiten, die Über das Menü angesteuert werden können. Das Verhalten ist also einer z.B. simplen MVC-Applikation ähnlich. Wird ein Punkt im Menü gecklickt, erneuert sich der Inhalt der ganzen Seite. Allerdings nur fast, denn das Menü soll auf allen Seiten gleich sein.

Wir setzte daher also das `router-outlet` (genauso wie das Menü) in die `app-component.html`. Jetzt wird das Menü gerendert und darunter das jeweilige Routing-Ergebnis.

**Wichtig!**
Angular ist ein reaktives Framework. Bei einem Seitenwechsel wie wir ihn nun implementieren, wird nicht die gesamte Seite neu geladen, sondern es wird nur ein anderer Inhalt angezeigt. Alle Inhlate aller Seiten (außer Daten) wurden ja bereits beim ersten Aufruf der Seite geladen.

```html
<div class="row">
    <router-outlet></router-outlet>
</div>

Dann die Links in der Nav-Komponente anpassen. Diese müssen natürlich auf ein Event-Bindig gesetzt werden. Ein Click ist ein Event, also Event-Binding.

```html
<li><a [routerLink]="['/schueler']">Schüler</a></li>
```

## Eine '404-Not Found' Komponente erstellen

Die Komponente wird über das bereits bekannte ng Generate-Kommando erstellt. Idealerweise in einem Unterverzeichnis, z.B. `error-pages/`.

```powershell
 ng g component error-pages/not-found
 ```

Nun muss natürlich noch das Routing dafür im Hauptmodul (App-Module) angepasst werden:

```typescript
{ path: '404', component: NotFoundComponent },
...
{ path: '**', redirectTo: '/404', pathMatch: 'full' }
```

Zur Erklärung: Die erste Route ist einfach nur eine Route zur Komponente `NotFound`, also für die URL: `http:///localhost:4200/404`. Das hat nichts mit dem Fehler zu tun, es ist eine normale Route. Die zweite Route `**` aktiviert die erste Route (`404`) für alle Routen, für die keine Übereinstmmung gefunden werden kann.

Das `app-module.ts` nun bisher:

```typescript
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule} from '@angular/router'

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    NotFoundComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: "home", component: HomeComponent },
      { path: "404", component: NotFoundComponent },
      { path: "", redirectTo: '/home', pathMatch: 'full' },
      { path: "**", redirectTo: '/404', pathMatch: 'full' }
    ])
  ],
  providers: [
    SessionDataService,
    EnvironmentUrlService,
    RepositoryService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
```

## Übung

Es soll eine Angular-App erstellt werden die über eine Übersichtsseite und eine Archivseite verfügt. Damit soll Archivmaterial verwaltet und nach Monat und Jahr sortiert werden können.

Die Startseite enthält eine Liste von Datumswerten [Monat]/[Jahr]
z.B.
* 10/2021
* 11/2021
* 12/2021
* 01/2022
* 02/2022

Klickt man auf ein Datum in dieser Liste, soll die Archivseite angezeigt werden. Die Überschrift darauf stellt einen Namen (*Archiv für ...*) und das gewählte Monat und Jahr dar. (Archiv für Jan. 2022). Das numerische Monat soll also in einen Namen umgewandelt werden.

Die Archivseite verfügt darüber hinaus über einen Button, der wiederum auf die Übersichtsseite routet.

Die Parameter zur Archivseite sind *required*. (URL: http://localhost4200/archive/01/2022). **KEINE optionalen Parameter!**. Ist die Url unvollständig angegeben, oder wird ein Datum angegeben, das nicht existiert, ist eine *Not-Found*-Seite anzuzeigen.
