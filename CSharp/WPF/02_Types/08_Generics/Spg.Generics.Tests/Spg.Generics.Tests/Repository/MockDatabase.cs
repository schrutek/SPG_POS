using Spg.Generics.Tests.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spg.Generics.Tests.Repository
{
    public class MockDatabase
    {
        private List<SchoolClass> _schoolclasses = new List<SchoolClass>()
        {
            new SchoolClass(){Id=1, RoomNumber="C.3.07" },
            new SchoolClass(){Id=2, RoomNumber="C.4.08" },
            new SchoolClass(){Id=3, RoomNumber="C.3.12" },
        };

        private List<Pupil> _pupils = new List<Pupil>()
        {
            new Pupil(){ Id=1, FirstName="Vorname 1", LastName="Nachname 1" },
            new Pupil(){ Id=2, FirstName="Vorname 2", LastName="Nachname 2" },
            new Pupil(){ Id=3, FirstName="Vorname 3", LastName="Nachname 3" },
            new Pupil(){ Id=4, FirstName="Vorname 4", LastName="Nachname 4" },
            new Pupil(){ Id=5, FirstName="Vorname 5", LastName="Nachname 5" },
        };

        public List<SchoolClass> ListSchoolClasses()
        {
            return _schoolclasses;
        }

        public List<Pupil> ListPupils()
        {
            return _pupils;
        }

        /// <summary>
        /// Generische Methode
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public object GetDbSet<T>()
        {
            switch (typeof(T).Name)
            {
                case "SchoolClass":
                    return _schoolclasses;
                case "Pupil":
                    return _pupils;
                default:
                    return null;
            }
        }
    }
}
