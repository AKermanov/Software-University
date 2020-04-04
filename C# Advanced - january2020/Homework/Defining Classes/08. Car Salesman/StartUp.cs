using System;
using System.Collections.Generic;
using System.Linq;

namespace _08.CarSalesman
{
  public class StartUp
    {
        static void Main(string[] args)
        {
            HashSet<Engine> engines = new HashSet<Engine>();
            List<Car> cars = new List<Car>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Engine engine = null;
                var engineArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                var model = engineArgs[0];
                var power = int.Parse(engineArgs[1]);

                if (engineArgs.Length == 4)
                {
                    var displacement = int.Parse(engineArgs[2]);
                    var efficiency = engineArgs[3];
                    engine = new Engine(model, power, displacement, efficiency);
                }
                else if (engineArgs.Length == 3)
                {
                    int displacmenet;
                    bool isDIsplacement = int.TryParse(engineArgs[2], out displacmenet); //опитваме да парснем, ако успее връща
                                                                                       //true и записва резултата в displacement
                    if (isDIsplacement)
                    {
                        engine = new Engine(model, power, displacmenet);
                    }
                    else
                    {
                        engine = new Engine(model, power, engineArgs[2]);
                    }
                }
                else if (engineArgs.Length == 2)
                {
                    engine = new Engine(model, power);
                }
                if (engine != null)
                {
                engines.Add(engine);
                }
            }

            int m = int.Parse(Console.ReadLine());

            for (int i = 0; i < m; i++)
            {
                var carArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                Car car = null;
                string model = carArgs[0];
                Engine engine = engines.First(e => e.Model == carArgs[1]); // бъркам в хашсета с двигателите и взимаме първия двигател 
                                                                           //на който модела == на това което е в инпута
                if (carArgs.Length == 2)
                {
                    car = new Car(model, engine);
                }
                else if (carArgs.Length == 3)
                {
                    double weight;
                    bool isWeight = double.TryParse(carArgs[2], out weight);
                    if (isWeight)
                    {
                        car = new Car(model, engine, weight); // тук записваме колата с whight
                    }
                    else
                    {
                        car = new Car(model, engine, carArgs[2]); //тук записваме колата с цвят
                    }
                }
                else if (carArgs.Length == 4)
                {
                    int weight = int.Parse(carArgs[2]);
                    string color = carArgs[3];
                    car = new Car(model, engine, weight, color);
                }
                if (car != null)
                {
                    cars.Add(car);
                }
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }
    }
}
