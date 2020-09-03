# typescript Lazy Loading

Unter layzy Loading versteht man im allgemeinen das Laden von Informationen erst dann, wenn man sie tatsächlich benötigt. Ein OR.Mapper verfügt in der regel über Lazy Loading Mechanismen. Dabei werden die Daten erst dann aus einer DB geladen, wenn sie tatsächlich angefragt werden. Z.B. Detaildaten eines Datensatzes, werden erst dann geladen, wenn auf ein Property der Details zugeriffen wird.

Ähnlich ist es hier in Angular. Eingangs habe ich erwähnt, dass Angular zu Beginn alles html lädt, welches später irgendwo auf der Web-Seite angezeigt werden wird. Das stimmt auch, hat aber den Nachteil dass der erste Ladevorgang recht lange dauert, da ja alles html des gesamten Web-Portals übertragen wird. Mit Lazy Loading kann man nun Teile erst dann laden, wenn sie tatsächlich angesurft werden. Es könnte ja sein, dass einige Teile eines Web-Portals kaum oder selten angesurft werden. (z.B. die AGB :D)

Das Lazy-Loading wird im Prinzip über Routing und die Aufteilung der Module definiert. Hat ein Modul Untermodule und wird das Routing zu dessen Komponenten speziell angegeben, werden diese nachgeladen (Lazy). Ein Beispiel wird das verdeutlichen:

## Ein Schüler-Modul erstellen

Als erstes wird ein neues Sub-Module erstellt. Das Hauptmodul ist ja `app.module.ts`. Im Verzeichnis `schueler` erstellen wir nun ein weiters Modul `schueler.modul.ts`. Das ist nun ein Untermodul (Sub-Modlue).

```powershell
ng g module schueler
```

## Die Schüler-Komponenten erstellen

in diesem Verzeichnis werden nun und weitere Komponenten erstellet:

```powershell
ng g component schueler/schueler-list
```

```powershell
ng g component schueler/schueler-details
```

## Das Routing zum Schüler-Modul mit Lazy Loading

Zuerst muss das Routing im App-Module (Hauptmodul) geändert werden. Wir routen nicht mehr zur Child-Komponente `schueler`, sondern zum Child-Module.

das weg:

```typescript
{ path: "schueler", component: SchuelerComponent },
```

das dazu:

```typescript
{ path: "schueler", loadChildren: './schueler/schueler.module#SchuelerModule' },
```

Nicht vergessen, die alte Schüler-Komponnete aus den imports zu löschen! Es passiert zwar nichts wenn an das vergisst, ist aber kein sauberes coding.

## Routing für das Child-Module

Das App-Module routet nun auf das Child-Module. Dieses muss das Routing aufnehmen und zu den ihm bekannten Komponenten weiter routen. Dafür müssen wir das RouterModule in der Child-Komponente importieren (klar, damit Routing überhaupt funktioniert) und darauf die Methode `forChild` anwenden.

```typescript
import { RouterModule } from '@typescript/router';
```

```typescript
RouterModule.forChild([
      { path: 'list', component: SchuelerListComponent }
    ])
```

Anschließend muss die Routing-Url in der Nav-Komponente geändert werden:

```typescript
<li><a [routerLink]="['/schueler/list']">Schüler</a></li>
```

Jetzt (man kann es nicht auf den ersten Blick erkennen) wird die Schüler-Liste erst dann geladen, wenn man im Menü auf den Link klickt. In den Debugging-Tool des Browsers (unter Network) kann man es nachvollziehen.
