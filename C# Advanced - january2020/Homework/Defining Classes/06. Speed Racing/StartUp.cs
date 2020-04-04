using System;
using System.Collections.Generic;
using System.Linq;

namespace _06._Speed_Racing
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var carsInRace = new List<Car>();

            for (int i = 0; i < n; i++)
            {
                var input = Console.ReadLine().Split();
                var model = input[0];
                var fuleAmount = double.Parse(input[1]);
                var fuelConsum = double.Parse(input[2]);
                var currentCar = new Car(model, fuleAmount,fuelConsum, 0);
                carsInRace.Add(currentCar);
            }

            string command = null;
            while ((command=Console.ReadLine()) != "End")
            {
                var cmdArgs = command.Split();
                var carModel = cmdArgs[1];
                var amountOfKm = double.Parse(cmdArgs[2]);
                Car car = carsInRace.First(x => x.Model == carModel);
                car.Move(amountOfKm);
            }
            Console.WriteLine(string.Join(Environment.NewLine,carsInRace));
        }

    }
}
