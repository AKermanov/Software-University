using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp105
{
    class Program
    {
        static void Main(string[] args)
        {
			try
			{
                List<Person> people = new List<Person>();
                List<Product> products = new List<Product>();

                string[] inputPeople = Console.ReadLine()
                       .Split(';', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < inputPeople.Length; i++)
                {
                    string[] currentPerson = inputPeople[i].Split('=');
                    string name = currentPerson[0];

                    decimal money = decimal.Parse(currentPerson[1]);
                    Person person = new Person(name, money);

                    people.Add(person);
                }

                string[] inputProducts = Console.ReadLine()
                    .Split(';', StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < inputProducts.Length; i++)
                {
                    string[] cProduct = inputProducts[i].Split('=');
                    string name = cProduct[0];

                    decimal cost = decimal.Parse(cProduct[1]);
                    Product currentProduct = new Product(name, cost);

                    products.Add(currentProduct);
                }

                string input = Console.ReadLine();
                while (input != "END")
                {
                    string[] inputInfo = input.Split();
                    string name = inputInfo[0];
                    string productName = inputInfo[1];

                    Person person = people.FirstOrDefault(x => x.Name == name);
                    Product product = products.FirstOrDefault(x => x.Name == productName);
                    person.AddToBag(product);

                    input = Console.ReadLine();
                }

                foreach (var person in people)
                {
                    Console.WriteLine(person);
                }
            }
			catch (Exception ex)
			{

                Console.WriteLine(ex.Message);
			}
        }
    }
}
