using System;

namespace SPG.LambdaTutorial
{
    public class Program
    {
        public static bool MyFilterCondition(Schueler s) 
        { 
            return s.Klasse == "3AHIF"; 
        }

        public static void Main(string[] args)
        {
            // Eine Liste von Schülern wird erstellt und mittels initializer befüllt.
            SchuelerList schuelers = new SchuelerList()
            {
                new Schueler() { Id = 1, Nachame = "Muster1", Vorname = "Max1", Klasse = "3AHIF" },
                new Schueler() { Id = 2, Nachame = "Muster2", Vorname = "Max2", Klasse = "3AHIF" },
                new Schueler() { Id = 3, Nachame = "Muster3", Vorname = "Max3", Klasse = "3AHIF" },
                new Schueler() { Id = 4, Nachame = "Muster4", Vorname = "Max4", Klasse = "4AHIF" },
                new Schueler() { Id = 5, Nachame = "Muster5", Vorname = "Max5", Klasse = "5AHIF" }
            };

            SchuelerList result1 = null;
            SchuelerList result2 = null;

            //TODO: Implementierung!

            result1 = schuelers.Filter(MyFilterCondition);

            result2 = schuelers.Where(s => s.Klasse == "3AHIF");

            int result3 = schuelers.CalculateSum((idA, idB) => idA + idB);

            int result4 = schuelers.CalculateSum(
                (idA, idB) =>
                {
                    try
                    {
                        if (idA == 3)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                });

            // Ausgabe
            foreach (Schueler s in result1)
            {
                Console.WriteLine($"{s.Id}: {s.Vorname} {s.Nachame} ({s.Klasse})");
            }

            Console.WriteLine(result3);
            Console.WriteLine(result4);
        }
    }
}
