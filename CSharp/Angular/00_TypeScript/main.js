"use strict";
exports.__esModule = true;
var person_1 = require("./person");
var person = new person_1.Person('Martin', 'NoName');
person.LastName = 'Schrutek';
person.eMail = 'schrutek@spengergasse.at';
person.writeInfo();
console.log(person.address.street);
