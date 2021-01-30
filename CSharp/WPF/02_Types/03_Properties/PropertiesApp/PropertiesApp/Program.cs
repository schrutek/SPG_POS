using System;

namespace PropertiesApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool result;
            // Teste die Klasse Rechteck
            Rechteck r1 = new Rechteck();
            Rechteck r2 = new Rechteck() { Laenge = 10, Breite = 20 };

            // Fläche OK?
            result = (r2.Flaeche == 200);
            Console.WriteLine($"Fläche OK: {result}");
            // Ungültige Zuweisung wird richtig erkannt:
            try
            {
                r1.Laenge = -1;
                result = false;
            }
            catch (ArgumentException)
            {
                try
                {
                    r1.Breite = -1;
                    result = false;
                }
                catch (ArgumentException)
                {
                    result = true;
                }
            }
            Console.WriteLine($"Exception bei Länge und Breite OK: {result}");

            // TESTE DIE KLASSE LEHRER
            Lehrer l1 = new Lehrer() { Vorname = "Heinrich", Zuname = "Schlau", Bruttogehalt = 4000 };
            Lehrer l2 = new Lehrer() { Vorname = "Daniela", Zuname = "E" };
            Lehrer l3 = new Lehrer();
            // Teste die Initialisierung von Zuname
            result = (l3.Vorname == "" && l3.Zuname == null);
            Console.WriteLine($"Vor- und Zuname Initialisierung OK: {result}");
            // Teste das Property Bruttogehalt
            result = (l1.Bruttogehalt == 4000 && l2.Bruttogehalt == null);
            Console.WriteLine($"Bruttogehalt Initialisierung OK: {result}");
            // Teste die Zuweisung von Bruttogehalt
            l1.Bruttogehalt = 20;
            l2.Bruttogehalt = 2000;
            result = (l1.Bruttogehalt == 4000 && l2.Bruttogehalt == 2000);
            Console.WriteLine($"Bruttogehalt Zuweisung OK: {result}");
            // Teste das Nettogehalt
            result = (l1.Nettogehalt == 3200 && l3.Nettogehalt == 0);
            Console.WriteLine($"Nettogehalt OK: {result}");
            // Teste das Kürzel
            result = (l2.Kuerzel == "E" && l3.Kuerzel == "");
            Console.WriteLine($"Kuerzel OK: {result}");

            Console.ReadLine();
        }
    }
}
