using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grouping.Model;
using System.Text.Json;
using System.Threading.Tasks;

namespace Grouping
{
    class Pupil
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    class Program
    {
        static async Task Main(string[] args)
        {

            // *************************************************************************************
            // Schreibe in den nachfolgenden Übungen statt der Zeile
            // object resultX = null;
            // die korrekte LINQ Abfrage. Verwende den entsprechenden Datentyp statt object.
            // Du kannst eine "schöne" (also eingerückte) Ausgabe der JSON Daten erreichen, indem
            // du die Variable WriteIndented auf true setzt.
            // *************************************************************************************
            var WriteIndented = false;
            var serializerOptions = new JsonSerializerOptions { WriteIndented = WriteIndented };
            TestsData db = await TestsData.FromFile("db/tests.json");


            // *************************************************************************************
            // ÜBUNG 1: Erstelle für jeden Lehrer eine Liste der Fächer, die er unterrichtet. Es
            // sind nur die ersten 10 Datensätze auszugeben. Das kann mit
            // .OrderBy(t=>t.TeacherId).Take(10)
            // am Ende der LINQ Anweisung gemacht werden. Hinweis: Verwende Distinct für die
            // liste der Unterrichtsgegenstände.
            // *************************************************************************************

            var result1 = db.Lesson
                .GroupBy(l => l.L_Teacher)
                .Select(g => new
                {
                    TeacherId = g.Key,
                    Subjects = g.Select(g2 => new
                    {
                        g2.L_Subject
                    }).Distinct()
                })
                .OrderBy(t => t.TeacherId).Take(10);

            Console.WriteLine("RESULT1");
            Console.WriteLine(JsonSerializer.Serialize(result1, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 2: Die 5AHIF möchte wissen, in welchem Monat sie welche Tests hat.
            //          Hinweis: Mit den Properties Month und Year kann auf das Monat bzw. Jahr
            //          eines DateTime Wertes zugegriffen werden. Die Ausgabe in DisplayMonth kann
            //          $"{mydate.Year:00}-{mydate.Month:00}" (mydate ist zu ersetzen)
            //          erzeugt werden
            // *************************************************************************************

            var result2 = db.Test
                .Where(t => t.TE_Class == "5AHIF")
                .GroupBy(t => $"{t.TE_Date.Year:00}-{t.TE_Date.Month:00}")
                .Select(g => new
                {
                    DisplayMonth = g.Key,
                    Tests = g.Select(g2 => new
                    {
                        Date = g2.TE_Date,
                        Teacher = g2.TE_Teacher,
                        Subject = g2.TE_Subject,
                    })
                });

            Console.WriteLine("RESULT2");
            Console.WriteLine(JsonSerializer.Serialize(result2, serializerOptions));


            // *************************************************************************************
            // ÜBUNG 3: Jeder Schüler der 5AHIF soll eine Übersicht bekommen, welche Tests er pro Fach
            //          abgeschlossen hat.
            //          Es sind nur die ersten 2 Schüler mit OrderBy(p => p.P_ID).Take(2) am Ende des
            //          Statements auszugeben.
            //          Hinweis: Beachte die Datenstruktur in der Ausgabe.
            //   Pupil                           <-- Zuerst wird der Schüler projiziert (Select)
            //     |
            //     +-- P_ID
            //         P_Firstname
            //         P_Lastname
            //         Tests                     <-- Hier soll nach Subject gruppiert werden
            //           |
            //           +---- Subject           <-- y der GrupKepierung
            //           +---- Termine           <-- Projektion der Gruppierung
            //                    |    
            //                    +------ TE_Teacher
            //                    +------ TE_Date
            //                    +------ TE_Lesson
            // *************************************************************************************

            var result3 = db.Pupil
                .Where(p => p.P_Class == "5AHIF")
                .Select(p => new
                {
                    p.P_ID,
                    p.P_Firstname,
                    p.P_Lastname,
                    Tests = p.P_ClassNavigation.Tests
                    .GroupBy(p => p.TE_Subject)
                    .Select(g => new
                    {
                        Subject = g.Key,
                        Termine = g.Select(g2 => new
                        {
                            g2.TE_Teacher,
                            g2.TE_Date,
                            g2.TE_Lesson
                        })
                    })
                })
                .OrderBy(p => p.P_ID).Take(2);

            Console.WriteLine("RESULT3");
            Console.WriteLine(JsonSerializer.Serialize(result3, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 4: Wie viele Klassen sind pro Tag und Stunde gleichzeitig im Haus?
            //          Hinweis: Gruppiere zuerst nach Tag und Stunde in Lesson. Für die Ermittlung
            //          der Klassenanzahl zähle die eindeutigen KlassenIDs, indem mit Distinct eine
            //          Liste dieser IDs (C_ID) erzeugt wird und dann mit Count() gezählt wird.
            //          Es sind mit OrderByDescending(g=>g.ClassCount).Take(5) nur die 5
            //          "stärksten" Stunden auszugeben.
            // *************************************************************************************

            var result4 = db.Lesson
                .GroupBy(l => new { l.L_Day, l.L_Hour })
                .Select(g => new
                {
                    Day = g.Key.L_Day,
                    Hour = g.Key.L_Hour,
                    ClassCount = g.Select(g2 => g2.L_Class).Distinct().Count()
                })
                .OrderByDescending(g => g.ClassCount).Take(5);

            Console.WriteLine("RESULT4");
            Console.WriteLine(JsonSerializer.Serialize(result4, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 5: Wie viele Klassen gibt es pro Abteilung?
            // *************************************************************************************

            var result5 = db.Schoolclass
                .GroupBy(s => s.C_Department)
                .Select(g => new
                {
                    Department = g.Key,
                    KlassenCount = g.Count()
                });

            Console.WriteLine("RESULT5");
            Console.WriteLine(JsonSerializer.Serialize(result5, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 6: Wie die vorige Übung, allerdings sind nur Abteilungen
            //          mit mehr als 10 Klassen auszugeben.
            //          Hinweis: Filtere mit Where nach dem Erstellen der Objekte mit Department
            //                   und Count
            // *************************************************************************************

            var result6 = db.Schoolclass
                .GroupBy(s => s.C_Department)
                .Select(g => new
                {
                    Department = g.Key,
                    KlassenCount = g.Count()
                })
                .Where(s => s.KlassenCount > 10);

            Console.WriteLine("RESULT6");
            Console.WriteLine(JsonSerializer.Serialize(result6, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 7: Wann ist der letzte Test (Max von TE_Date) pro Lehrer und Fach der 5AHIF
            //          in der Tabelle Test?
            // *************************************************************************************

            object result7 = db.Test
                .Where(t => t.TE_Class == "5AHIF")
                .GroupBy(t => new { t.TE_Teacher, t.TE_Subject })
                .Select(g => new
                {
                    g.Key.TE_Teacher,
                    g.Key.TE_Subject,
                    LastTest = g.Max(g2 => g2.TE_Date)
                });

            Console.WriteLine("RESULT7");
            Console.WriteLine(JsonSerializer.Serialize(result7, serializerOptions));
        }
    }
}
