using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Grouping.Model;
using System.Text.Json;

namespace Grouping
{
    class Pupil
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            TestsData data = TestsData.FromFile("../db/tests.json");

            var result = from p in data.Pupil
            select new
            {
                p.P_ClassNavigation.C_Department,
                p.P_ClassNavigation.C_ID,
                p.P_ClassNavigation.C_ClassTeacher,
                p.P_Lastname,
                p.P_Firstname
            };

            var result2 = from c in data.Schoolclass
            from p in data.Pupil
            select new
            {
                c.C_Department,
                c.C_ID,
                c.C_ClassTeacher,
                p.P_Lastname,
                p.P_Firstname
            };



            var result99 = from l in data.Lesson
            group l by l.L_Class into g
            select new
            {
                Class = g.Key,
                Count = g.Count(),
                MaxHour = g.Max(x => x.L_Hour)
            };



            // *************************************************************************************
            // Schreibe in den nachfolgenden Übungen statt der Zeile
            // object resultX = null;
            // die korrekte LINQ Abfrage. Verwende den entsprechenden Datentyp statt object.
            // *************************************************************************************
            // ÜBUNG 1: Wie viele Klassen gibt es pro Abteilung?
            // *************************************************************************************
            object result1 = from s in data.Schoolclass
                             group s by s.C_Department into d
                             select new
                             {
                                 KlassenAnzahl = d.Count()
                             };

            Console.WriteLine("RESULT1");
            Console.WriteLine(JsonSerializer.Serialize(result1));

            // *************************************************************************************
            // ÜBUNG 2: Wie (1), allerdings sind nur Abteilungen mit mehr als 10 Klassen auszugeben.
            //          Hinweis: Filtere mit Where nach dem Erstellen der Objekte mit Department
            //                   und Count
            // *************************************************************************************
            var result2 = from s in data.Schoolclass
                          group s by s.C_Department into d
                          where d.Count() > 10
                          select new
                          {
                              KlassenAnzahl = d.Count()
                          };


            Console.WriteLine("RESULT2");
            Console.WriteLine(JsonSerializer.Serialize(result2));

            // *************************************************************************************
            // ÜBUNG 3: Wann ist der letzte Test (Max von TE_Date) pro Lehrer und Fach der 5AHIF
            //          in der Tabelle Test?
            // *************************************************************************************
            object result3 = from t in data.Test
                             where t.TE_ClassNavigation.C_ID == "5AHIF"
                             group t by new { t.TE_Teacher, t.TE_Subject } into d
                             select new
                             {
                                 LastTest = d.Max(c => c.TE_Date)
                             };

            Console.WriteLine("RESULT3");
            Console.WriteLine(JsonSerializer.Serialize(result3));

            // *************************************************************************************
            // ÜBUNG 4
            // Bei Verschmutzungen wird oft der Lehrer, der die letzte Stunde pro Tag in einem Raum
            // war, befragt. Dafür filtere die Tabelle data.Lesson so, dass die Stunde gleich der
            // letzten Stunde des entsprechenden Raumes und Tages ist. Dafür wird in where eine
            // Unterabfrage benötigt, die nochmals data.Lesson filtert und die letzte Stunde 
            // ermittelt.
            // Gib nur die Räume aus C2 (verwende StartsWith) am Montag (L_Day ist 1) aus. Beachte,
            // dass L_Room auch null sein kann. Verwende daher den ?. Operator. In diesem Fall soll
            // als Standardwert false geliefert werden. Achte außerdem auf die Rangfolge der Operatoren
            // ?? und &&.
            // *************************************************************************************
            object result4 = from l in data.Lesson
                             where l.L_Hour == data.Lesson.Max(p => p.L_Hour)
                             select new
                             {
                                 Room = l.L_Room.StartsWith("C2"),
                                 Day = l.L_Day,
                                 Hour = l.L_Hour,
                                 Teacher = l.L_Teacher,
                                 Class = l.L_Class
                             };

            //{ "Room":"C2.09","Day":1,"Hour":16,"Teacher":"PUC","Class":"5BBIF"}
            Console.WriteLine("RESULT4");
            Console.WriteLine(JsonSerializer.Serialize(result4));

            // *************************************************************************************
            // ÜBUNG 5 (schwer!)
            // Die vorige Abfrage hat eine sehr schlechte Laufzeit: Für jede Stunde wird abgefragt,
            // ob sie die letzte Stunde ist. Dafür wird wiederum die gesamte Lesson Tabelle
            // aggregiert. In Datenbanken wird dieses Problem daher so gelöst: Es wird eine View
            // mit 3 Spalten (Raum, Tag, letzte Stunde) erstellt. Danach wird mit einem Join aus
            // der Lesson Tabelle die letzte Stunde geholt.
            // Setze diese Technik nun in LINQ um, indem du in lastLesson eine Collection mit Raum,
            // Tag und der letzten Stunde (Max von L_Hour) speicherst. Dann führe einen Join durch.
            // Der Join mit mehreren Spalten funktioniert in LINQ so:
            //     from x in table1
            //     join y in table2 on new { X1 = x.Field1, X2 = Field2 } equals new { X1 = y.Field1, X2 = y.Field2 }
            // Die Ausgabe muss natürlich dem Beispiel 4 entsprechen.
            // *************************************************************************************
            object result5 = null;
            Console.WriteLine("RESULT5");
            Console.WriteLine(JsonSerializer.Serialize(result5));

        }
    }
}
