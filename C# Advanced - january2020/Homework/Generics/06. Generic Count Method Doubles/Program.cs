using System;
using System.Linq;

namespace ConsoleApp90
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int count = int.Parse(Console.ReadLine());
            Box<double> box = new Box<double>(0);

            for (int i = 0; i < count; i++)
            {
                double input = double.Parse(Console.ReadLine());
                box.Values.Add(input);
            }

            double targerItem = double.Parse(Console.ReadLine());
            int result = box.GreaterValuesThan(targerItem);

            Console.WriteLine(result);

        }
    }
}
