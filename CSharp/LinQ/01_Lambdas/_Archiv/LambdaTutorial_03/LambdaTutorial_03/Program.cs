using System;
using System.Collections.Generic;

namespace LambdaTutorial_03
{
    public class SampleCollection<T>
    {
        private T[] array = new T[100];

        public T this[int index] { get { return array[index]; } set { array[index] = value; } }
    }

    public class SchuelerList : List<Schueler>
    {
        public delegate bool FilterHandler(Schueler s);

        public SchuelerList Filter(FilterHandler predicate)
        {
            SchuelerList resultList = new SchuelerList();
            foreach (Schueler schueler in this)
            {
                if (predicate(schueler))
                {
                    resultList.Add(schueler);
                }
            }
            return resultList;
        }

        public SchuelerList Where(Func<Schueler, bool> predicate)
        {
            SchuelerList resultList = new SchuelerList();
            foreach (Schueler schueler in this)
            {
                if (predicate(schueler))
                {
                    resultList.Add(schueler);
                }
            }
            return resultList;
        }

        public int TestFunc(Func<int, int, int> predicate)
        {
            return predicate(this[2].Id, this[3].Id);
        }
    }

    public class Program
    {
        private static bool SchuelerFilterMethod(Schueler schueler)
        {
            return schueler.Klasse == "3AHIF";
        }

        public static void Main(string[] args)
        {
            SchuelerList sl = new SchuelerList();
            sl.Add(new Schueler() { Id = 1, Name = "Muster", Vorname = "Max", Klasse = "3AHIF" });
            sl.Add(new Schueler() { Id = 2, Name = "Muster1", Vorname = "Max", Klasse = "3AHIF" });
            sl.Add(new Schueler() { Id = 3, Name = "Muster2", Vorname = "Max", Klasse = "3AHIF" });
            sl.Add(new Schueler() { Id = 4, Name = "Muster3", Vorname = "Max", Klasse = "4AHIF" });

            SchuelerList result = null;
            SchuelerList result2 = null;

            result = sl.Filter(SchuelerFilterMethod);

            result2 = sl.Where(s => s.Klasse == "4AHIF");

            int result3 = sl.TestFunc((idA, idB) => idA + idB);

            int result4 = sl.TestFunc(
                (idA, idB) =>
                {
                    if (idA == idB)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            );

            foreach (Schueler s in result)
            {
                Console.WriteLine(s.Id + " " + s.Name);
            }
        }
    }

    public class Schueler
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Vorname { get; set; }

        public string Klasse { get; set; }
    }
}
