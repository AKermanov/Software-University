using System;

namespace ConsoleApp47
{
   public class StartUp
    {
        static void Main(string[] args)
        {
			try
			{
				var name = Console.ReadLine();
				var age = int.Parse(Console.ReadLine());
				var person = new Child(name, age);
				Console.WriteLine(person);
			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}
        }
    }
}
