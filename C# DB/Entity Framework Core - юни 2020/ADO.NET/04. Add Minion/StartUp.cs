using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace _4._Add_Minion
{
    class StartUp
    {
        private static string connetionString = @"Server=TW1T4Y\SQLEXPRESS;Database=MinionsDB;Integrated Security=true;";

        static void Main(string[] args)
        {
            //като слагаме using, когато GC види endOfScoup ще го закрие. Това е от c# 8
            using SqlConnection sqlConnection = new SqlConnection(connetionString);

            sqlConnection.Open();

            var minionsInput = Console.ReadLine().Split(": ").ToArray();
            var minionsInfo = minionsInput[1].Split(" ").ToArray();

            var villainInput = Console.ReadLine().Split(": ").ToArray();
            var villainInfo = villainInput[1].Split(" ").ToArray();

            string result = AddMinionToDatabase(sqlConnection, minionsInfo, villainInfo);

            Console.WriteLine(result);
        }

        private static string AddMinionToDatabase(SqlConnection sqlConnection, string[] minionsInfo, string[] villainInfo)
        {
            var output = new StringBuilder();

            string minionName = minionsInfo[0];
            string minionAge = minionsInfo[1];
            string minionTown = minionsInfo[2];

            string villainName = villainInfo[0];

            //town вече съществува
            string towId = EnsureTownExists(sqlConnection,minionTown, output);

            //villain вече съществува
            string villainId = EnsureVillianExists(sqlConnection, villainName, output);

            string insertMinionQueryText = @"INSERT INTO Minions ([Name], Age, TownId) VALUES (@name, @age, @townId)";
            using SqlCommand insertMinionsCmd = new SqlCommand(insertMinionQueryText, sqlConnection);

            //вместо да ги инсъртваме 1по1 както тпреди, подаваме масив и инсъртваме наведнъж
            insertMinionsCmd.Parameters.AddRange(new[] { 
              new SqlParameter("@name", minionName),
              new SqlParameter("@age", minionAge),
              new SqlParameter("@townId", towId)
            });

            insertMinionsCmd.ExecuteNonQuery();

            string getMinionIdQueryText = @"SELECT Id FROM Minions WHERE Name = @Name";

            using SqlCommand getMinionIdCmd = new SqlCommand(getMinionIdQueryText, sqlConnection);

            getMinionIdCmd.Parameters.AddWithValue("@Name", minionName);

            string minionId = getMinionIdCmd.ExecuteScalar().ToString();

            string insertIntoMappingQueryText = @"INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@minionId, @villainId)";

            using SqlCommand insertIntoMappingCmd = new SqlCommand(insertIntoMappingQueryText, sqlConnection);
            insertIntoMappingCmd.Parameters.AddRange(new[]
            {
                new SqlParameter("@minionId", minionId),
                new SqlParameter("@villainId", villainId)
            }); 

            insertIntoMappingCmd.ExecuteNonQuery();
            output.AppendLine($"Successfully added {minionName} to be minion of {villainName}.");

            return output.ToString().TrimEnd();
        }

        private static string EnsureVillianExists(SqlConnection sqlConnection, string villainName, StringBuilder output)
        {
            string getVillainIdQueryText = @"SELECT Id FROM Villains WHERE Name = @Name";
            using SqlCommand getVillainIdCmd = new SqlCommand(getVillainIdQueryText, sqlConnection);

            getVillainIdCmd.Parameters.AddWithValue("@Name", villainName);

            string villainId = getVillainIdCmd.ExecuteScalar()?.ToString();

            if (villainId == null)
            {
                string getFactorIdQueryTest = @"SELECT Id FROM EvilnessFactors WHERE [Name] = 'Evil'";

                using SqlCommand getFactorIdCmd = new SqlCommand(getFactorIdQueryTest, sqlConnection);

                string factorId = getFactorIdCmd.ExecuteScalar()?.ToString();

                string insertVillainQueryText = @"INSERT INTO Villains([Name], EvilnessFactorId)  VALUES(@villainName, @factorId)";

                using SqlCommand insertVillainCmd = new SqlCommand(insertVillainQueryText, sqlConnection);

                insertVillainCmd.Parameters.AddWithValue("@villainName", villainName);
                insertVillainCmd.Parameters.AddWithValue("@factorId", factorId);

                insertVillainCmd.ExecuteNonQuery();

                villainId = getVillainIdCmd.ExecuteScalar().ToString();

                output.AppendLine($"Villain {villainName} was added to the database.");
            }

            return villainId;
        }

        private static string EnsureTownExists(SqlConnection sqlConnection, string minionTown, StringBuilder output)
        {
            //взимаме townId
            string getTownIdeQueryText = @"SELECT Id FROM Towns WHERE Name = @townName";

            using SqlCommand getTownIdCmd = new SqlCommand(getTownIdeQueryText, sqlConnection);
            getTownIdCmd.Parameters.AddWithValue("@townName", minionTown);

            string townInd = getTownIdCmd.ExecuteScalar()?.ToString();

            //ако townId го няма (null) 
            if (townInd == null)
            {
                //инсъртваме града директно в България
                string insertTownQueryText = @"INSERT INTO Towns([Name], CountryCode) VALUES (@townName, 1)";

                using SqlCommand insertTownCmd = new SqlCommand(insertTownQueryText, sqlConnection);

                insertTownCmd.Parameters.AddWithValue("@townName", minionTown);
                insertTownCmd.ExecuteNonQuery();

                //на инсертнатия град взимаме towId
                townInd = getTownIdCmd.ExecuteScalar().ToString();

                //append line може
                output.AppendLine($"Town {minionTown} was added to the database.");
            }

            return townInd;
        }
    }
}
