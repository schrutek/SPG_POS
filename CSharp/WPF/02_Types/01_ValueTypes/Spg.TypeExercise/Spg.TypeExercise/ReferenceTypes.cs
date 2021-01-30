using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.TypeExercise
{
    public class Person
    {
        public int age = 0;
    }
    // Entspricht Pupil extends Person
    public class Pupil : Person
    {
        public string klasse = "";
    }

    public class ReferenceTypes
    {
        public void DoPersonSomething()
        {
            Person p;            // p ist null.
            p = new Person();    // Eine Person wird am Heap erstellt und
                                 // die Referenzadresse in p geschrieben.
            Person p2 = p;       // Nun habe ich eine Instanz, auf die 2
                                 // Referenzvariablen zeigen.
            p2.age = 18;         // p.age liefert natürlich auch 18.

            Pupil pu = new Pupil();
            Person p3;




            p3 = (Person)pu;        // "Hinaufcasten" ist möglich, da die 
                                    // Vererbung ja eine "is-a" beziehung ist. 
            //p3.klasse = "3BHIF";    // Geht natürlich nicht mehr.
            object obj1 = pu;       // Alles ist von object abgeleitet.
            //obj1.age = 12;          // Natürlich nicht mehr möglich.
            ((Pupil)obj1).age = 12; // Das würde gehen, aber nur wenn obj1 
                                    // ein Pupil war.




            // Wenn pu nicht in Pupil umgewandelt werden kann, wird NULL 
            // geliefert. In diesem Fall wird eine neue Instanz von 
            // Pupil erstellt.
            p3 = pu as Person ?? new Pupil();
            // true, da is angibt, ob ein Typencast durchgeführt werden kann.
            if (pu is Person)
            {
                Console.WriteLine("pu is Person.");
            }
            // In object gibt es die Methode GetType() und ToString().
            // Liefert "ReferenceTypesApp.Pupil".
            string type = pu.GetType().ToString();
        }
    }
}
