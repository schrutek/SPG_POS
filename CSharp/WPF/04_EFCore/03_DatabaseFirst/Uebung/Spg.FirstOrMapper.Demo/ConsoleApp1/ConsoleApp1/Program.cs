using ConsoleApp1.Models;
using System;
using System.Linq;
using System.Text.Json;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
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

            // TODO: Hier OR-Mapper instanzieren!!!

            // *************************************************************************************
            // ÜBUNG 1: Die 5AHIF möchte wissen, wieviele Tests sie pro Lehrer hat.
            // *************************************************************************************

            object result1 = null;

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

            object result2 = null;

            Console.WriteLine("\r\nRESULT2");
            Console.WriteLine(JsonSerializer.Serialize(result2, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 3: Wie viele Klassen gibt es pro Abteilung?
            // *************************************************************************************

            object result3 = null;

            Console.WriteLine("RESULT3");
            Console.WriteLine(JsonSerializer.Serialize(result3, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 4: Wie die vorige Übung, allerdings sind nur Abteilungen
            //          mit mehr als 10 Klassen auszugeben.
            //          Hinweis: Filtere mit Where nach dem Erstellen der Objekte mit Department
            //                   und Count
            // *************************************************************************************

            object result4 = null;

            Console.WriteLine("\r\nRESULT4");
            Console.WriteLine(JsonSerializer.Serialize(result4, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 5: Wann ist der letzte Test (Max von TE_Date) pro Lehrer und Fach der 5AHIF
            //          in der Tabelle Test?
            // *************************************************************************************

            object result5 = null;

            Console.WriteLine("\r\nRESULT5");
            Console.WriteLine(JsonSerializer.Serialize(result5, serializerOptions));

            // *************************************************************************************
            // ÜBUNG 6: Erstelle für jeden Lehrer eine Liste der Fächer, die er unterrichtet. Es
            // sind nur die ersten 10 Datensätze auszugeben. Das kann mit
            // .OrderBy(t=>t.TeacherId).Take(10)
            // am Ende der LINQ Anweisung gemacht werden. Hinweis: Verwende Distinct für die
            // liste der Unterrichtsgegenstände.
            //
            // !Hinweis: Der OR-Mapper kann diesen Query eigetnlich nicht in SQL umsetzen. 
            // In der Exception steht aber ein Hinweis auf die Lösung. Diese ist auch in
            // diesem Kapitel beschrieben:
            // https://github.com/schrutek/SPG_POS/tree/master/CSharp/EF%20Core/03_Queries#grenzen-von-ef-core
            // (Verwende AsEnumerable() nach Lessons)
            // *************************************************************************************

            object result6 = null;

            Console.WriteLine("\r\nRESULT6");
            Console.WriteLine(JsonSerializer.Serialize(result6, serializerOptions));
        }
    }
}
