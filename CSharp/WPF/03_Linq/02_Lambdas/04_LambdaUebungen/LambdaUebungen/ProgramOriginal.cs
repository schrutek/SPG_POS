using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// ÜBUNG ZU LAMBDA EXPRESSIONS
/// 
/// KORREKTE AUSGABE:
/// Beispiel 1: Converter
/// 263.15
/// 273.15
/// 283.15
/// 293.15
/// 303.15
/// Beispiel 2: Filter
/// 263.15
/// Beispiel 3: Division
/// 0
/// Es wurde versucht, durch 0 (null) zu teilen.
/// 0
/// Beispiel 4: Callback Funktion
/// Hello World.
/// Hello World again.
/// Programm mit Fehlern beendet.
/// </summary>
namespace LambdaUebungen
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beispiel 1: Converter");
            decimal[] converted = LambdaTest.Converter(new decimal[] { -10, 0, 10, 20, 30 }, CelsiusToKelvin);
            LambdaTest.ForEach(converted, PrintValue);

            Console.WriteLine("Beispiel 2: Filter");
            decimal[] freezed = LambdaTest.Filter(converted, WasserGefriert);
            LambdaTest.ForEach(freezed, PrintValue);

            Console.WriteLine("Beispiel 3: Division");
            decimal result = LambdaTest.ArithmeticOperation(2, 0, DivideSafe);
            Console.WriteLine(result);
            result = LambdaTest.ArithmeticOperation(2, 0, Divide, PrintError);
            Console.WriteLine(result);

            Console.WriteLine("Beispiel 4: Callback Funktion");
            LambdaTest.RunCommand(SayHello);

            Console.ReadLine();
        }

        public static decimal CelsiusToKelvin(decimal value)
        {
            return value + 273.15M;
        }

        public static void PrintValue(decimal value)
        {
            Console.WriteLine(value);
        }

        public static decimal Divide(decimal x, decimal y)
        {
            return x / y;
        }

        public static decimal DivideSafe(decimal x, decimal y)
        {
            if (y == 0) { return 0; }
            return x / y;
        }

        public static void PrintError(string message)
        {
            Console.Error.WriteLine(message);
        }

        public static void SayHello()
        {
            Console.WriteLine("Hello World.");
            Console.WriteLine("Hello World again.");
        }

        public static bool WasserGefriert(decimal val)
        {
            return val < 273.15M;
        }
    }
}