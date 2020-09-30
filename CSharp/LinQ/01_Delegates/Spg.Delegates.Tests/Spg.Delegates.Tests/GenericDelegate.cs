using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Delegates.Tests
{
    public class GenericDelegate
    {
        private bool GreatMethodForNearlyEverything(MyFunction<int, int, bool> handler, int x, int y)
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

        private int GreatMethodForNearlyEverything2(MyFunction<int, int, string, int> handler, int a, int b, string s)
        {
            Console.Write(s);
            return a * b;
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
            bool result = GreatMethodForNearlyEverything(CompareEqual, 5, 5);

            Console.WriteLine(result);
        }
    }
}
