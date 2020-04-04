using System;
using System.Text;

namespace _08.CarSalesman
{
   public class Engine
	{
		private string model;
		private int power;
		private int? displacement;
		private string efficinaciy;

		public Engine(string model, int power)
		{
			this.Model = model;
			this.Power = power;
		}
		public Engine(string model, int power, int displacement): this(model, power)
		{
			this.Displacement = displacement;
		}
		public Engine(string model, int power, string efficinecy) : this(model, power)
		{
			this.Efficiency = efficinecy;
		}
		public Engine(string model, int power, int displacement, string efficinecy) : this(model, power)
		{
			this.Displacement = displacement;
			this.Efficiency = efficinecy;
		}
		public string Model
		{
			get { return model; }
			set { model = value; }
		}
		public int Power
		{
			get { return power; }
			set { power = value; }
		}
		public int? Displacement
		{
			get { return displacement; }
			set { displacement = value; }
		}
		public string Efficiency
		{
			get { return efficinaciy; }
			set { efficinaciy = value; }
		}
		public override string ToString()
		{
			string dispString = this.Displacement.HasValue ? this.Displacement.ToString() : "n/a";
			string effStr = String.IsNullOrEmpty(this.Efficiency) ? "n/a" : this.Efficiency;
			var sb = new StringBuilder();
			sb.
				AppendLine($"{this.Model}:")
				.AppendLine($"    Power: {this.Power}")
				.AppendLine($"    Displacement: {dispString}")
				.AppendLine($"    Efficiency: {effStr}");

			return sb.ToString().TrimEnd();
		}
	}
}
