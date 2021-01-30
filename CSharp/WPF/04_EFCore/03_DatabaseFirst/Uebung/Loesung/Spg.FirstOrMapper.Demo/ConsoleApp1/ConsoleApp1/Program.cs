using ConsoleApp1.Models;
using System;
using System.Linq;
using System.Text.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
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
            
            TestsAdministratorContext db = new TestsAdministratorContext();


            // Gibt es Schüler, die nur in POS eine Prüfung haben?
            //var result0 = db.Tests
            //    .Where(s => s.TE_ClassNavigation.Tests
            //        .Any(t => t.TE_Subject == "POS1"))
            //    .ToList();

            //Console.WriteLine("\r\nRESULT0");
            //Console.WriteLine(JsonSerializer.Serialize(result0, serializerOptions));

            // Welche Schüler hatten im November AM Prüfungen?



            // *************************************************************************************
            // ÜBUNG 1: Die 5AHIF möchte wissen, wieviele Tests sie pro Lehrer hat.
            // *************************************************************************************

            var result1 = db.Tests
                .Where(t => t.TE_Class == "5AHIF")
                .GroupBy(t =>t.TE_Teacher)
                .Select(g => new
                {
                    Teacher = g.Key,
                    Tests = g.Count()
                }).ToList();

            Console.WriteLine("\r\nRESULT1");
            Console.WriteLine(JsonSerializer.Serialize(result1, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 2: Wie viele Klassen sind pro Tag und Stunde gleichzeitig im Haus?
            //          Hinweis: Gruppiere zuerst nach Tag und Stunde in Lesson. Für die Ermittlung
            //          der Klassenanzahl zähle die eindeutigen KlassenIDs, indem mit Distinct eine
            //          Liste dieser IDs (C_ID) erzeugt wird und dann mit Count() gezählt wird.
            //          Es sind mit OrderByDescending(g=>g.ClassCount).Take(5) nur die 5
            //          "stärksten" Stunden auszugeben.
            // *************************************************************************************

            var result2 = db.Lessons
                .GroupBy(l => new { l.L_Day, l.L_Hour })
                .Select(g => new
                {
                    Day = g.Key.L_Day,
                    Hour = g.Key.L_Hour,
                    ClassCount = g.Select(g2 => g2.L_Class).Distinct().Count()
                })
                .OrderByDescending(g => g.ClassCount).Take(5)
                .ToList();

            Console.WriteLine("\r\nRESULT2");
            Console.WriteLine(JsonSerializer.Serialize(result2, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 3: Wie viele Klassen gibt es pro Abteilung?
            // *************************************************************************************

            var result3 = db.Schoolclasses
                .GroupBy(s => s.C_Department)
                .Select(g => new
                {
                    Department = g.Key,
                    KlassenCount = g.Count()
                }).ToList();

            Console.WriteLine("RESULT3");
            Console.WriteLine(JsonSerializer.Serialize(result3, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 4: Wie die vorige Übung, allerdings sind nur Abteilungen
            //          mit mehr als 10 Klassen auszugeben.
            //          Hinweis: Filtere mit Where nach dem Erstellen der Objekte mit Department
            //                   und Count
            // *************************************************************************************

            var result4 = db.Schoolclasses
                .GroupBy(s => s.C_Department)
                .Select(g => new
                {
                    Department = g.Key,
                    KlassenCount = g.Count()
                })
                .Where(s => s.KlassenCount > 10)
                .ToList();

            Console.WriteLine("\r\nRESULT4");
            Console.WriteLine(JsonSerializer.Serialize(result4, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 5: Wann ist der letzte Test (Max von TE_Date) pro Lehrer und Fach der 5AHIF
            //          in der Tabelle Test?
            // *************************************************************************************

            var result5 = db.Tests
                .Where(t => t.TE_Class == "5AHIF")
                .GroupBy(t => new { t.TE_Teacher, t.TE_Subject })
                .Select(g => new
                {
                    g.Key.TE_Teacher,
                    g.Key.TE_Subject,
                    LastTest = g.Max(g2 => g2.TE_Date)
                })
                .ToList();

            Console.WriteLine("\r\nRESULT5");
            Console.WriteLine(JsonSerializer.Serialize(result5, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 6: Erstelle für jeden Lehrer eine Liste der Fächer, die er unterrichtet. Es
            // sind nur die ersten 10 Datensätze auszugeben. Das kann mit
            // .OrderBy(t=>t.TeacherId).Take(10)
            // am Ende der LINQ Anweisung gemacht werden. Hinweis: Verwende Distinct für die
            // liste der Unterrichtsgegenstände.
            //
            // !Hinweis: Der OR-Mapper kann diesen Query eigentlich nicht in SQL umsetzen. 
            // In der Exception steht aber ein Hinweis auf die Lösung. Diese ist auch in
            // diesem Kapitel beschrieben:
            // https://github.com/schrutek/SPG_POS/tree/master/CSharp/EF%20Core/03_Queries#grenzen-von-ef-core
            // (Verwende AsEnumerable() nach Lessons)
            // *************************************************************************************

            var result6 = db.Lessons
                .AsEnumerable()
                .GroupBy(t => t.L_Teacher)
                .Select(g => new
                {
                    TeacherId = g.Key,
                    Subjects = g.Select(g2 => new
                    {
                        g2.L_Subject
                    }).Distinct()
                })
                .OrderBy(g => g.TeacherId).Take(10)
                .ToList();

            Console.WriteLine("\r\nRESULT6");
            Console.WriteLine(JsonSerializer.Serialize(result6, serializerOptions));
        }
    }
}
