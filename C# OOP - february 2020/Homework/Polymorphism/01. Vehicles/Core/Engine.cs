using System;
using System.Collections.Generic;
using System.Text;
using Vehicles.Models;

namespace Vehicles.Core
{
   public class Engine
    {
        public void Run()
        {
            var carInfo = Console.ReadLine()
                .Split();

            var truckInfo = Console.ReadLine()
               .Split();

            var carFuelQuantity = double.Parse(carInfo[1]);
            var carFuelConsuptnion = double.Parse(carInfo[2]);

            var truckFuelQuantity = double.Parse(truckInfo[1]);
            var truckFuelConsuptnion = double.Parse(truckInfo[2]);

            var car = new Car(carFuelQuantity, carFuelConsuptnion);
            var truck = new Truck(truckFuelQuantity, truckFuelConsuptnion);

            var count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                var inputInfo = Console.ReadLine()
                    .Split();

                var command = inputInfo[0];
                var type = inputInfo[1];
                var value = double.Parse(inputInfo[2]);

                if (command == "Drive")
                {
                    if (type == "Car")
                    {
                        DriveVehicle(car, value);
                    }
                    else if (type == "Truck")
                    {
                        DriveVehicle(truck, value);
                    }
                }
                else if (command == "Refuel")
                {
                    if (type =="Car")
                    {
                        car.Refuel(value);
                    }
                    else if (type == "Truck")
                    {
                        truck.Refuel(value);
                    }
                }
            }

            Console.WriteLine($"Car: {car.FuelQUantity:f2}");
            Console.WriteLine($"Truck: {truck.FuelQUantity:f2}");
        }

        private static void DriveVehicle(Vehicle car, double value)
        {
            bool canTravel = car.Drive(value);

            string result = !canTravel 
                ? $"{car.GetType().Name} needs refueling" 
                : $"{car.GetType().Name} travelled {value} km";

            Console.WriteLine(result);
        }
    }
}
