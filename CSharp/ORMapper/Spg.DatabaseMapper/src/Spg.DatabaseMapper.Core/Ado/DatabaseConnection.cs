using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.DatabaseMapper.Core.Ado
{
    public class DatabaseConnection
    {
        public void Read()
        {
            using (SqliteConnection connection = new(@"C:\HTL\POS\CSharp\ORMapper\Spg.DatabaseMapper\SayonaraXXX.db"))
            {
                SqliteCommand command = new("", connection);
                command.Parameters.AddWithValue("@pricePoint", paramValue);

                try
                {
                    connection.Open();
                    SqliteDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var s = reader[0];
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
