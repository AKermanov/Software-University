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

            int[] indexes = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int a = indexes[0];
            int b = indexes[1];

            box.Swap(a, b);

            Console.WriteLine(box.ToString());

        }
    }
}
