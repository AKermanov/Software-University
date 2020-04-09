using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
	public abstract class Animal //не може да се инстанцира, когато класа е бастрактен
	{
		private const string ERROR_MESSAGE = "Invalid input!";
		private string name;
		private int age;
		private string gender;

		public Animal(string name, int age, string gender)
		{
			this.Name = name;
			this.Age = age;
			this.Gender = gender;
		}

		public string Gender
		{
			get { return gender; }
			private set
			{
				if (value != "Male" && value != "Female")
				{
					throw new AggregateException(ERROR_MESSAGE);
				}
				gender = value;
			}
		}

		public int Age
		{
			get { return this.age; }
			set
			{
				if (value < 0)
				{

					throw new AggregateException(ERROR_MESSAGE);
				}
				this.age = value;
			}

		}

		public string Name
		{
			get { return this.name; }
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException(ERROR_MESSAGE);
				}
				this.name = value;
			}
		}

		public abstract string ProduceSound(); //няма тяло, всеки който наследява 
											   //базовия клас трябва да му даде тяло на мотода
											   //може да има бастрактен метод само в абстрактен клас


		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendLine($"{this.GetType().Name}"); //връща името на класа
			sb.AppendLine($"{this.Name} {this.Age} {this.Gender}");
			sb.AppendLine($"{this.ProduceSound()}");

			return sb.ToString().TrimEnd();
		}
	}
}
