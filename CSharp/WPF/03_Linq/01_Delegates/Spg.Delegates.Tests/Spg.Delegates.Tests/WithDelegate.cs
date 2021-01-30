using System;

namespace Spg.Delegates.Tests
{
    public class WithDelegate
    {
        public delegate bool CompareHandler(int x, int y);

        /// <summary>
        /// Die Lösung ist ein Delegate. Hier wird der Methode die Logik
        /// zum Vergleichen als Parameter mitgegeben. Ein Stück Logik
        /// wird über den Parameter in die Methode gegeben und dort 
        /// ausgeführt.
        /// </summary>
        /// <param name="handler">Die Methode welche die Vergleichslogik erhält</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private bool GreatMethodForNearlyEverything(CompareHandler handler, int x, int y)
        {
            // ...
            // 1000 Zeilen Code (z.B. irgendweche Vorbereitungen werden duhgeführt)
            // ...

            bool result = handler(x, y);

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
            bool result = GreatMethodForNearlyEverything(CompareNotEqual, 5, 5);

            Console.WriteLine(result);
        }
    }
}