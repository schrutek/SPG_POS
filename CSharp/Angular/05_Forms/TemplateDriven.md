# Template riven Forms

Angular bietet 2 Varianten von Forms. **Template Driven Forms** und **Reactive Forms**.

## Form

Im Type Script File, das zur Komponente gehört, benötigen wir natürlich ein public-Property für die Daten `ProductItem`, eine Methode für die Submit-Action und eine Methode für die Log-Action, die aufgerufen wird, wenn sich im ersten Eingabefeld etwas verändert.

```typescript
import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ProductItem } from 'src/app/interfaces/product-item';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent {
  public productItem: ProductItem = new ProductItem();

  onSubmit(): void {
    console.log(`Product-Info:  ${this.productItem.name} | ${this.productItem.price}`);
  }

  log(x: any): void {
    console.log(x);
  }
}

```
Im HTML wird die Methode `onSubmit` mit dem Attribut `ngSubmit` gebunden.


```html
<form (ngSubmit)="onSubmit()">
  <input type="text" id="name" name="name" class="form-control" (change)="log(name)">

  <input type="text" id="price" name="price" class="form-control">

  <br>
  
  <button class="btn btn-success" type="submit">Save</button>
</form>
```

## Validation

Template Driven Forms sind einfacher zu verwenden als Reactive Forms, aber auch weniger flexibel im Funktionsumfang. Type Script ist in den gängigsten Validierungsfällen (required, minLength, maxLength, pattern) nicht notwendig. Es kann im HTML gebunden werden.

### required, minLength

```html
<form (ngSubmit)="onSubmit()">
  <input type="text" id="name" name="name" class="form-control" required minlength="4"
      [(ngModel)]="productItem.name" [ngModelOptions]="{standalone: true}" #name="ngModel" (change)="log(name)">
  <div *ngIf="name.invalid && (name.dirty || name.touched)" class="alert alert-danger">
    <div *ngIf="name.errors?.['required']">
      Name is required.
    </div>
    <div *ngIf="name.errors?.['minlength']">
      Name must be at least 4 characters long.
    </div>
  </div>
  
  <input type="text" id="price" name="price" class="form-control"
      [(ngModel)]="productItem.price" [ngModelOptions]="{standalone: true}" #price="ngModel">
  <div *ngIf="price.invalid && (price.dirty || price.touched)" class="alert alert-danger">
    <div *ngIf="price.errors?.['required']">
      Price is required.
    </div>
  </div>

  <br>
  
  <button class="btn btn-success" type="submit">Save</button>
</form>
```

folgende Attribute werden dem input-Field zusätzlich gegeben: `required minlength="4" [(ngModel)]="productItem.name" [ngModelOptions]="{standalone: true}" #name="ngModel"`

* `ngModel` enthält das gesamte Model der Form. Die Methode `log(name)` gibt dessen Inhalt in der Konsole aus.
* `#name="ngModel"` bindet das Feld `name` an das Model (`ngModel`).
* `id="price" name="price"` wurden vorher schon gesetzt. Die Angabe beider Attribute ist notwendig.
* `required minlength="4"` sind die eigentlichen Beschränkungen für das Eingabefeld. Es ist ein Pflichtfeld (`required`), die Eingabe muss mindesten 4 Zeichen lang sein (`minlength="4"`).

Die Darstellung der Validierungsinformationen (bzw. Fehlermeldungen) erfolgt nun folgendermaßen:

```html
<div *ngIf="name.invalid && (name.dirty || name.touched)" class="alert alert-danger">
  <div *ngIf="name.errors?.['required']">
    Name is required.
  </div>
  <div *ngIf="name.errors?.['minlength']">
    Name must be at least 4 characters long.
  </div>
</div>
```

Dieses div wird direkt unterhalb des `input`-Fields platziert. `ngModel` bietet nun verschiedene Properties an (`log()` zeigt diese in der Konsole), anhand derer der Zustand der Eingabe ausgewertet werden kann. Die Properties treten dabei immer paarweise auf (valid - invalid, touched - untouched, dirty - pristine, ...)

Die wichtigsten im Überblick:

* valid/invalid: Entspricht die Eingabe der Validation.
* touched/untouched: Ob das Feld nach einem Fokus verlassen wurde.
* dirty/pristine: Ob sich der Wert im Feld verändert hat.
* errors: Ein Array, das die fehlgeschlagenen Validierungen enthält. Achtung! Nicht die Fehlermeldung, die steht ja direkt im `div` als `InnerHtml`.
