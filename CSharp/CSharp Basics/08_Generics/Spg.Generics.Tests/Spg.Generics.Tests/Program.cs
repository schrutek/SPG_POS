using Spg.Generics.Tests.Model;
using Spg.Generics.Tests.Repository;
using System;

namespace Spg.Generics.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            SchoolClassRepository schoolClassRepository = new SchoolClassRepository();
            SchoolClass schoolClass = schoolClassRepository.GetById(2);
            Console.WriteLine($"Klasse!  ID: {schoolClass.Id}, Raum: {schoolClass.RoomNumber}");

            PupilRepository pupilRepository = new PupilRepository();
            Pupil pupil = pupilRepository.GetById(3);
            Console.WriteLine($"Schüler! ID: {pupil.Id}, Name: {pupil.FirstName} {pupil.LastName}");


            RepositoryBase<SchoolClass> schoolClassRepositoryGeneric = new RepositoryBase<SchoolClass>();
            SchoolClass schoolClassGeneric = schoolClassRepositoryGeneric.GetById(2);
            Console.WriteLine($"Klasse!  ID: {schoolClassGeneric.Id}, Raum: {schoolClassGeneric.RoomNumber}");

            RepositoryBase<Pupil> pupilRepositoryGeneric = new RepositoryBase<Pupil>();
            Pupil pupilGeneric = pupilRepositoryGeneric.GetById(3);
            Console.WriteLine($"Schüler! ID: {pupilGeneric.Id}, Name: {pupilGeneric.FirstName} {pupilGeneric.LastName}");
        }
    }
}
