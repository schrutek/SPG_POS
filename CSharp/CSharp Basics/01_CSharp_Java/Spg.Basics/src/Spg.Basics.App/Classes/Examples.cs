using Spg.Basics.App.Enumerations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Spg.Basics.App.Classes
{
    public class Examples
    {
        /// <summary>
        /// Verwenden eineer Enumeration
        /// </summary>
        public void WriteMyColor()
        {
            Console.WriteLine("*** WriteMyColor ***");

            MyColor myColor = MyColor.Blue;
            Console.WriteLine(myColor);
        }

        /// <summary>
        /// Einige String-Operationen
        /// </summary>
        /// <remarks>
        /// Hier wird die Überöadung der Methode <code>WriteLine</code> direkt ein
        /// String-Objekt geschrieben. String lässt sich einfachmit "" Instanzieren.
        /// Natürlich geht das gleiche auch mit dem Typ <code>string</code>.
        /// z.B: <code>string s = String.Format("Hello {0} {1}", name, lastName)</code>.
        /// </remarks>
        public void SomethingAboutStrings()
        {
            Console.WriteLine("*** SomethingAboutStrings ***");

            string name = "Alfonso";
            Console.WriteLine(name);

            name = "Don" + " ";
            Console.WriteLine(name);

            name = name + "Alfonso";
            Console.WriteLine(name);

            Console.WriteLine(name[4]);

            Console.WriteLine(name.Length);

            if (name.Substring(0, 3).ToLower() == "don")
            {
                Console.WriteLine("ja, ist don");
            }
            else
            {
                Console.WriteLine("nein, ist was anderes");
            }

            string lastName = "Bauer";
            Console.WriteLine("Hello {0} {1}", name, lastName);
            Console.WriteLine($"Hello {name} {lastName}");

            Console.WriteLine("C:\\HTL\\3AHIF\\Readme.md");
            Console.WriteLine(@"C:\HTL\3AHIF\Readme.md");

            Console.WriteLine(
                @"Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod 
tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam 
et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem 
ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy 
eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos 
et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus 
est Lorem ipsum dolor sit amet."
                );

            // Hier wird, wie obenbeschrieben direkt in die Konsole geschreieben Das gleiche 
            // geht auch direkt mit String-Objekten:

            string myfirstLineText = String.Format("Hello {0} {1}", name, lastName);
            string myPath = String.Format(@"C:\HTL\3AHIF\Readme.md");
        }

        /// <summary>
        /// Structs sind Wertetpen, sie werden am Stack, nicht am Heap abgelegt.
        /// Structs sind speichersparender als Objekte aus Klassen.
        /// Weil es Wertetypen sind, können Structs nicht <code>NULL</code> sein.
        /// Structs können nicht vererbt werden und können nicht erben
        /// Structs können Interfaces implementieren.
        /// Structs können keine Default-Konstruktor enthalten.
        /// Structs müssen nicht unbedingt mit new Instanziert werden.
        /// </summary>
        public void UseAStruct()
        {
            Console.WriteLine("*** UseAStruct ***");

            Point p;
            //p = new Point(3, 4);    // Der Konstruktor initialisiert ein Objekt am Stack.

            // field access
            p.x = 1;
            p.y = 2;

            // method call
            p.MoveTo(10, 20);

            Point q = p;

            Console.WriteLine($"STRUCT Q: {q.x}, {q.y}");
        }

        /// <summary>
        /// Objekt werden in C# wie in Java behandelt.
        /// </summary>
        /// <remarks>
        /// Eine Besonderheit ist das sog. Boxing und unboxing. Hier kann, wenn es der
        /// Datentyp zulässt, ein Wertetyp in einen Refereztyp umgewandelt (geboxt ("eingeschachtelt")) werden
        /// und umgekehrt (unboxing). Das ist manchmal ganz praktisch, sollte aber mit Bedacht und nur
        /// selten verwendet werden, das es recht Laufzeitintensiv ist, da der Speicherinhalt vom Hep in 
        /// den Stack umgespeichert werden muss und umgekehrt!
        /// In diesem Beispiel wird ein Int-Objekt in einen Int umgewandelt.
        /// 
        /// Alle Objekte leiten von <code>object</code> ab und können natürlich zu diesem gecastet werden.
        /// (Mehr zu Vererbung später)
        /// </remarks>
        public void ExampleWithObject()
        {
            Console.WriteLine("*** ExampleWithObject ***");

            Rectangle rectangle = new Rectangle(30, 50);

            object rectangleObject = rectangle;

            int myValue = 3;

            object myObject = 5;

            myValue = (int)myObject;

            Console.WriteLine(myValue);
        }

        /// <summary>
        /// Standardmäßig werden overflows nicht überprüft, 
        /// eine Prüfung kann aber aktiviert werden <code>checked</code>.
        /// </summary>
        public void OverflowChecks()
        {
            Console.WriteLine("*** OverflowChecks ***");

            int index = 2147483647;
            Console.WriteLine(index);

            int x = 1000000;
            try
            {
                x = checked(x * x);          // -727379968, no error
            }
            catch (OverflowException ex)
            {
                Console.WriteLine("Zahl ist zu groß");
            }
            Console.WriteLine(x);
        }

        /// <summary>
        /// In C# gibt es das <code>foreach</code>.Keyword. Es kann alles Iterieren,
        /// wasdas Interface <code>IEnumerator</code> implementiert.
        /// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/iterators
        /// </summary>
        public void ForEachStatement()
        {
            Console.WriteLine("*** ForEachStatement ***");

            int[] indexes = { 2, 5, 123, 85, 39, 42, 9, 17, 41, 32, 51 };
            foreach (int index in indexes)
            {
                Console.Write(index + ", ");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Methoden in abgeleitetn Klassen können überschrieben werden. ISt das der 
        /// Fall sollte die Methode der Basisklasse mit dem Modifier <code>virtual</code>
        /// gekennzeichnet werden.
        /// Eine Methode kann mehrere Überladungen haben. D.h. der Metodenname ist gleich,
        /// aber die Parameter unterscheiden sich. 2 Methoden mit gleichm Namen und gelichen
        /// Parametern sind nicht zulässig.
        /// </summary>
        public void OverrideExample()
        {
            Console.WriteLine("*** OverrideExample ***");

            Person person = new Person() { FirstName = "Alfonso", LastName = "Bauer" };
            //person.FirstName = "Alfonso";
            //person.LastName = "Bauer";

            person.PrintName();
            person.PrintName(true);
        }

        public void SomeFunctiom()
        {
            int index = 3;
            CopyByValue(index); // val == 3
        }

        /// <summary>
        /// Der Wert wird in der Methode erhöht, der Parameter vom Aufrufer 
        /// ändert sich aber nicht. (bleibt 3)
        /// </summary>
        /// <param name="counter"></param>
        private void CopyByValue(int counter)
        {
            counter = counter + 1;
        }

        /// <summary>
        /// Der Wert wird in der Methode erhöht, der Parameter vom Aufrufer 
        /// ändert sich. (wird 4)
        /// </summary>
        /// <param name="counter"></param>
        private void CopyByReference(ref int counter)
        {
            counter = counter + 1;
        }

        /// <summary>
        /// Die beiden Parameter <code>first</code> und <code>next</code> sind
        /// in der Methode mit keinem Wert belöegt. Der Werte werden nach dem Aufruf 
        /// der Methode <code>SetValues</code> mit den Werten belegt.
        /// </summary>
        public void SomeMoreFunction()
        {
            int first;
            int next;

            Read(out first, out next);
        }

        /// <summary>
        /// Let die beiden Parameter fest und gibt die Werte an den Aufrufer zurück.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        private void Read(out int a, out int b)
        {
            a = 5;
            b = 12;
        }
    }
}
