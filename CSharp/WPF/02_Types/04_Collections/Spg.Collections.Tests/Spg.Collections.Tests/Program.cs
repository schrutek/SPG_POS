using System;
using System.Collections;
using System.Collections.Generic;

namespace Spg.Collections.Tests
{
    public class PersonList : List<Person>
    {
        public Person this [string vorname]
        {
            get
            {
                foreach (Person item in this)
                {
                    if (item?.Vorname?.ToLower() == vorname?.ToLower())
                    {
                        return item;
                    }
                }
                return null;
            }
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Zuname { get; set; }
        public string Vorname { get; set; }
        public override string ToString()
        {
            return $"{Id} {Zuname} {Vorname}";
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            PersonList myPersons =  new PersonList()
            {
                new Person() { Id = 3, Vorname = "Vorname 3", Zuname = "Zuname 3" },
                new Person() { Id = 1, Vorname = "Vorname 1", Zuname = "Zuname 1" },
                new Person() { Id = 2, Vorname = "Vorname 2", Zuname = "Zuname 2" },
                new Person() { Id = 4, Vorname = "Vorname 4", Zuname = "Zuname 4" }
            };
            myPersons.Add(null);

            Person myPerson = myPersons[1];
            myPerson = myPersons["Vorname 5"];

            foreach (Person p in myPersons)
            {
                
            }
        }
    }
}
