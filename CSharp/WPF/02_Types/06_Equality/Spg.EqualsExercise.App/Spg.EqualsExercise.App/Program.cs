using System;
using System.Collections.Generic;

namespace Spg.EqualsExercise.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // HTL Wien V
            PhoneNr nr1 = new PhoneNr(01, 54615);
            // BMBWF
            PhoneNr nr2 = new PhoneNr(01, 53120);
            // Handynummer
            PhoneNr nr3 = new PhoneNr(0699, 99999999);
            // HTL Wien V
            PhoneNr nr4 = new PhoneNr(01, 54615);

            Console.WriteLine($"nr1 ist ident mit nr2?:           {nr1.Equals(nr2)}");
            Console.WriteLine($"nr1 ist ident mit nr4?:           {nr1.Equals(nr4)}");
            Console.WriteLine($"nr1 ist ident mit (object) nr4?:  {nr1.Equals((object)nr4)}");
            Console.WriteLine($"nr3 ist ident mit null?:          {nr3.Equals(null)}");
            Console.WriteLine($"nr3 ist größer als n4?:           {nr3.CompareTo(nr4) > 0}");
            Console.WriteLine($"nr3 ist größer als n4?:           {nr3 > nr4}");

            Console.WriteLine($"Hash von nr1:           {nr1.GetHashCode()}");
            Console.WriteLine($"Hash von nr4:           {nr4.GetHashCode()}");

            List<PhoneNr> numbers = new List<PhoneNr>() { nr1, nr2, nr3, nr4 };
            numbers.Sort();
            foreach (PhoneNr n in numbers)
            {
                Console.WriteLine(n);
            }
        }
    }
}
