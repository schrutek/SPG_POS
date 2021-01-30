using System;

namespace Spg.Equality.Examples
{

    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Person firstPerson = new Person() { Id = 1, Name = "Martin Schrutek" };

            Person secondPerson = new Person() { Id = 2, Name = "Martin Schrutek" };

            Console.WriteLine(firstPerson.Equals(secondPerson));

            secondPerson = firstPerson;

            Console.WriteLine(firstPerson.Equals(secondPerson));

            string a = "Hallo";

            string b = "Hallo";

            Console.WriteLine(a.Equals(b));
        }
    }
}
