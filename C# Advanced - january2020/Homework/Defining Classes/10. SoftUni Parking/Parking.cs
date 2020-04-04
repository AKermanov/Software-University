using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SoftUniParking
{
  public class Parking
    {
		private List<Car> cars;
		private int capacity;
		private int count;

		

		public Parking(int capacity)
		{
			this.Cars = new List<Car>();
			this.Capacity = capacity;
		}
		public List<Car> Cars
		{
			get { return cars; }
			set { cars = value; }
		}
		public int Capacity
		{
			get { return capacity; }
			set { capacity = value; }
		}
		public int Count
		{
			get { return cars.Count(); }
		}
		public void AddCar(Car car)
		{
			bool isCarInParking = cars.Any(x => x.RegistrationNumber == car.RegistrationNumber);

			if (!isCarInParking)
			{
				if (cars.Count < Capacity)
				{
					cars.Add(car);
					Console.WriteLine($"Successfully added new car {car.Make} {car.RegistrationNumber}");
				}
				else
				{
					Console.WriteLine("Parking is full!");
				}
			}
			else
			{
				Console.WriteLine("Car with that registration number, already exists!");
			}
		}

		public void RemoveCar(string registrationNumber)
		{
			bool isCarInParking = cars.Any(x => x.RegistrationNumber == registrationNumber);

			if (!isCarInParking)
			{
				Console.WriteLine("Car with that registration number, doesn't exist!");
			}
			else
			{
				cars.RemoveAll(x => x.RegistrationNumber == registrationNumber);
				Console.WriteLine($"Successfully removed {registrationNumber}");
			}
		}
		public void GetCar(string registrationNumber)
		{
			var car = cars.Find(c => c.RegistrationNumber == registrationNumber).ToString();
			Console.WriteLine(car);
		}
		public void RemoveSetOfRegistrationNumber(List<string> registrationNumbers)
		{
			
		}
	}
}
