# Services

Service spielen in typescript eine zentrale Rolle. Services sind immer Klassen, die Funktionen zur Verfügung stellen. Dabei sollte ein Service immer eine sehr spezielle Funktion haben, um die Wiederverwendbarkeit von Services und Wartbarkeit von Applikation zu erhöhen.

Services können untereinander abhängigkeiten haben, aslo die in C# bekannten Aggregationen.

Services können sowohl von mOdulen, laso auch von Komponenten verwendet werden, abgesehen davon, dass auch Services andere Services verwenden können (Aggregation). Die Instanzierung der Services erfolgt dabei immer über Dependenzy-Injection.

![](dependency-injection.png)

Um eine Service letzlich verwwenden zu können muss dieser dem Service-Provider (provider) bekannt gegeben werden. Dabei wird der Service in dem Modul, in dem er zur Verfügung stehen soll dem Provider-Array hinzugefügt. Dies wird nachstehend an einem Beispiel erklärt.

### Achtung! typescript ist asynchron

Auf eine Sache ist aufzupassen. Wenn Servies ansynchrone Methoden beinhalten, die weiderum synchrone, oder auch asnychrone Methoden anderer Service aufrufen, kann es zu unerwarteten Ergebnissen kommen. In solchen Fällen schafft das Schlüsselwort `then` Abhilfe.

Weitere Informationen hier: https://typescript.io/guide/architecture-services

---

Zum Start aber, erweitern wir unsere Home-Kopüonente:

## Ein einfacher Counter in der Home-Component

Wir erstellen einen einfachen Counter in der Home-Component. Auf eine Variable im ts-File wird im html-File ein Binding gesetzt.

{{myCounter}}

z.B.:

```html
<h2>
  Ein einfacher Counter:
</h2>
<h3>
  {{myCount}}
</h3>
```

`home-component.ts:`

```typescript
public myCount: number = 0;
```

Zwei Methoden inkrementiren, bzw. dekrementieren die Variable:

Als erstes die beiden Buttons im html-Fle:

```html
  <button type="button" id="decrement" class="btn btn-default" (click)="decrement()">( - )</button>
  <button type="button" id="increment" class="btn btn-default" (click)="increment()">( + )</button>
```

Dann natürlich die beiden Methoden im ts-File:

```typescript
  public increment() {
    this.myCount++;
  }

  public decrement() {
    this.myCount--;
  }
```

Was passiert nun mit dem Wert für `myCount`, wenn die Route gewehselt wird?

---

### Antwort

typescript hat keinen eigenen State. Wie jede andere Web-Technologie ist typescript stateless. Aber es stellt durchaus brauchbare Möglichkeiten zur Verfügung um States zu generieren. Eine Einfache Lösung wird nun vorgestellt.

# Ein Erster Root-Service (State-Service)

Wird die Route gewechselt, geht der Wert für `myCount` verloren.

Lösung:

Wir erstellen über die CLI einen Service über

```powershell
ng g service shared/services/session-data
```

und registreiren ihn im app.module.ts im providers-Array. Jeder Service muss dort registriert werden, wenn er für die ganze Applikation zur verfügung stehen soll.

```typescript
import { SessionDataService } from './shared/services/session-data.service';
...
providers: [
    SessionDataService
  ],
```

In der `home-component.ts` kann ich nun diesen Service im Konstruktor angeben und so über Dependency-Injection verwenden.

In den beiden Methoden (increment, decrement) wird nun der aktuelle Wert von myCount im  Service gesetzt. In der Methode `ngOnInit()` wird er aus dem Service gelesen und damit die lokale Variable befüllt.

!Nicht vergessen, im Service für die Variable einen Default-Wert zu setzen!

### Erweiterung zu Subjects

comming soon :)
