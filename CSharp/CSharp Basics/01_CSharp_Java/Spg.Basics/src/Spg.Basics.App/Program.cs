using Spg.Basics.App.Classes;
using System;

namespace Spg.Basics.App
{
    public class Program
    {
        /// <summary>
        /// Startpunkt der Applikation
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Execute();

            Console.WriteLine("\r\n***** ALL DONE :) ******************************");
        }

        /// <summary>
        /// Ausführen der einzelnen Beispiele
        /// </summary>
        private static void Execute()
        {
            Examples examples = new Examples();

            examples.WriteMyColor();
            examples.SomethingAboutStrings();
            examples.UseAStruct();
            examples.ExampleWithObject();
            examples.OverflowChecks();
            examples.ForEachStatement();
            examples.OverrideExample();
            examples.SomeFunctiom();
            examples.SomeMoreFunction();
        }
    }
}
