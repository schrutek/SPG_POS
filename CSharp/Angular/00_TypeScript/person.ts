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