using System;
using System.Text;

namespace _08.CarSalesman
{
   public class Car
	{
		private string  model;
		private Engine engine;
		private double? weight; //може да връща null
		private string color;
		public Car(string model, Engine engine)
		{
			this.Model = model;
			this.Engine = engine;
		}
		public Car(string model, Engine engine, double weight) : this(model, engine)
		{
			this.Weight = weight;
		}
		public Car(string model, Engine engine, string color) : this(model, engine)
		{
			this.Color = color;
		}
		public Car(string model, Engine engine, double weight, string color) : this(model,engine)
		{
			this.Weight = weight;
			this.Color = color;
		}
		public string Color
		{
			get { return color; }
			set { color = value; }
		}

		public double? Weight
		{
			get { return weight; }
			set { weight = value; }
		}

		public Engine Engine
		{
			get { return engine; }
			set { engine = value; }
		}

		public string  Model
		{
			get { return model; }
			set { model = value; }
		}

		public override string ToString()
		{
			string weightStr = this.Weight.HasValue ? this.Weight.ToString() : "n/a"; //има ли записана стойносто, ако има върни записаната стойност
																					  //ако няма "?" върни "n/a"
			string colorStr = String.IsNullOrEmpty(this.Color) ? "n/a" : this.Color;
			var sb = new StringBuilder();   //ползваме sb закощо конкатенацията е бавен процес
			sb
				.AppendLine($"{this.Model}:")
				.AppendLine($"  {this.Engine}")
				.AppendLine($"  Weight: {weightStr}") 
				.AppendLine($"  Color: {colorStr}");

			return sb.ToString().TrimEnd();
		}
	}
}
