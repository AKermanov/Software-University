using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp47
{
   public class Child:Person
    {
        public Child(string name, int age)
            :base(name, age)
        {

        }

        private int age;

        public override int Age
        {
            get { return age; }
            set 
            {
                age = value; 
            }
        }

    }
}
