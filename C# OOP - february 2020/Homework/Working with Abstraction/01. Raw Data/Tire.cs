namespace RawData
{
   public class Tire
	{
		private int age;
		private double pressure;

		public Tire(int age, double pressure)
		{
			this.Age = age;
			this.Pressure = pressure;
		}

		public double Pressure
		{
			get { return pressure; }
			set { pressure = value; }
		}

		public int Age
		{
			get { return age; }
			set { age = value; }
		}

	}
}
