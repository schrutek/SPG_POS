using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelDemoApp.Model
{
    /// <summary>
    /// Simuliert eine Datenbank mit einer Tabelle Personen.
    /// </summary>
    public class PersonDb
    {
        /// <summary>
        /// Tabelle mit den Personendaten.
        /// </summary>
        public ICollection<Person> Persons { get; private set; }
        /// <summary>
        /// Verhindert eine direkte Instanzierung mit einer null Liste.
        /// </summary>
        private PersonDb() { }
        /// <summary>
        /// Füllt die Instanz mit Musterdaten und gibt sie zurück.
        /// </summary>
        /// <returns></returns>
        public static PersonDb FromMockup()
        {
            PersonDb personDb = new PersonDb();
            personDb.Persons = new List<Person>();

            personDb.Persons.Add(new Person { Firstname = "Sonja", Lastname = "Mayer", DateOfBirth = new DateTime(2000, 1, 21), Sex = Sex.Male });
            personDb.Persons.Add(new Person { Firstname = "Jasmin", Lastname = "Hofer", DateOfBirth = new DateTime(2001, 1, 22), Sex = Sex.Male });
            personDb.Persons.Add(new Person { Firstname = "Manuel", Lastname = "Hoffer", DateOfBirth = new DateTime(2002, 1, 23), Sex = Sex.Female });
            personDb.Persons.Add(new Person { Firstname = "Stefan", Lastname = "Müller", DateOfBirth = new DateTime(2003, 1, 24), Sex = Sex.Male });
            personDb.Persons.Add(new Person { Firstname = "Martin", Lastname = "Schrutek", DateOfBirth = new DateTime(2004, 1, 25), Sex = Sex.Female });
            return personDb;
        }
    }
}
