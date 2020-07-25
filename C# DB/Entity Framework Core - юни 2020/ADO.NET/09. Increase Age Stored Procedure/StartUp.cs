using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace _9._Increase_Age_Stored_Procedure
{
    class StartUp
    {
        private static string connetionString = @"Server=TW1T4Y\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

        static void Main(string[] args)
        {
            //като слагаме using, когато GC види endOfScoup ще го закрие. Това е от c# 8
            using SqlConnection sqlConnection = new SqlConnection(connetionString);

            sqlConnection.Open();

            int minionId = int.Parse(Console.ReadLine());

            string result = IncreaseMinionAgeById(sqlConnection, minionId);

            Console.WriteLine(result);
        }

        private static string IncreaseMinionAgeById(SqlConnection sqlConnection, int minionId)
        {
            var sb = new StringBuilder();
            string procName = "usp_GetOlder";
            using SqlCommand increaseAgeCmd = new SqlCommand(procName, sqlConnection);

            increaseAgeCmd.CommandType = CommandType.StoredProcedure;

            increaseAgeCmd.Parameters.AddWithValue("@minionId", minionId);

            increaseAgeCmd.ExecuteNonQuery();

            string getMinionInfoQueryText  = @"SELECT [Name], Age FROM Minions WHERE Id = @minionId";

            using SqlCommand getMinionInfoCmd = new SqlCommand(getMinionInfoQueryText, sqlConnection);
            getMinionInfoCmd.Parameters.AddWithValue("@minionId", minionId);

            using SqlDataReader reader = getMinionInfoCmd.ExecuteReader();

            reader.Read();

            string minionName = reader["Name"]?.ToString();
            string minionAge = reader["Age"]?.ToString();

            sb.AppendLine($"{minionName} – {minionAge} years old");

            return sb.ToString().TrimEnd();
        }
    }
}
