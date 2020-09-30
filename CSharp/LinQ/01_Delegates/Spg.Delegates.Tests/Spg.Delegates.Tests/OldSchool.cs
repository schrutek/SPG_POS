using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Delegates.Tests
{
    public class OldSchool
    {
        /// <summary>
        /// Diese Methode hat den Nachteil, dass der Vergleich fix gecodet ist.
        /// Möchte ich auf != vergleichen, muss ich die Methode ändern.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool GreatMethodForNearlyEverything(int x, int y)
        {
            // ...
            // 1000 Zeilen Code (z.B. irgendweche Vorbereitungen werden duhgeführt)
            // ...

            bool result = CompareEqual(x, y);

            // ...
            // 1000 Zeilen Code (z.B. result wird irgendwie verarbeitet)
            // ...

            return result;
        }

        private bool CompareEqual(int x, int y)
        {
            return (x == y);
        }
        private bool CompareNotEqual(int x, int y)
        {
            return (x != y);
        }

        public void DoSomeWork()
        {
            bool result = GreatMethodForNearlyEverything(5, 5);

            Console.WriteLine(result);
        }
    }
}
