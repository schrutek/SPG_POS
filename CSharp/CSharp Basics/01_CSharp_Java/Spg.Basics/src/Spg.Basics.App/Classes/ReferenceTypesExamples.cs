using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Basics.App.Classes
{
    public class User
    {
        public int age = 0;
    }

    // Entspricht Pupil extends Person
    public class Pupil : User
    {
        public string klasse = "";
    }

    public class ReferenceTypesExamples
    {
        public void DoSomething()
        {
            User user = new User();
            user.age = 43;

            Pupil pupil = new Pupil();
            pupil.age = 17;
            pupil.klasse = "3AHIF";

            User user2 = pupil;
            user2.age = 18;

            //Pupil pupil2 = (Pupil)user;

            object pupilObject = pupil;
            pupilObject.ToString();

            ((Pupil)pupilObject).klasse = "";

            bool r1 = pupil.GetType() == typeof(Pupil);
            bool r2 = pupil is Pupil;
            bool r3 = pupil.GetType() == typeof(User);
            bool r4 = pupil is User;


            bool r5 = pupil.GetType() == typeof(Pupil);
            bool r6= pupil is Pupil;

            bool r7 = pupil.GetType() == typeof(User);
            bool r8 = pupil is User;

            User user3 = pupil as Pupil ?? new Pupil();

        }
    }
}
