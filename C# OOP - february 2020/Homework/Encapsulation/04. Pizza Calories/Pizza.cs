using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pizza
{
   public class Pizza
    {
		private string name;
		private Dough dough;
		private List<Topping> toppings;

		public Pizza(string name, Dough dough)
		{
			this.Name = name;
			this.toppings = new List<Topping>();
			this.Dough = dough;
		}
		public int NumberOfToppings => this.toppings.Count;
		public double GetTotalCalories => toppings.Sum(x => x.GetToppingCalories()) + this.Dough.GetTotalCalories();
		public Dough Dough
		{
			get { return dough; }
			private set { dough = value; }
		}

		public string Name
		{
			get { return name; }
			private set 
			{
				if (value.Length < 1 || value.Length > 15 || value == string.Empty)
				{
					throw new Exception("Pizza name should be between 1 and 15 symbols.");
				}
				name = value; 
			}
		}
		public void AddTopping(Topping name)
		{
			if (toppings.Count == 10)
			{
				throw new Exception("Number of toppings should be in range [0..10].");
			}
			this.toppings.Add(name);
		}
	}
}
