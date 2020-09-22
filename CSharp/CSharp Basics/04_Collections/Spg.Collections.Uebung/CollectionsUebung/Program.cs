using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ExCollection.App
{
    public class Klasse
    {
        // TODO: Erstelle ein Property Schuelers, welches alle Schüler der Klasse in einer
        //       Liste speichert.
        // ...

        // TODO: Erstelle ein Default-Property "Name" (string)
        // ...

        // TODO: Erstelle ein Default-Property "KV" (string)
        // ...

        /// <summary>
        /// Fügt den Schüler zur Liste hinzu und setzt das Property KlasseNavigation
        /// des Schülers korrekt auf die aktuelle Instanz.
        /// </summary>
        /// <param name="s"></param>
        public void AddSchueler(Schueler s)
        {
            // TODO: Implementation
        }
    }
    public class Schueler
    {
        // TODO: Erstelle ein Proeprty KlasseNavigation vom Typ Klasse, welches auf
        //       die Klasse des Schülers zeigt.
        // Füge dann über das Proeprty die Zeile [JsonIgnore]
        // ein, damit der JSON Serializer das Objekt ausgeben kann.
        // ...

        // TODO: Erstelle ein Default-Property "Id" (int)
        // ...

        // TODO: Erstelle ein Default-Property "Vorname" (string)
        // ...

        // TODO: Erstelle ein Default-Property "Zuname" (string)
        // ...

        /// <summary>
        /// Ändert die Klassenzugehörigkeit, indem der Schüler
        /// aus der alten Klasse, die in KlasseNavigation gespeichert ist, entfernt wird.
        /// Danach wird der Schüler in die neue Klasse mit der korrekten Navigation eingefügt.
        /// </summary>
        /// <param name="k"></param>
        public void ChangeKlasse(Klasse k)
        {
            // TODO: Implementation
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Dictionary<string, Klasse> klassen = new Dictionary<string, Klasse>();
            klassen.Add("3AHIF", new Klasse() { Name = "3AHIF", KV = "KV1" });
            klassen.Add("3BHIF", new Klasse() { Name = "3BHIF", KV = "KV2" });
            klassen.Add("3CHIF", new Klasse() { Name = "3CHIF", KV = "KV3" });
            klassen["3AHIF"].AddSchueler(new Schueler() { Id = 1001, Vorname = "VN1", Zuname = "ZN1" });
            klassen["3AHIF"].AddSchueler(new Schueler() { Id = 1002, Vorname = "VN2", Zuname = "ZN2" });
            klassen["3AHIF"].AddSchueler(new Schueler() { Id = 1003, Vorname = "VN3", Zuname = "ZN3" });
            klassen["3BHIF"].AddSchueler(new Schueler() { Id = 1011, Vorname = "VN4", Zuname = "ZN4" });
            klassen["3BHIF"].AddSchueler(new Schueler() { Id = 1012, Vorname = "VN5", Zuname = "ZN5" });
            klassen["3BHIF"].AddSchueler(new Schueler() { Id = 1013, Vorname = "VN6", Zuname = "ZN6" });

            Schueler s = klassen["3AHIF"].Schuelers[0];
            Console.WriteLine($"s sitzt in der Klasse {s.KlasseNavigation.Name} mit dem KV {s.KlasseNavigation.KV}.");
            Console.WriteLine("3AHIF vor ChangeKlasse:");
            Console.WriteLine(JsonConvert.SerializeObject(klassen["3AHIF"].Schuelers));
            s.ChangeKlasse(klassen["3BHIF"]);

            Console.WriteLine("3AHIF nach ChangeKlasse:");
            Console.WriteLine(JsonConvert.SerializeObject(klassen["3AHIF"].Schuelers));
            Console.WriteLine("3BHIF nach ChangeKlasse:");
            Console.WriteLine(JsonConvert.SerializeObject(klassen["3BHIF"].Schuelers));
            Console.WriteLine($"s sitzt in der Klasse {s.KlasseNavigation.Name} mit dem KV {s.KlasseNavigation.KV}.");
        }
    }
}