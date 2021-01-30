using System;

namespace Spg.Properties.Tests
{
    public class Pupil
    {
        private string _vorname;
        private string _zuname;

        public int Alter
        {
            get 
            {
                return _alter;
            }
            set
            {
                if (value > 0)
                {
                    _alter = value;
                }
                else
                {
                    throw new ArgumentException("Ungültiges Alter");
                }
            }
        }
        private int _alter;

        public string Vorname { get; set; } = "Martin";


        public long RegistrationNumber { get; set; }

        public string Nachname { get; } = "Schrutek";

        public string FullName
        {
            get
            {
                return Vorname + " " + Nachname;
            }
        }



        public string GetLogname()
        {
            return Vorname;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Pupil pupil = new Pupil();
            pupil.Alter = 18;
            pupil.Vorname = "Martin";
            //pupil.Nachname = "Schrutek";

            try
            {
                Pupil pupil2 = new Pupil()
                {
                    Alter = -1,
                    Vorname = "Martin",
                };
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }



            int alter = pupil.Alter;

            string str = pupil.GetLogname();

            Console.WriteLine("Hello World!");
        }
    }
}
