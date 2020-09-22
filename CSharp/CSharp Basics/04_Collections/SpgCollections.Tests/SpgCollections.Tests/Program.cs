using System;
using System.Collections.Generic;

namespace SpgCollections.Tests
{
    public class Person
    {
        public int Id { get; set; }
        public string Vorname { get; set; }
        public string Zuname { get; set; }
        public override string ToString()
        {
            return $"{Id} {Zuname} {Vorname}";
        }
    }

    public class MyPersonList : List<Person>
    {
        /// <summary>
        /// Mein überladener Indexer, der die Person nach dem Zunamen sucht
        /// </summary>
        /// <param name="zuname"></param>
        /// <returns></returns>
        public Person this[string zuname]
        {
            get
            {
                foreach (Person item in this)
                {
                    return item.Zuname == zuname ? item : null;
                }
                return new Person();
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            MyPersonList list = new MyPersonList()
            {
                new Person() {Id = 1, Zuname = "Zuname1", Vorname = "Vorname1"},
                new Person() {Id = 2, Zuname = "Zuname2", Vorname = "Vorname2"},
                new Person() {Id = 3, Zuname = "Zuname3", Vorname = "Vorname3"},
            };

            Person nummer2 = list["Zuname2"];
        }
    }
}
