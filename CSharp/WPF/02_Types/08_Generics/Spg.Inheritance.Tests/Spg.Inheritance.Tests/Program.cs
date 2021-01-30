using System;

namespace Spg.Inheritance.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Person person = new Person();
            person.Name = "Martin Schrutek";
            Console.WriteLine(person.GetFullName());

            Teacher teacher = new Teacher();
            Console.WriteLine(teacher.GetFullName());

            Pupil pupil = new Pupil();
            Console.WriteLine(pupil.GetFullName());
        }
    }
}