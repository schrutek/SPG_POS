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
