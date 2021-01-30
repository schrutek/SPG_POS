using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace SPG._3BHIF.Equals
{
    class PhoneNr : IEquatable<PhoneNr>, IComparable<PhoneNr>, IComparable
    {
        public long Vorwahl { get; }

        public long Telefonummer { get; }

        public PhoneNr(long vorwahl, long telefonnummer)
        {
            this.Vorwahl = vorwahl;
            this.Telefonummer = telefonnummer;
        }

        public int CompareTo(object obj)
        {
            return CompareTo(obj);
        }

        /// <summary>
        /// zuerst die Vorwahl prüfen, wenn die gleich ist, die Telefonnummer vergleichen. 
        /// Wenn die Vorwahl schon ungleich ist ist die Prüfung schon angeschlossen.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo([AllowNull] PhoneNr other)
        {
            int result = Vorwahl.CompareTo(other?.Vorwahl);
            if (result == 0)
            {
                return Telefonummer.CompareTo(other?.Telefonummer);
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            return Equals((PhoneNr)obj);
        }

        /// <summary>
        /// Einfach mit logischem UND verknüpfen. Beides liefert boolsche Werte, kann man einfach boolsch verunden
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals([AllowNull] PhoneNr other)
        {
            return Vorwahl.Equals(other?.Vorwahl) && Telefonummer.Equals(other?.Telefonummer);
        }

        public override int GetHashCode()
        {
            return Vorwahl.GetHashCode() ^ Telefonummer.GetHashCode();
        }

        public override string ToString()
        {
            return $"0{Vorwahl.ToString()} / {Telefonummer}";
        }

        public static bool operator < (PhoneNr nr1, PhoneNr nr2) => nr1.CompareTo(nr2) < 0;

        public static bool operator > (PhoneNr nr1, PhoneNr nr2) => nr1.CompareTo(nr2) > 0;

        public static bool operator <= (PhoneNr nr1, PhoneNr nr2) => nr1.CompareTo(nr2) <= 0;

        public static bool operator >= (PhoneNr nr1, PhoneNr nr2) => nr1.CompareTo(nr2) >= 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection collection = new ServiceCollection();
            collection.AddScoped<IService, Service01>();
            collection.AddScoped<IService, Service02>();




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
