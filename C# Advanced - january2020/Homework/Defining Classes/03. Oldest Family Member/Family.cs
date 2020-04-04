using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefiningClasses
{
    public class Family
    {
        private List<Person> people;

        public Family()
        {
            this.people = new List<Person>();
        }
        public void AddMember(Person person)
        {
            this.people.Add(person);
        }

        //public Person GetOldestMember()
        //{
        //    var person = people.OrderByDescending(a => a.Age).FirstOrDefault();
        //    return person;
        //}
        public Person GetOldestMember() //ова е съкратен синтаксис
        => people.OrderByDescending(a => a.Age).FirstOrDefault();

    }
}
