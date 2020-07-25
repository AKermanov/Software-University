using System;
using System.Data.SqlClient;

namespace _6._Remove_Villain
{
    class StartUp
    {
        private static string connectionString = "Server=TW1T4Y\\SQLEXPRESS;" + "Database=MinionsDB;" + "Integrated Security=true;";

        private static SqlConnection connection = new SqlConnection(connectionString);

        private static SqlTransaction transaction;
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            connection.Open();

            using (connection)
            {
                transaction = connection.BeginTransaction();

                try
                {
                    var command = new SqlCommand();
                    //сетваме каква ни е връзката
                    command.Connection = connection;

                    //сетваме какъв ни е транзакшъна
                    command.Transaction = transaction;

                    //сетваме какво ще изпълняваме с тази команда
                    //връща 1 запис
                    command.CommandText = @"SELECT Name FROM Villains WHERE Id = @villainId";

                    //ескейпваме го за да не ни SQL enjection
                    command.Parameters.AddWithValue("@villainId", id);

                    //връща obkect
                    object value = command.ExecuteScalar();

                    if (value == null)
                    {
                        throw new ArgumentException ("No such villain was found.");
                    }

                    var villainName = (string)value;
                    
                    //executing the command
                    command.CommandText = @"DELETE FROM MinionsVillains 
                                            WHERE VillainId = @villainId";

                    var minionsDeleted = command.ExecuteNonQuery();

                    command.CommandText = @"DELETE FROM Villains
                                            WHERE Id = @villainId";

                    command.ExecuteNonQuery();

                    transaction.Commit();

                    Console.WriteLine($"{villainName} was deleted.");
                    Console.WriteLine($"{minionsDeleted} minions were released.");
                }
                catch (ArgumentException ane)
                {
                    //проверяваме дали rollback не е fail
                    try
                    {
                        Console.WriteLine(ane.Message);
                        transaction.Rollback();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                catch (Exception e)
                {
                    //проверяваме дали rollback не е fail
                    try
                    {
                        Console.WriteLine(e.Message);
                        transaction.Rollback();
                    }
                    catch (Exception re)
                    {
                        Console.WriteLine(re.Message);
                    }
                }
            }
        }
    }
}
