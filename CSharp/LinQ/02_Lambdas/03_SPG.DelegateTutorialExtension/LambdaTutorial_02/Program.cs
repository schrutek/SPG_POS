using System;
using System.Collections;
using System.Collections.Generic;
//using System.Linq;

namespace LambdaTutorial_02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<Schueler> sl = new List<Schueler>();
            sl.Add(new Schueler() { Id = 1, Name = "Muster", Vorname = "Max", Klasse = "3AHIF" });
            sl.Add(new Schueler() { Id = 2, Name = "Muster1", Vorname = "Max", Klasse = "3AHIF" });
            sl.Add(new Schueler() { Id = 3, Name = "Muster2", Vorname = "Max", Klasse = "3AHIF" });
            sl.Add(new Schueler() { Id = 4, Name = "Muster3", Vorname = "Max", Klasse = "4AHIF" });

            var result = sl.Where(s => s.Klasse == "3AHIF");
        }
    }

    public static class ListExtensions
    {
        /// <summary>
        /// Meine Lambda-Methode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> Where<T>(this IEnumerable<T> data, Func<T, bool> predicate)
        {
            List<T> result = new List<T>();
            foreach (T item in data)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }
            return result;
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
