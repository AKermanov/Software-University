using RawData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            HashSet<Car> cars = new HashSet<Car>();

            for (int i = 0; i < n; i++)
            {
                var carAgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var model = carAgs[0];

                var engineSpeed = int.Parse(carAgs[1]);
                var enginePower = int.Parse(carAgs[2]);

                var cargoWeight = int.Parse(carAgs[3]);
                var cargoType = carAgs[4];

                Engine engine = CreateEngine(engineSpeed, enginePower);
                Cargo cargo = CreateCargo(cargoWeight, cargoType);
                List<Tire> tires = new List<Tire>();
                GetTires(carAgs, tires);

                var car = new Car(model,engine,cargo,tires);
                cars.Add(car);
            }

            var command = Console.ReadLine();
            if (command == "fragile")
            {
                HashSet<Car> result = cars
                     .Where(c => c.Cargo.Type == "fragile" &&   //бъркаме в cargo за да вземем типа
                            c.Tires.Any(t => t.Pressure < 1))  // tires е списък и когато сложим Any, достъпваме 1 гума и взимаме pressure
                     .ToHashSet();
                Console.WriteLine(string.Join(Environment.NewLine,result));
            }
            else if (command == "flamable")
            {
                HashSet<Car> result = cars
                    .Where(c => c.Cargo.Type == "flamable" &&
                    c.Engine.Power > 250)
                    .ToHashSet();
                Console.WriteLine(string.Join(Environment.NewLine, result));
            }
        }

        private static void GetTires(string[] carAgs, List<Tire> tires)
        {
            for (int j = 5; j < carAgs.Length; j += 2)
            {
                double pressure = double.Parse(carAgs[j]);
                int age = int.Parse(carAgs[j + 1]);
                Tire tire = CreateTire(pressure, age);
                tires.Add(tire);
            }
        }

        static Engine CreateEngine( int speed, int power)
        {
            return new Engine(speed, power);
        } //disine pattern - factory pattern - подаваме аргументи и ни 
        static Cargo CreateCargo(int weight, string type)
        {
            return new Cargo(weight, type);
        } //връщат инстанции на обекти
        static Tire CreateTire(double pressure, int age)
        {
            return new Tire(age, pressure);
        }
    }
}
