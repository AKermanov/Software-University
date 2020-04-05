using System;
using System.Linq;

namespace ConsoleApp90
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            Box<string> box = new Box<string>(string.Empty);

            for (int i = 0; i < count; i++)
            {
              string input = Console.ReadLine();
                box.Values.Add(input);
            }

            string targerItem = Console.ReadLine();
           int result = box.GreaterValuesThan(targerItem);

            Console.WriteLine(result);

        }
    }
}
