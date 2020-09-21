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

            User user2 = (User)pupil;
            user2.age = 18;

            object pupilObject = pupil;
            pupilObject.ToString();

            ((Pupil)pupilObject).klasse = "";

            bool yesIstIs1 = pupil.GetType() ==  typeof(Pupil);
            bool yesIstIs2 = pupil is Pupil;

            bool yesIstIs3 = pupil.GetType() == typeof(User);
            bool yesIstIs4 = pupil is User;

            User user3 = pupil as Pupil ?? new Pupil();
        }
    }
}
