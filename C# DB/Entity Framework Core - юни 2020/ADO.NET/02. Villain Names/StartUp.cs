using System;
using System.Data.SqlClient;

namespace _2._Villain_Names
{
    class StartUp
    {
        private static string connectionString = "Server=TW1T4Y\\SQLEXPRESS;" + "Database=MinionsDB;" + "Integrated Security=true;";

        private static SqlConnection connection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            connection.Open();

            using (connection)
            {
                string queryText = @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                     FROM Villains AS v 
                                     JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                     GROUP BY v.Id, v.Name 
                                     HAVING COUNT(mv.VillainId) > 3 
                                     ORDER BY COUNT(mv.VillainId)";
                var cmd = new SqlCommand(queryText,connection);

                var reader = cmd.ExecuteReader(); //IDisposable - всяко нещо,което може да се диспоузва трябва да се
                                                  // отваря и затваря!!

                using (reader)
                {
                    while (reader.Read()) 
                    {
                        Console.WriteLine($"{reader["Name"]} - {reader["MinionsCount"]}");
                    }
                }
            }
        }
    }
}
