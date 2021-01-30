using System;

namespace Spg.ReferenceTypes.Excercise
{
    /// <summary>
    /// Eine Person die als Basistyp verwednet werden kann
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Feld Vorname
        /// </summary>
        public string FirstName;

        /// <summary>
        /// Feld Nachname
        /// </summary>
        public string LastName;
    }

    /// <summary>
    /// Ein spezialisierter Typ User, der Person als Grundlage hat.
    /// </summary>
    public class User : Person
    {
        /// <summary>
        /// Feld Username
        /// </summary>
        public string UserName;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Erweitere die Methode so, dass die Länge des Benutzernamens
        /// in der Konsole ausgegeben wird. Ist der Benutzername <code>null</code>
        /// darf keine Exception geworfen werden.
        /// Verwende dazu den ternary conditional operator.
        /// </remarks>
        public void PrintUserNameLength()
        {
            Console.WriteLine(UserName?.Length.ToString() ?? "unknown");
        }
    }

    public class Program
    {
        /// <summary>
        /// Enthält die programmlogik für die Übung
        /// </summary>
        /// <remarks>
        /// Erstelle ein <code>Person</code>-Objekt vom Typ <code>Person</code> und befülle
        /// die Felder (Properties kennen wir ja noch nicht) mittels
        /// Initializer.
        /// 
        /// Caste <code>Person</code> auf ein <code>User</code>-Objekt und befülle auch den
        /// <code>UserName</code>.
        /// 
        /// Verwende die Methode <code>PrintUserNameLength()</code>.
        /// Es darf keine dabei Exception geworfen werden.
        /// 
        /// Setze <code>UserName</code> auf <code>null</code>.
        /// 
        /// Verwende erneut die Methode <code>PrintUserNameLength()</code>.
        /// Es darf keine dabei Exception geworfen werden.
        /// </remarks>
        public static void PrintResults()
        {
            User user = new User() 
            { 
                FirstName = "Martin", 
                LastName = "Schrutek" 
            };
            
            Person person = (Person)user;

            object obj1 = user;
            ((User)obj1).UserName = "schrutek";

            //person.UserName = "schrutek";
            //person.PrintUserNameLength();

            //person.UserName = null;
            //person.PrintUserNameLength();
        }

        /// <summary>
        /// Startpunkt der Anwendung
        /// </summary>
        /// <param name="args">Startparameter die in der Konsole übergeben wurden.</param>
        public static void Main(string[] args)
        {
            PrintResults();
        }
    }
}
