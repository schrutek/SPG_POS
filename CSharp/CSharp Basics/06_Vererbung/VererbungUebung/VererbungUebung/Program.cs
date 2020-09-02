using System;

namespace VererbungUebung
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Klasse k1 = new Klasse("3AHIF") { Kv = new Lehrer(1, "GC") { Zuname = "Gründl" } };
            Schueler s1 = new Schueler(1) { Zuname = "ZN1" };
            Schueler s2 = new Schueler(2) { Zuname = "ZN2" };
            Schueler s3 = new Schueler(1) { Zuname = "ZN3" };

            try
            {
                k1.Add(s1);
                k1.Add(s2);
                k1.Add(s3);
            }
            catch (InvalidOperationException e)
            {
                Console.Error.WriteLine(e.Message);
            }

            Console.WriteLine(k1);
            k1.Remove(s1);
            Console.WriteLine(k1);
        }
    }
}
