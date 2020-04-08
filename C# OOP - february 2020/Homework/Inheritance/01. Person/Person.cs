namespace ConsoleApp47
{ using System;
   public class Person
		
    {
		private int age;
		private string name;

		public Person(string name, int age)
		{
			this.Name = name;
			this.Age = age;
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public virtual int Age
		{
			get { return age; }
			set
			{
				//if (age <=0)
				//{
					//throw new InvalidOperationException("Age can't be zero or negative");
				//}
				age = value; 
			}
		}

		public override string ToString()
		{
			return $"Name: {this.Name}, Age: {this.Age}";
		}
	}
}
