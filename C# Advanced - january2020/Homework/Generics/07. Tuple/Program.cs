using System;
using System.Linq;

namespace ConsoleApp90
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] inputPersonInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string[] inputPersonBeer = Console.ReadLine().Split();
            string[] inputNumbersInfo = Console.ReadLine().Split();

            string fullName = inputPersonInfo[0] + " " + inputPersonInfo[1];
            string address = inputPersonInfo[2];

            string name = inputPersonBeer[0];
            int liters = int.Parse(inputPersonBeer[1]);

            int myInte = int.Parse(inputNumbersInfo[0]);
            double myDouble = double.Parse(inputNumbersInfo[1]);

            MyTuple<string, string> personInfo = new MyTuple<string, string>(fullName, address);
            MyTuple<string, int> personBEER = new MyTuple<string, int>(name, liters);
            MyTuple<int, double> numberInfo = new MyTuple<int, double>(myInte, myDouble);

            Console.WriteLine(personInfo);
            Console.WriteLine(personBEER);
            Console.WriteLine(numberInfo);

        }
    }
}
