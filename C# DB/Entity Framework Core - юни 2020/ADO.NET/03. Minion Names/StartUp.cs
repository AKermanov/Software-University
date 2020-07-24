using System;
using System.Data.SqlClient;
using System.Text;

namespace _3._Minion_Names
{
    class StartUp
    {
        private static string connetionString = @"Server=TW1T4Y\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

        static void Main(string[] args)
        {
            //като слагаме using, когато GC види endOfScoup ще го закрие. Това е от c# 8
            using SqlConnection sqlConnection = new SqlConnection(connetionString);

            sqlConnection.Open();

            int villaniId = int.Parse(Console.ReadLine());

            string result = GetMinionsInfoAboutVillaian(sqlConnection, villaniId);

            Console.WriteLine(result);
        }

        private static string GetMinionsInfoAboutVillaian(SqlConnection sqlConnection, int villaniId)
        {
            var sb = new StringBuilder();
            //проверяваме дали виалана го има по ID

            string villinaName = GetVillainName(sqlConnection, villaniId);

            if (villinaName == null)
            {
                sb.Append($"No villain with ID {villaniId} exists in the database.");
            }
            else
            {
                sb.AppendLine($"Villain: {villinaName}");

                string getMinionsInfoQueryText = @"SELECT m.[Name], m.Age FROM Villains as v
                                                 LEFT OUTER JOIN MinionsVillains as mv on v.Id = mv.VillainId
                                                 LEFT OUTER JOIN Minions as m ON mv.MinionId = m.Id
                                                 WHERE v.[Name] = @villianName
                                                 ORDER BY m.[Name]";

                SqlCommand getMinionsInfroCommand = new SqlCommand(getMinionsInfoQueryText, sqlConnection);

                getMinionsInfroCommand.Parameters.AddWithValue("@villianName", villinaName);

                using SqlDataReader reader = getMinionsInfroCommand.ExecuteReader();

                //проверяваме дали има миниони

                int rowNum = 1;
                while (reader.Read())
                {

                    string minionName = reader["Name"]?.ToString();
                    string minionAge = reader["Age"]?.ToString();

                    if (minionName == "" && minionAge == "")
                    {
                        sb.AppendLine("(no minions)");
                        break;
                    }
                    sb.AppendLine($"{rowNum}. {minionName} {minionAge}");

                    rowNum++;
                }
            }
            return sb.ToString().TrimEnd();
        }

        private static string GetVillainName(SqlConnection sqlConnection, int villaniId)
        {
            string getVillianNameQueryText = @"SELECT Name FROM Villains WHERE Id = @villianId";

            using var getVillianNameCmd = new SqlCommand(getVillianNameQueryText, sqlConnection);

            //подаваме параметър на getVillianNameQueryText
            getVillianNameCmd.Parameters.AddWithValue("@villianId", villaniId);

            // ExecuteScalar()? ни предпажва ако върне null да не изгърми с NullRefferenceException
            // null?.ToString(); ако е null, ни връща null, вместо да гърми, защото след въпросителната нищо не се изпълнява
            string villinaName = getVillianNameCmd.ExecuteScalar()?.ToString(); //return object

            return villinaName;
        }
    }
}
