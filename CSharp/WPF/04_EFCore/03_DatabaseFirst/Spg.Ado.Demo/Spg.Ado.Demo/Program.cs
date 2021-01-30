using LinqUebung1.App.Model;
using Spg.Ado.Demo.MockModels;
using Spg.Ado.Demo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Spg.Ado.Demo
{
    public class Program
    {
        private static string connectionString =
            "Data Source=.\\SQLEXPRESS;Initial Catalog=TestsAdministrator;Integrated Security=True;";
        
        public static void Main(string[] args)
        {
            Program program = new Program();
            program.ListPupilsWithA();

            //PupilService service = new PupilService();
            //service.ListPupils();
        }

        public void ListPupilsWithA()
        {
            string nameBeginsWith = "C";
            SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=TestsAdministrator;Integrated Security=True;");
            conn.Open();

            // Einladung für SQL Injections...
            SqlCommand command = new SqlCommand("SELECT * FROM Pupil WHERE P_Lastname LIKE '" + nameBeginsWith + "%'", conn);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                // Wir casten auf "gut Glück", denn alles ist ein object.
                long id = (long)reader["P_ID"];
                string lastname = reader["P_Lastname"] == DBNull.Value ? null : (string)reader["P_Lastname"];
                Console.WriteLine($"{id}: {lastname}");
            }

            // Wer schließt reader und conn?? Es wurde kein using verwendet.        }
        }

        public void ListPupilsWithAClean()
        {
            // Provide the query string with a parameter placeholder.
            string queryString =
                $"SELECT * FROM Pupil WHERE P_Lastname LIKE @nameBeginsWith";

            // Specify the parameter value.
            string nameBeginsWith = "A%";

            // Create and open the connection in a using block. This
            // ensures that all resources will be closed and disposed
            // when the code exits.
            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@nameBeginsWith", nameBeginsWith);

                // Open the connection in a try/catch block.
                // Create and execute the DataReader, writing the result
                // set to the console window.
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // Wir casten auf "gut Glück", denn alles ist ein object.
                        long id = (long)reader["P_ID"];
                        string lastname = reader["P_Lastname"] == DBNull.Value ? null : (string)reader["P_Lastname"];
                        Console.WriteLine($"{id}: {lastname}");
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadLine();
            }
        }
    }
}