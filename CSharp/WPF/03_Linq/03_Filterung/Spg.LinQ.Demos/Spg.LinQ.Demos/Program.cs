using Spg.LinQ.Demos.LinQ;
using System;
using System.Collections.Generic;
//using System.Linq;

namespace Spg.LinQ.Demos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Eine Liste von Schülern wird erstellt und mittels initializer befüllt.
            List<Schueler> schuelers = new List<Schueler>()
            {
                new Schueler() { Id = 1, Nachame = "Muster1", Vorname = "Max1", Klasse = "3AHIF" },
                new Schueler() { Id = 2, Nachame = "Muster2", Vorname = "Max2", Klasse = "3AHIF" },
                new Schueler() { Id = 3, Nachame = "Muster3", Vorname = "Max3", Klasse = "3AHIF" },
                new Schueler() { Id = 4, Nachame = "Muster4", Vorname = "Max4", Klasse = "4AHIF" },
                new Schueler() { Id = 5, Nachame = "Muster5", Vorname = "Max5", Klasse = "5AHIF" }
            };

            SchuelerList result = schuelers.Filter(s => s.Klasse == "3AHIF");


            //SchuelerList result1 = schuelers.Filter(s => s.Klasse == "3AHIF");
            //IEnumerable<Schueler> result2 = schuelers.Filter2(s => s.Klasse == "3AHIF");
            //IEnumerable<Schueler> result3 = schuelers.Where(s => s.Klasse == "3AHIF");

            // Ausgabe
            foreach (Schueler s in result)
            {
                Console.WriteLine($"{s.Id}: {s.Vorname} {s.Nachame} ({s.Klasse})");
            }
        }

        public static bool CheckKlasse(Schueler s)
        {
            return s.Klasse == "3AHIF";
        }

    }
}
