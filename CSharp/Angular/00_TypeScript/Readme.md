# Basics

Dieses Kapitel erklärt die Grundlagen von Node.js, und TypeScript

## Node.js

![Node](hire-nodejs-developer.png)

Node.js ist eine plattformübergreifende Entwicklungsumgebung, die nicht im Browser ausgeführt wird. Anders gesagt, JavaScript kann damit am Server ausgeführt werden (Back End). Node ist dabei keine eigene Programmier- oder Script-Sprache, es ist lediglich ein Container (`Node.exe`, geschrieben in C++) in dem Java Script ausgeführt werden kann. Hierfür kam Googles v8-Engine zum Einsatz (weitere wären: Chakra für Edge, SpiderMonkey für Firefox).

Im erstenMoment klingt das eigenartig. Warum JavaScript am Server, wo es doch tolle Programmiersprachen (C#, Java, ...) gibt.

**Vorteile**:
* Viele Frontendentwickler, können nun auch am BackEnd arbeiten.
* Prototyping: Ideal für Prototypen, die relativ zeitnah erstellt werden müssen.
* JavaScript nun sowohl am Server, als auch am Client.
* Viele Open Source Libraries.

Node wird also verwendet um TypeScript Code zu interpretieren. Node stellt dafür einen Transpiler (Achtung, nicht Compiler) zur Verfügung, der TypeScript in JavaScript transpiliert. Letztlich kann ein Browser und auch Node nur JS-Code ausführen.

## TypeScript, JavaScript, ECMAScript

### ECMAScript

*ECMAScript* ist die Skriptsprache, auf welcher JavaScript basiert. [*ECMA International*](https://www.ecma-international.org) ist mit der Normung von ECMAScript befasst.

### JavaScript

Java Script basiert auf ECMAScript. ECMAScript definiert den Standard, den JavaScript erfüllt. JavaScript kann also all dies und gegebenenfalls noch etwas mehr.

### TypeScript

TypeScript ist ein Superset, also eine Erweiterung von JavaScript. TypeScript kann also alles was JavaScript auch kann und noch etwas mehr. Man könnte also problemlos in einem TS-File JavaScript Code schreiben. Node könnte das interpretieren.

Wir erinnern uns, auch Node führt nur JS Code aus. Der TS Code wird also zu JS transpiliert. Man kann das mit Programm Code und Intermediate Language einer Programmiersprache (C#, Java) vergleichen.

Weiterführende Links für Interessierte:

* https://www.typescriptlang.org
* https://developer.mozilla.org/de/docs/Web/JavaScript
* https://www.ecma-international.org

### tsc

Folgendes ist das Node-Kommando mit dem man ein TS-File in ein JS-File transpiliert. (``tcs <filename>.ts``)

```powershell
tsc demo.ts
```

Dies erstellt das File `demo.js` im selben Verzeichnis.

Vorteile von TS sind nun z.B.:
* **Strong Typing**: Ähnlich wie in C# oder Java kann man in TS Datentypen definieren. Das bringt Vorteile wie z.B....
* **Compile Time Errors**: Durch die strenge Typisierung können Fehler bereits zur Entwurfszeit/Kompilierzeit in der IDE angezeigt werden. Sie treten also nicht, wie bei JS, erst zur Laufzeit auf.
* **Object Oriented Features**: Das objektorientierte Konzepte wie Vererbung, Interfaces, Polymorphismus, Konstruktoren, ... ist umgesetzt
* **Tooling**: Umfangreiches Tool Set durch Node.js

### Beispiele TS:

Folgendes TS...

TS:
```typescript
function logMessage(message) {
    console.log(message)
}

let message='Hello World!';

logMessage(message);
```

...erstellt folgendes JS:

JS:
```javascript
function logMessage(message) {
    console.log(messages);
}
var message = 'Hello World!';
logMessage(message);
```

Die beiden Files sind also bis auf Zeilenumbrüche ident.

Ausführen kann man das JS-File mit `node <filename>.js`:

```powershell
node demos.js
```

Gehen wir nun zu den Unterschieden:

Ersetzen wir mal `var` durch `let`. `let` gibt es in ES5 nicht, daher wird es von Node durch `var` ersetzt. `var` hat den Scope der nächsten function, `let` jenen des nächsten Blocks. Anders gesagt: `var` gilt in der gesamten function, `let` nur innerhalb des `for`-Blocks. Das führt in TS zu einem Fehler zur Entwurfszeit. Node erstellt dennoch das JS-File. Es ist sogar lauffähig.

TS:
```typescript
function log() {
    for (let i = 0 ; i< 5 ; i++) {
        console.log(i)
    }
    console.log('Final: ' + i)
}

log();
```
**Achtung!!**

JS: Die Variable ``i`` wird in der Zeile ``console.log('Final' + i)`` rot unterwellt sein.

Das erstellte JS-File dazu:

JS:
```javascript
function log() {
    for (var i = 0 ; i< 5 ; i++) {
        console.log(i)
    }
    console.log('Final: ' + i)
}
log();
```

`let` wurde durch `var` ersetzt.

### Typisierung

Die Typisierung ist jener zu C# sehr ähnlich. Die Syntax ist vielleicht etwas gewöhnugsbedürftig.

Wird kein Datentyp explizit angegeben, ist der Default-Datentyp ``any``. Er ist mit var in C# vergleichbar. Zur Kompilierzeit wird er festgelegt.

Einige Beispiele:

TS:
```typescript
function demos() {
    let a = 5;              // wird als Number festgelegt
    let b;                  // Wird asl any festgelegt
    let c:string;           // Wird als string festgelegt
    let d = 'Hello World!'  // Wird asl string festgelegt

    let e = 12;
    e = 'Hello World!'      // Liefert hier einen Fehler, nicht aber in JS

    console.log(a);
}
demos();
```

TS:
```typescript
firstName
```

``firstName`` ist nun mit `any` initialisiert.

Explizite Typisierung:

TS:
```typescript
firstName: string
```

Beispiel ``any``:

TS:
```typescript
function demos() {
    let a = 5;
    a = 'Hello World!'
    console.log(a);
}
demos();
```

Liefert einen Kompilierfehler, ist aber gültig in ES5 und würde ausgeführt werden.

JS:
```javascript
function demos() {
    var a = 5;
    a = 'Hello World!';
    console.log(a);
}
demos();
```

Wie in C# ist es ratsam sich von Anfang an auf Datentypen festzulegen.

### Enum

Bekannt aus C#:

TS:
```typescript
enum Colours {Red, Blue, Green}
let backgoundColor = Colours.Blue
```

Das erstellte JS-File ist etwas weniger gut lesbar:

JS:
```javascript
var Colours;
(function (Colours) {
    Colours[Colours["Red"] = 0] = "Red";
    Colours[Colours["Blue"] = 1] = "Blue";
    Colours[Colours["Green"] = 2] = "Green";
})(Colours || (Colours = {}));
var backgoundColor = Colours.Blue;
```

### Intellisense

Durch die strenge Typisierung erhalten wir bereits zur Entwurfszeit Kompilierungsfehler in der IDE. Aber nicht nur das. Dadurch ist auch das bekannte Intellisense aus Visual Studio möglich. Durch die Typisierung weiß der Compiler ja welche Felder, Methoden, Konstruktoren, Properties der Typ zur Verfügung stellt.

### Arrow Functions

Bereits aus C# bekannt, die Lambda Expressions. Ein ähnliches Konstrukt gibt es in TS. Hier ist der Name *Arrow Function*:

Eine klassische Methode...

TS:
```typescript
let log = function(x) {
    console.log(x);
}
```

...als Arrow Function

TS:
```typescript
let log = (x) => console.log(x);
```

### Class, Interface

Arrow Functions können schnell mehrere bis viele Parameter enthalten. Das sollte vermieden werden. Die Lösung bietet z.B. ein Interface:

weniger gut:

TS:
```typescript
let log = function(firstName, lastName, eMail, d, e, f, g, h) {
    console.log(firstName + ' ' + lastName + ' ' + eMail);
}
```
besser:

TS:
```typescript
let log = function(person: Person) {
    console.log(person.firstName + ' ' + person.lastName + ' ' + person.eMail);
}
```

mit dem Interface-Typ `Person`. Das führt uns zwangsweise zum Kapitel Klassen und Interfaces, die wir in TS wie aus C# oder Java gewohnt, auch hier zur Verfügung haben.

### Klassen und Interfaces

Für die vorherige Korrektur benötigen wird einen eigenen Typ.

TS:
```typescript
interface Person {
    firstName: string,
    lastName: string,
    eMail: string,
    d, e, f, g, h

    // writeFullName() {...} kann es in Interfaces nicht geben
}
```

Nachteil am Interface: Man kann keine Methoden implementieren. Die Lösung wäre eine Klasse zu definieren.

TS:
```typescript
class Person {
    firstName: string;
    lastName: string;
    eMail: string;

    // writeFullName() {...}
}
```

### Konstruktor

Ebenfalls sind Konstruktoren möglich. Pro Klasse gibt es nur einen Konstruktor. Er kann beliebig viele Parameter enthalten. Möchte man nicht alle Parameter beim Aufruf angeben, kann man sie optional setzten. Dafür werden sie einfach mit einem Fragezeichen am Ende versehen `(a, b, c, d?, e?, f?)`. Alle Parameter links neben dem ersten optionalen Parameter müssen ebenfalls optional sein. (Wie in C#)

TS:
```typescript
constructor(firstName: string, lastName: string, eMail?: string) {
    this.firstName = firstName
    this.lastName = lastName
    this.address = new Address('Spengergasse');
}
```

### Properties

Wie in Java werden Get- und Set-Methoden geschrieben:

TS:
```typescript
class Person {
    private lastName: string;

    getLastName() {
        return this.lastName;
    }

    setLastName(value) {
        if (value == '') {
            throw Error('Nachname ist ein Pflichtfeld!');
        }
        this.lastName = value;
    }
}
```

Zu einem Property wird dies indem man den Get/Set-Methodennamen durch ein Leerzeichen "auftrennt":

TS:
```typescript
class Person {
    private lastName: string;

    get LastName() {
        return this.lastName;
    }
    set LastName(value) {
        if (value == '') {
            throw Error('Nachname ist ein Pflichtfeld!');
        }
        this.lastName = value;
    }
}
```

### Modules

Prinzipiell ist ein Modul ein File. Klassen darin werden als Module exportiert, damit sie in anderen Files (als Module) verwendet werden können. Files können natürlich mehrere Klassen enthalten. Es wird dann aus einer Klasse ein Modul mit dem Keyword `export` exportiert.

![Modules](Modules.png)

```typescript
export class Person { }
```

Nun kann das Modul in einem anderen File importiert werden.

Z.B.: ``main.ts`` importiert nun das Modul ``Person``. Der Filename in der Zeile `import` ist der Name des TS-Files indem das Modul exportiert wird. Die Extension muss entfallen. Der Pfad kann relativ geändert werden.

Mit dem `new`-Keyword wird einen neue Instanz der Klasse `Person` erstellt. Alles weitere ist aus der Objektorientierung bekannt.

```typescript
import { Person } from './person';

let person = new Person('Martin', 'NoName');
person.LastName = 'Schrutek';
person.eMail = 'schrutek@spengergasse.at';
person.writeInfo();
console.log(person.address.street);
```

### Die ganzen Klassen

TS:
```typescript
export class Person {
    private firstName: string;
    private lastName: string;
    eMail: string;
    address: Address;

    constructor(firstName: string, lastName: string, eMail?: string) {
        this.firstName = firstName
        this.lastName = lastName
        this.address = new Address('Spengergasse');
    }

    writeInfo() {
        console.log(this.firstName + ' ' + this.lastName + ' ' + this.eMail)
    };

    get FirstName() {
        return this.firstName;
    }

    get LastName() {
        return this.lastName;
    }

    set LastName(value) {
        if (value == '') {
            throw Error('Nachname ist ein Pflichtfeld!');
        }
        this.lastName = value;
    }
}
class Address {
    street: string;

    constructor(street: string) {
        this.street = street;
    }
}
```

```typescript
import { Person } from './person';

let person = new Person('Martin', 'NoName');
person.LastName = 'Schrutek';
person.eMail = 'schrutek@spengergasse.at';
person.writeInfo();
console.log(person.address.street);
```

Es werden mittels Node folgende JS's transpiliert:

JS:
```javascript
"use strict";
exports.__esModule = true;
exports.Person = void 0;
var Person = /** @class */ (function () {
    function Person(firstName, lastName, eMail) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.address = new Address('Spengergasse');
    }
    Person.prototype.writeInfo = function () {
        console.log(this.firstName + ' ' + this.lastName + ' ' + this.eMail);
    };
    ;
    Object.defineProperty(Person.prototype, "FirstName", {
        get: function () {
            return this.firstName;
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(Person.prototype, "LastName", {
        get: function () {
            return this.lastName;
        },
        set: function (value) {
            if (value == '') {
                throw Error('Nachname ist ein Pflichtfeld!');
            }
            this.lastName = value;
        },
        enumerable: false,
        configurable: true
    });
    return Person;
}());
exports.Person = Person;
var Address = /** @class */ (function () {
    function Address(street) {
        this.street = street;
    }
    return Address;
}());
```

```javascript
"use strict";
exports.__esModule = true;
var person_1 = require("./person");
var person = new person_1.Person('Martin', 'NoName');
person.LastName = 'Schrutek';
person.eMail = 'schrutek@spengergasse.at';
person.writeInfo();
console.log(person.address.street);
```

## Fazit

C# gewöhnte Entwickler haben eher weniger Schwierigkeiten sich an TS zu gewöhnen, da die Ähnlichkeiten auf der Hand liegen. Auch hier ein hilfreiches Werkzeug auf dem Weg vom BackEnd- oder FrontEnd-, zum FullStack-Entwickler.

## Auf dem Weg zu Angular

Die hier besprochenen Strukturen werden wir in Angular wiederfinden. Wenn man diese Grundlagen verstanden hat, ist es leichter sich im vermeintlichen "Angular-Dschungel" zurechtzufinden. In Wirklichkeit ist Angular, auch dadurch, sehr gut strukturiert.

Ich greife hier deutlich vor und zeige noch eine Klasse9, wie sie in Angular verwendet würde:

```typescript
import { Component } from "@angular/core";

@Component({
    selector: 'app-products',
    templateUrl: 'products.component.html',
    styleUrls: ['products.component.css']
})
export class ProductsComponent {
    title: string = 'Products Works!!'

    getTitle() {
        return this.title;
    }

    getProducts() {
        return [ 
            { name: 'Product A', price: 10.12 },
            { name: 'Product B', price: 12.54 },
            { name: 'Product C', price: 5.85 },
            { name: 'Product D', price: 53.60 } ]
    }
}
```

Wir finden den Import-Block, den Export-Block, Felder und Methoden. Diese Klasse verfügt über aber keine Properties oder Konstruktor. Lediglich neu: `@Component({ ... })`. Aber dazu mehr später.

## Übung

Es sind 3 Klassen zu implementieren ``program``, ``schoolClass``, ``student``

<svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" contentStyleType="text/css" height="505px" preserveAspectRatio="none" style="width:517px;height:505px;background:#FFFFFF;" version="1.1" viewBox="0 0 517 505" width="517px" zoomAndPan="magnify"><defs/><g><!--MD5=[dd19b58f1c5d03099707fafabb4a2785]
class schoolClass--><g id="elem_schoolClass"><rect codeLine="4" fill="#F1F1F1" height="113.1875" id="schoolClass" rx="2.5" ry="2.5" style="stroke:#181818;stroke-width:0.5;" width="144" x="23.5" y="245"/><ellipse cx="52" cy="261" fill="#ADD1B2" rx="11" ry="11" style="stroke:#181818;stroke-width:1.0;"/><path d="M50.7969,261.875 L52.25,261.875 L52.25,261.9844 C52.25,262.3906 52.2813,262.5469 52.3594,262.7031 C52.5156,262.9531 52.7969,263.1094 53.0938,263.1094 C53.3438,263.1094 53.6094,262.9688 53.7656,262.75 C53.8906,262.5938 53.9219,262.4375 53.9219,261.9844 L53.9219,260.0625 C53.9219,259.9063 53.9219,259.8594 53.9063,259.7031 C53.8438,259.2344 53.5313,258.9219 53.0781,258.9219 C52.8281,258.9219 52.5625,259.0625 52.3906,259.2813 C52.2813,259.4531 52.25,259.6094 52.25,260.0625 L52.25,260.1875 L50.7969,260.1875 L50.7969,257.7813 L54.7813,257.7813 L54.7813,258.6406 C54.7813,259.0469 54.8125,259.2188 54.8906,259.375 C55.0625,259.625 55.3438,259.7813 55.625,259.7813 C55.8906,259.7813 56.1563,259.6406 56.3281,259.4219 C56.4375,259.25 56.4688,259.1094 56.4688,258.6406 L56.4688,256.0938 L48.8438,256.0938 C48.4063,256.0938 48.2813,256.1094 48.125,256.2031 C47.875,256.3594 47.7188,256.6563 47.7188,256.9375 C47.7188,257.2188 47.8594,257.4688 48.0781,257.6406 C48.2344,257.75 48.4219,257.7813 48.8438,257.7813 L49.0938,257.7813 L49.0938,264.2969 L48.8438,264.2969 C48.4375,264.2969 48.2813,264.3125 48.125,264.4219 C47.875,264.5938 47.7188,264.8594 47.7188,265.1563 C47.7188,265.4219 47.8594,265.6719 48.0781,265.8281 C48.2188,265.9531 48.4531,266 48.8438,266 L56.8438,266 L56.8438,263.4219 C56.8438,262.9844 56.8125,262.8438 56.7344,262.6875 C56.5625,262.4375 56.2813,262.2813 56,262.2813 C55.7344,262.2813 55.4688,262.3906 55.2969,262.6406 C55.1875,262.7969 55.1563,262.9375 55.1563,263.4219 L55.1563,264.2969 L50.7969,264.2969 L50.7969,261.875 Z " fill="#000000"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="82" x="69" y="265.8467">schoolClass</text><line style="stroke:#181818;stroke-width:0.5;" x1="24.5" x2="166.5" y1="277" y2="277"/><ellipse cx="34.5" cy="288" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="40" x="43.5" y="293.9951">name</text><ellipse cx="34.5" cy="304.2969" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="104" x="43.5" y="310.292">List&lt;student&gt;</text><line style="stroke:#181818;stroke-width:1.0;" x1="24.5" x2="166.5" y1="317.5938" y2="317.5938"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="132" x="29.5" y="334.5889">schoolClass(name)</text><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="81" x="29.5" y="350.8857">printInfos()</text></g><!--MD5=[e6f955026f95a25accb184d80cb741ec]
class student--><g id="elem_student"><rect codeLine="12" fill="#F1F1F1" height="178.375" id="student" rx="2.5" ry="2.5" style="stroke:#181818;stroke-width:0.5;" width="269" x="162" y="7"/><ellipse cx="264.25" cy="23" fill="#ADD1B2" rx="11" ry="11" style="stroke:#181818;stroke-width:1.0;"/><path d="M263.0469,23.875 L264.5,23.875 L264.5,23.9844 C264.5,24.3906 264.5313,24.5469 264.6094,24.7031 C264.7656,24.9531 265.0469,25.1094 265.3438,25.1094 C265.5938,25.1094 265.8594,24.9688 266.0156,24.75 C266.1406,24.5938 266.1719,24.4375 266.1719,23.9844 L266.1719,22.0625 C266.1719,21.9063 266.1719,21.8594 266.1563,21.7031 C266.0938,21.2344 265.7813,20.9219 265.3281,20.9219 C265.0781,20.9219 264.8125,21.0625 264.6406,21.2813 C264.5313,21.4531 264.5,21.6094 264.5,22.0625 L264.5,22.1875 L263.0469,22.1875 L263.0469,19.7813 L267.0313,19.7813 L267.0313,20.6406 C267.0313,21.0469 267.0625,21.2188 267.1406,21.375 C267.3125,21.625 267.5938,21.7813 267.875,21.7813 C268.1406,21.7813 268.4063,21.6406 268.5781,21.4219 C268.6875,21.25 268.7188,21.1094 268.7188,20.6406 L268.7188,18.0938 L261.0938,18.0938 C260.6563,18.0938 260.5313,18.1094 260.375,18.2031 C260.125,18.3594 259.9688,18.6563 259.9688,18.9375 C259.9688,19.2188 260.1094,19.4688 260.3281,19.6406 C260.4844,19.75 260.6719,19.7813 261.0938,19.7813 L261.3438,19.7813 L261.3438,26.2969 L261.0938,26.2969 C260.6875,26.2969 260.5313,26.3125 260.375,26.4219 C260.125,26.5938 259.9688,26.8594 259.9688,27.1563 C259.9688,27.4219 260.1094,27.6719 260.3281,27.8281 C260.4688,27.9531 260.7031,28 261.0938,28 L269.0938,28 L269.0938,25.4219 C269.0938,24.9844 269.0625,24.8438 268.9844,24.6875 C268.8125,24.4375 268.5313,24.2813 268.25,24.2813 C267.9844,24.2813 267.7188,24.3906 267.5469,24.6406 C267.4375,24.7969 267.4063,24.9375 267.4063,25.4219 L267.4063,26.2969 L263.0469,26.2969 L263.0469,23.875 Z " fill="#000000"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="56" x="284.75" y="27.8467">student</text><line style="stroke:#181818;stroke-width:0.5;" x1="163" x2="430" y1="39" y2="39"/><ellipse cx="173" cy="50" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="144" x="182" y="55.9951">firstName: Property</text><ellipse cx="173" cy="66.2969" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="233" x="182" y="72.292">lastName: Property (private set)</text><ellipse cx="173" cy="82.5938" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="117" x="182" y="88.5889">eMail?: Property</text><ellipse cx="173" cy="98.8906" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="53" x="182" y="104.8857">Gender</text><ellipse cx="173" cy="115.1875" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="55" x="182" y="121.1826">address</text><line style="stroke:#181818;stroke-width:1.0;" x1="163" x2="430" y1="128.4844" y2="128.4844"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="257" x="168" y="145.4795">student(firstName, lastName, eMail)</text><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="81" x="168" y="161.7764">printInfos()</text><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="150" x="168" y="178.0732">addStudent(student)</text></g><!--MD5=[8739cefb37fa88296c52d839cadb2af0]
class address--><g id="elem_address"><rect codeLine="24" fill="#F1F1F1" height="113.1875" id="address" rx="2.5" ry="2.5" style="stroke:#181818;stroke-width:0.5;" width="187" x="203" y="245"/><ellipse cx="264.75" cy="261" fill="#ADD1B2" rx="11" ry="11" style="stroke:#181818;stroke-width:1.0;"/><path d="M263.5469,261.875 L265,261.875 L265,261.9844 C265,262.3906 265.0313,262.5469 265.1094,262.7031 C265.2656,262.9531 265.5469,263.1094 265.8438,263.1094 C266.0938,263.1094 266.3594,262.9688 266.5156,262.75 C266.6406,262.5938 266.6719,262.4375 266.6719,261.9844 L266.6719,260.0625 C266.6719,259.9063 266.6719,259.8594 266.6563,259.7031 C266.5938,259.2344 266.2813,258.9219 265.8281,258.9219 C265.5781,258.9219 265.3125,259.0625 265.1406,259.2813 C265.0313,259.4531 265,259.6094 265,260.0625 L265,260.1875 L263.5469,260.1875 L263.5469,257.7813 L267.5313,257.7813 L267.5313,258.6406 C267.5313,259.0469 267.5625,259.2188 267.6406,259.375 C267.8125,259.625 268.0938,259.7813 268.375,259.7813 C268.6406,259.7813 268.9063,259.6406 269.0781,259.4219 C269.1875,259.25 269.2188,259.1094 269.2188,258.6406 L269.2188,256.0938 L261.5938,256.0938 C261.1563,256.0938 261.0313,256.1094 260.875,256.2031 C260.625,256.3594 260.4688,256.6563 260.4688,256.9375 C260.4688,257.2188 260.6094,257.4688 260.8281,257.6406 C260.9844,257.75 261.1719,257.7813 261.5938,257.7813 L261.8438,257.7813 L261.8438,264.2969 L261.5938,264.2969 C261.1875,264.2969 261.0313,264.3125 260.875,264.4219 C260.625,264.5938 260.4688,264.8594 260.4688,265.1563 C260.4688,265.4219 260.6094,265.6719 260.8281,265.8281 C260.9688,265.9531 261.2031,266 261.5938,266 L269.5938,266 L269.5938,263.4219 C269.5938,262.9844 269.5625,262.8438 269.4844,262.6875 C269.3125,262.4375 269.0313,262.2813 268.75,262.2813 C268.4844,262.2813 268.2188,262.3906 268.0469,262.6406 C267.9375,262.7969 267.9063,262.9375 267.9063,263.4219 L267.9063,264.2969 L263.5469,264.2969 L263.5469,261.875 Z " fill="#000000"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="55" x="285.25" y="265.8467">address</text><line style="stroke:#181818;stroke-width:0.5;" x1="204" x2="389" y1="277" y2="277"/><ellipse cx="214" cy="288" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="45" x="223" y="293.9951">street</text><ellipse cx="214" cy="304.2969" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="27" x="223" y="310.292">city</text><ellipse cx="214" cy="320.5938" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="22" x="223" y="326.5889">zip</text><line style="stroke:#181818;stroke-width:1.0;" x1="204" x2="389" y1="333.8906" y2="333.8906"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="175" x="209" y="350.8857">address(street, city, zip)</text></g><!--MD5=[075979526866cabf2dc6d6167ae50c7d]
class program--><g id="elem_program"><rect codeLine="32" fill="#F1F1F1" height="80.5938" id="program" rx="2.5" ry="2.5" style="stroke:#181818;stroke-width:0.5;" width="177" x="7" y="418"/><ellipse cx="60.25" cy="434" fill="#ADD1B2" rx="11" ry="11" style="stroke:#181818;stroke-width:1.0;"/><path d="M59.0469,434.875 L60.5,434.875 L60.5,434.9844 C60.5,435.3906 60.5313,435.5469 60.6094,435.7031 C60.7656,435.9531 61.0469,436.1094 61.3438,436.1094 C61.5938,436.1094 61.8594,435.9688 62.0156,435.75 C62.1406,435.5938 62.1719,435.4375 62.1719,434.9844 L62.1719,433.0625 C62.1719,432.9063 62.1719,432.8594 62.1563,432.7031 C62.0938,432.2344 61.7813,431.9219 61.3281,431.9219 C61.0781,431.9219 60.8125,432.0625 60.6406,432.2813 C60.5313,432.4531 60.5,432.6094 60.5,433.0625 L60.5,433.1875 L59.0469,433.1875 L59.0469,430.7813 L63.0313,430.7813 L63.0313,431.6406 C63.0313,432.0469 63.0625,432.2188 63.1406,432.375 C63.3125,432.625 63.5938,432.7813 63.875,432.7813 C64.1406,432.7813 64.4063,432.6406 64.5781,432.4219 C64.6875,432.25 64.7188,432.1094 64.7188,431.6406 L64.7188,429.0938 L57.0938,429.0938 C56.6563,429.0938 56.5313,429.1094 56.375,429.2031 C56.125,429.3594 55.9688,429.6563 55.9688,429.9375 C55.9688,430.2188 56.1094,430.4688 56.3281,430.6406 C56.4844,430.75 56.6719,430.7813 57.0938,430.7813 L57.3438,430.7813 L57.3438,437.2969 L57.0938,437.2969 C56.6875,437.2969 56.5313,437.3125 56.375,437.4219 C56.125,437.5938 55.9688,437.8594 55.9688,438.1563 C55.9688,438.4219 56.1094,438.6719 56.3281,438.8281 C56.4688,438.9531 56.7031,439 57.0938,439 L65.0938,439 L65.0938,436.4219 C65.0938,435.9844 65.0625,435.8438 64.9844,435.6875 C64.8125,435.4375 64.5313,435.2813 64.25,435.2813 C63.9844,435.2813 63.7188,435.3906 63.5469,435.6406 C63.4375,435.7969 63.4063,435.9375 63.4063,436.4219 L63.4063,437.2969 L59.0469,437.2969 L59.0469,434.875 Z " fill="#000000"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="62" x="80.75" y="438.8467">program</text><line style="stroke:#181818;stroke-width:0.5;" x1="8" x2="183" y1="450" y2="450"/><line style="stroke:#181818;stroke-width:1.0;" x1="8" x2="183" y1="458" y2="458"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="165" x="13" y="474.9951">addClassAndStudents()</text><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="163" x="13" y="491.292">printInfos(schoolClass)</text></g><!--MD5=[a4ea2dcb44ebd57046df9af95d8b8c12]
class Gender--><g id="elem_Gender"><rect codeLine="38" fill="#F1F1F1" height="88.8906" id="Gender" rx="2.5" ry="2.5" style="stroke:#181818;stroke-width:0.5;" width="85" x="425" y="257"/><ellipse cx="440" cy="273" fill="#EB937F" rx="11" ry="11" style="stroke:#181818;stroke-width:1.0;"/><path d="M438.7969,273.875 L440.25,273.875 L440.25,273.9844 C440.25,274.3906 440.2813,274.5469 440.3594,274.7031 C440.5156,274.9531 440.7969,275.1094 441.0938,275.1094 C441.3438,275.1094 441.6094,274.9688 441.7656,274.75 C441.8906,274.5938 441.9219,274.4375 441.9219,273.9844 L441.9219,272.0625 C441.9219,271.9063 441.9219,271.8594 441.9063,271.7031 C441.8438,271.2344 441.5313,270.9219 441.0781,270.9219 C440.8281,270.9219 440.5625,271.0625 440.3906,271.2813 C440.2813,271.4531 440.25,271.6094 440.25,272.0625 L440.25,272.1875 L438.7969,272.1875 L438.7969,269.7813 L442.7813,269.7813 L442.7813,270.6406 C442.7813,271.0469 442.8125,271.2188 442.8906,271.375 C443.0625,271.625 443.3438,271.7813 443.625,271.7813 C443.8906,271.7813 444.1563,271.6406 444.3281,271.4219 C444.4375,271.25 444.4688,271.1094 444.4688,270.6406 L444.4688,268.0938 L436.8438,268.0938 C436.4063,268.0938 436.2813,268.1094 436.125,268.2031 C435.875,268.3594 435.7188,268.6563 435.7188,268.9375 C435.7188,269.2188 435.8594,269.4688 436.0781,269.6406 C436.2344,269.75 436.4219,269.7813 436.8438,269.7813 L437.0938,269.7813 L437.0938,276.2969 L436.8438,276.2969 C436.4375,276.2969 436.2813,276.3125 436.125,276.4219 C435.875,276.5938 435.7188,276.8594 435.7188,277.1563 C435.7188,277.4219 435.8594,277.6719 436.0781,277.8281 C436.2188,277.9531 436.4531,278 436.8438,278 L444.8438,278 L444.8438,275.4219 C444.8438,274.9844 444.8125,274.8438 444.7344,274.6875 C444.5625,274.4375 444.2813,274.2813 444,274.2813 C443.7344,274.2813 443.4688,274.3906 443.2969,274.6406 C443.1875,274.7969 443.1563,274.9375 443.1563,275.4219 L443.1563,276.2969 L438.7969,276.2969 L438.7969,273.875 Z " fill="#000000"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="53" x="454" y="277.8467">Gender</text><line style="stroke:#181818;stroke-width:0.5;" x1="426" x2="509" y1="289" y2="289"/><ellipse cx="436" cy="300" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="33" x="445" y="305.9951">Male</text><ellipse cx="436" cy="316.2969" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="52" x="445" y="322.292">Female</text><ellipse cx="436" cy="332.5938" fill="#000000" rx="3" ry="3" style="stroke:#000000;stroke-width:1.0;"/><text fill="#000000" font-family="sans-serif" font-size="14" lengthAdjust="spacing" textLength="20" x="445" y="338.5889">NA</text></g><!--MD5=[41da0079030c6b6890a6f9bc194dbf61]
link student to Gender--><g id="link_student_Gender"><path codeLine="44" d="M370.509,185.074 C386.472,204.072 402.981,223.718 417.752,241.297 " fill="none" id="student-to-Gender" style="stroke:#181818;stroke-width:1.0;"/><polygon fill="none" points="423.257,236.967,430.765,256.783,412.539,245.974,423.257,236.967" style="stroke:#181818;stroke-width:1.0;"/></g><!--MD5=[8cf7200d44918257a01f8dbef34baaa1]
link student to address--><g id="link_student_address"><path codeLine="45" d="M296.5,185.074 C296.5,203.473 296.5,222.48 296.5,239.628 " fill="none" id="student-to-address" style="stroke:#181818;stroke-width:1.0;"/><polygon fill="#181818" points="296.5,244.887,300.5,235.887,296.5,239.887,292.5,235.887,296.5,244.887" style="stroke:#181818;stroke-width:1.0;"/></g><!--MD5=[a1be4254a51d271e06b3f8f5eda52839]
link student to schoolClass--><g id="link_student_schoolClass"><path codeLine="46" d="M201.075,193.6112 C201.075,193.6112 172.534,222.508 159.64,235.562 " fill="none" id="student-schoolClass" style="stroke:#181818;stroke-width:1.0;"/><polygon fill="#181818" points="150.429,244.887,159.5995,241.2951,153.9428,241.3298,153.908,235.6731,150.429,244.887" style="stroke:#181818;stroke-width:1.0;"/><line style="stroke:#181818;stroke-width:1.0;" x1="153.9428" x2="159.565" y1="241.3298" y2="235.6385"/><polygon fill="none" points="209.508,185.074,202.4457,186.5316,201.075,193.6112,208.1372,192.1536,209.508,185.074" style="stroke:#181818;stroke-width:1.0;"/></g><!--MD5=[28c039d7692f367dbfaaca45110924c3]
link schoolClass to program--><g id="link_schoolClass_program"><path codeLine="47" d="M95.5,358.021 C95.5,375.97 95.5,395.633 95.5,412.7038 " fill="none" id="schoolClass-to-program" style="stroke:#181818;stroke-width:1.0;"/><polygon fill="#181818" points="95.5,417.9137,99.5,408.9137,95.5,412.9137,91.5,408.9137,95.5,417.9137" style="stroke:#181818;stroke-width:1.0;"/></g></svg>

http://www.plantuml.com/plantuml/png/PL5RQiCm4FpNAVIf5Fi2fRW9XPP24mezWOZMjK0FerqNqiVTArcI4Ob_F1xFpCvAUoGPQB66JLe11plJXLkWoIjigL63YGm3Hpf-uddotZmPYSX_68_FdPCMCdhJI0z8YuhYOQNwla_lrYQIVhUoSz2ENjteKUpOtpQZ9DJyKGUaKpJTy_VWPmXqoJ-ClEQvY95Vae0Zq2whu2YrURpsle1J43AMIWL0R0lN3c8Rtd4ZAreZRrR8H0pxADwIqQvMHjNLa080PNpMjTktdjikrIIokkD9sV6eQfFdu3-3j3cvjQw7Vd2r5OgRuitbbCJ5ydn0jUXH6iZe6Uo6JuSahzVLzxz3iJNSKEjld4zaCvCrE1dgAcmVinPh_W40

#### Student:
* ``lastName`` ist ein Pflichtfeld, wenn ``null`` soll ein Error geworfen werden.
* ``printInfo`` soll alle Daten in einer frei gestaltbaren Zeichenfolge in der Konsole ausgeben.

#### SchoolClass:
* ``addStudent`` soll einen Student der Liste hinzufügen.
* ``printInfo`` soll alle Daten in einer frei gestaltbaren Zeichenfolge in der Konsole ausgeben.

#### Program:
* ``printInfo`` soll die Infos der Klasse und die Infos aller Studenten ausgeben.
* ``addClassAndStudents`` soll eine Klasse mit einem beliebigen Namen (z.B. 5CHIF) und 3 Studenten mit beliebigen Daten der Klasse hinzufügen.
* Die beiden Methoden ``printInfo`` und ``addClassAndStudent`` sollen aufgerufen werden.
