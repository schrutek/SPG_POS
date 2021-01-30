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

    public class MyPersonList<T> : List<T>
        where T : Person, new()
    {
        /// <summary>
        /// Mein überladener Indexer, der die Person nach dem Zunamen sucht
        /// </summary>
        /// <param name="zuname"></param>
        /// <returns></returns>
        public T this[string zuname]
        {
            get
            {
                foreach (T item in this)
                {
                    if (item.Zuname == zuname)
                    {
                        return item;
                    }
                }
                return null;
            }
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            MyPersonList<Person> list = new MyPersonList<Person>()
            {
                new Person() {Id = 1, Zuname = "Zuname1", Vorname = "Vorname1"},
                new Person() {Id = 2, Zuname = "Zuname2", Vorname = "Vorname2"},
                new Person() {Id = 3, Zuname = "Zuname3", Vorname = "Vorname3"},
            };

            Person nummer2 = list["Zuname2"];
        }
    }
}
