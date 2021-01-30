using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.AnonymousFunctionsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Nun konvertieren wir eine Dezimalzahl.
            string strVal2 = "12.1";
            double doubleVal;
            // Im Deutschen ist , als Komma in Windows eingestellt. Wir
            // geben daher mit, dass die US Einstellung (.) als Culture
            // beim Parsen verwendet wird.
            // Ähnliche Probleme treten bei Datumswerten auf.
            if (!double.TryParse(strVal2, NumberStyles.Any, CultureInfo.InvariantCulture, out doubleVal))
            {
                Console.WriteLine("Fehler beim Konvertieren nach double");
            }



            Functions f = new Functions();
            f.Work();

            // Keep console window open in debug mode.
            Console.WriteLine("\r\n\r\n\r\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
