using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Text.RegularExpressions;
using System.IO;


namespace DefiningClasses
{
   public class StartUp
    {
        public static void Main(string[] args)
        {
            Family family = new Family();
            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                string[] personInfo = Console.ReadLine()
                    .Split();
                string name = personInfo[0];
                int age = int.Parse(personInfo[1]);
                Person person = new Person(name, age);
                family.AddMember(person);    
            }

            Person oldesMembet = family.GetOldestMember();

            Console.WriteLine($"{oldesMembet.Name} {oldesMembet.Age}");
        }
    }
}
