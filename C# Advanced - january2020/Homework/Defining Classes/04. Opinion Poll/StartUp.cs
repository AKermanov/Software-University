using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var family = new Family();
            var persons = new List<Person>();
            for (int i = 0; i < n; i++)
            {
                var member = Console.ReadLine().Split();
                var name = member[0];
                var age = int.Parse(member[1]);
                var currParson = new Person(name, age);
                persons.Add(currParson);
            }
            var over30 = persons.OrderBy(x => x.Name).Where(x => x.Age > 30).ToList();
            foreach (var item in over30)
            {
                Console.WriteLine($"{item.Name} - {item.Age}");
            }
           
        }
    }
}
