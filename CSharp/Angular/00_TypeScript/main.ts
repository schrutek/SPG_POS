import { Person } from './person';

let person = new Person('Martin', 'NoName');
person.LastName = 'Schrutek';
person.eMail = 'schrutek@spengergasse.at';
person.writeInfo();
console.log(person.address.street);
