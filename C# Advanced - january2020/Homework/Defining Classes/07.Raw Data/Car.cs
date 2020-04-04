using System;
using System.Collections.Generic;
using System.Text;

namespace RawData
{
   public class Car
	{
		private string model;
		private Engine engine;
		private Cargo cargo;
		private List<Tire> tires;
		public Car(string model, int engineSpeed, int enginePower, int cargoWegiht,
			string cargoType, double tire1Pressure, int tire1Age, double tire2Pressure,
			int tire2Age, double tire3Pressure, int tire3Age, double tire4Pressure, int tire4Age)
		{
			this.Model = model;
			this.Engine = new Engine(engineSpeed, enginePower);
			this.Cargo = new Cargo(cargoWegiht, cargoType);
			this.tires = new List<Tire>()
			{
				new Tire(tire1Age, tire1Pressure),
				new Tire(tire2Age, tire2Pressure),
				new Tire(tire3Age, tire3Pressure),
				new Tire(tire4Age, tire4Pressure)
			};
		}

		public Car(string model, Engine engine, Cargo cargo, List<Tire> tires)
		{
			this.Model = model;
			this.Engine = engine;
			this.Cargo = cargo;
			this.tires = tires;
		}
		public List<Tire> Tires
		{
			get { return tires; }
			
		}

		public Cargo Cargo
		{
			get { return cargo; }
			set { cargo = value; }
		}

		public Engine Engine
		{
			get { return engine; }
			set { engine = value; }
		}

		public string Model
		{
			get { return model; }
			set { model = value; }
		}

		public override string ToString()
		{
			return this.Model;	
		}
	}
}
