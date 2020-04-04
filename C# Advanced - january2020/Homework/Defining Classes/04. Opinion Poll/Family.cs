using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
   public class Family
    {
		private List<Person> familyMembers;

		public Family()
		{
			this.familyMembers = new List<Person>();
		}

		public List<Person> FamilyMembers
		{
			get { return familyMembers; }
			set { familyMembers = value; }
		}
		public void AddMember(Person person)
		{
			this.familyMembers.Add(person);
		}
		public Person GetOldestMember()
		{
		    Person person = familyMembers.OrderByDescending(a => a.Age).FirstOrDefault();
		   return person;
		}
	}
}
