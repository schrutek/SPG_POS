using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSE.AnonymousFunctionsTest
{
    public class Functions
    {
        public delegate void MessageDelegate(string s);

        private void Write(string message)
        {
            Console.WriteLine(message);
        }

        // History of Lambdas
        public void Work()
        {
            MessageDelegate testDelA = new MessageDelegate(Write);
            testDelA("Test-Delegate .Net 1.0");


            MessageDelegate testDelB = delegate(string s) { Console.WriteLine(s); };
            testDelB("Test-Delegate ab .Net 2.0");


            MessageDelegate testDelC = (string s) => { Console.WriteLine(s); };
            testDelC("Test-Delegate ab .Net 3.0");

            MessageDelegate testDelC1 = s => Console.WriteLine(s);
            testDelC("Test-Delegate ab .Net 3.0");


            Action<string> testDelD = s => { Console.WriteLine(s); };
            testDelD("Anonyme Funktion 'Action'");

            Func<string, string> testDelE = s => { Console.WriteLine(s); return "'MyRetVal'"; };
            string retVal = testDelE("Anonyme Funktion 'Func' (also mit Rückgabewert)");
            Console.WriteLine("  -> Rückgabewert = " + retVal);


            // Expressions
            Expression<Func<int, int, int>> e = (x, y) => x * x + y;
            Func<int, int, int> squaredNumber = e.Compile();
            int result = squaredNumber(5, 2);
            Console.WriteLine(e + " | " + result);

            int[] numbers = { 2, 3, 4, 5 };
            IEnumerable<int> squaredNumbers = numbers.Select(x => x * x);
            Console.WriteLine(string.Join(" ", squaredNumbers));


            ActionMethod(s => Console.WriteLine(s));
        }

        private void ActionMethod(Action<string> myAction)
        {
            Console.WriteLine("ActionMethod Teil 1");

            myAction("ActionMethod Mittelteil");

            Console.WriteLine("ActionMethod Teil 3");
        }
    }
}
