using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza
{
   public class Topping
	{
		private string toppingName;
		private double toppingWieght;

		public Topping(string name, double weight)
		{
			this.ToppingName = name;
			this.ToppingWight = weight;
				
		}
		public double ToppingWight
		{
			get { return toppingWieght; }
			private set 
			{
				if (value > 50 || value <= 0)
				{
					throw new Exception($"{this.ToppingName} weight should be in the range [1..50].");
				}
				toppingWieght = value;
			}
		}


		public string ToppingName
		{
			get { return toppingName; }
			private set 
			{
				if (!ToppingValidator.IsValidTopping(value.ToLower()))
				{
					throw new Exception($"Cannot place {value} on top of your pizza.");
				}
				toppingName = value;
			}
		}

		public double GetToppingCalories()
		{
			return this.ToppingWight * 2 * ToppingValidator.GetToppingModifire(this.ToppingName);

		}

	}
}
