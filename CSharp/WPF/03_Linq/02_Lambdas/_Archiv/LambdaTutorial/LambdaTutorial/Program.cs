using System;
using System.Collections.Generic;

namespace LambdaTutorial
{
    public class Schueler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public string Klasse { get; set; }
    }

    public class SchuelerList : List<Schueler>
    {
        /// <summary>
        /// Ein delegate ist eine "Schablone", wie eine Funktion aussehen soll.
        /// delegate Rückgabetyp Name(Argumente)
        /// C# 1 (2001)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public delegate bool FilterDelegate(Schueler s);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterFunction">filterFunction muss bool zurückliefern
        /// und einen Schueler als Argument bekommen.</param>
        /// <returns></returns>
        public SchuelerList Filter(FilterDelegate filterFunction)
        {
            return null;
        }

        public SchuelerList Where(Func<Schueler, bool> filterFunction)
        {
            SchuelerList result = new SchuelerList();
            foreach (Schueler s in this)
            {
                if (filterFunction(s))
                {
                    result.Add(s);
                }
            }
            return result;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            SchuelerList sl = new SchuelerList();
            sl.Add(new Schueler() { Id = 1, Name = "Muster", Vorname = "Max", Klasse = "3AHIF" });
            sl.Add(new Schueler() { Id = 2, Name = "Muster1", Vorname = "Max", Klasse = "3AHIF" });
            sl.Add(new Schueler() { Id = 3, Name = "Muster2", Vorname = "Max", Klasse = "3AHIF" });
            sl.Add(new Schueler() { Id = 4, Name = "Muster3", Vorname = "Max", Klasse = "4AHIF" });

            // Variante 1: Eine benannte Funktion wird übergeben.
            SchuelerList result = sl.Where(MySchuelerFilter);

            foreach (Schueler s in result)
            {
                Console.WriteLine(s.Id + " " + s.Name);
            }
            
            // Dazugehörige Lambda Expression
            result = sl.Where(s => s.Klasse == "3AHIF");
            
            Console.ReadLine();
        }

        /// <summary>
        /// Lambda: s => s.Klasse == "3AHIF"
        ///       (s) => s.Klasse == "3AHIF"
        ///       (s) => { return s.Klasse == "3AHIF" }
        ///        s  => { return s.Klasse == "3AHIF" }
        /// Datentyp: Func<Schueler, bool>
        /// </summary>
        static bool MySchuelerFilter(Schueler s)
        {
            return s.Klasse == "3AHIF";
        }

        /// <summary>
        /// Wenn 2 Argumente übergeben werden, muss ich eine
        /// Klammer links vom => schreiben.
        /// 
        /// Lambda: (x, y) => x == y
        /// Datentyp: Func<int, int, bool>
        /// </summary>
        static bool Testfunc1(int x, int y)
        {
            return x == y;
        }

        /// <summary>
        /// Umfasst die Funktion mehr als 1 Befehl, muss der Body
        /// in { } gesetzt werden. Es wird kein return mehr
        /// automatisch eingebaut!
        /// Lambda: (x, y) => {
        ///             if (x != y) { return x + 1; }
        ///             else        { return x; }
        ///         }
        /// Datentyp: Func<int, int, int>
        /// </summary>
        static int Testfunc2(int x, int y)
        {
            if (x != y) { return x + 1; }
            else { return x; }
        }
    }
}
