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

            var busInfo = Console.ReadLine()
              .Split();

            var carFuelQuantity = double.Parse(carInfo[1]);
            var carFuelConsuptnion = double.Parse(carInfo[2]);
            var carTankCapasity = double.Parse(carInfo[3]);

            var truckFuelQuantity = double.Parse(truckInfo[1]);
            var truckFuelConsuptnion = double.Parse(truckInfo[2]);
            var truckTankCapasity = double.Parse(truckInfo[3]);

            var busFuelQuantity = double.Parse(busInfo[1]);
            var busFuelConsuptnion = double.Parse(busInfo[2]);
            var busTankCapasity = double.Parse(busInfo[3]);

            var car = new Car(carFuelQuantity, carFuelConsuptnion,carTankCapasity);
            var truck = new Truck(truckFuelQuantity, truckFuelConsuptnion,truckTankCapasity);
            var bus = new Bus(busFuelQuantity,busFuelConsuptnion,busTankCapasity);

            var count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                try
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
                        else if (type == "Bus")
                        {
                            bus.IsEmpty = false;
                            DriveVehicle(bus, value);
                        }
                    }
                    else if (command == "Refuel")
                    {
                        if (type == "Car")
                        {
                            car.Refuel(value);
                        }
                        else if (type == "Truck")
                        {
                            truck.Refuel(value);
                        }
                        else if (type == "Bus")
                        {
                            bus.Refuel(value);
                        }
                    }
                    else if (command == "DriveEmpty")
                    {
                        bus.IsEmpty = true;
                        DriveVehicle(bus, value);
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine($"Car: {car.FuelQUantity:f2}");
            Console.WriteLine($"Truck: {truck.FuelQUantity:f2}");
            Console.WriteLine($"Bus: {bus.FuelQUantity:f2}");
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
