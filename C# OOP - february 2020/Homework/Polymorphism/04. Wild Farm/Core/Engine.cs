using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Core.Contracts;
using WildFarm.Exceptions;
using WildFarm.Factories;
using WildFarm.Models;
using WildFarm.Models.Animals;
using WildFarm.Models.Animals.Contracts;
using WildFarm.Models.Foods.Contracts;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private ICollection<IAnimal> animals;
        private FoodFactory foodFactory;
        public Engine()
        {
            this.animals = new List<IAnimal>();
            this.foodFactory = new FoodFactory();
        }
        public void Run()
        {
            string cmd;
            while ((cmd = Console.ReadLine()) != "End")
            {
                var animalArgs = cmd.Split();

                var foodArgs = Console.ReadLine().Split();

                var animalType = animalArgs[0];
                var name = animalArgs[1];
                var weight = double.Parse(animalArgs[2]);

                IAnimal animal = ProduseAnimal(animalArgs, animalType, name, weight);
                IFood food = this.foodFactory.ProduceFood(foodArgs[0], int.Parse(foodArgs[1]));

                this.animals.Add(animal);
                Console.WriteLine(animal.ProduseSund());

                try
                {
                    animal.Feed(food);
                }
                catch (UneatableFoodException ufe)
                {
                    Console.WriteLine(ufe.Message);
                }
            }

            foreach (var item in this.animals)
            {
                Console.WriteLine(item);
            }
        }

        private static IAnimal ProduseAnimal(string[] animalArgs, string animalType, string name, double weight)
        {
            IAnimal animal = null;

            if (animalType == "Owl")
            {
                var wingSize = double.Parse(animalArgs[3]);
                animal = new Owl(name, weight, wingSize);
            }
            else if (animalType == "Hen")
            {
                var wingSize = double.Parse(animalArgs[3]);
                animal = new Hen(name, weight, wingSize);
            }
            else
            {
                var leivingRegion = animalArgs[3];
                if (animalType == "Dog")
                {
                    animal = new Dog(name, weight, leivingRegion);
                }
                else if (animalType == "Mouse")
                {
                    animal = new Mouse(name, weight, leivingRegion);
                }
                else
                {
                    var breed = animalArgs[4];
                    if (animalType == "Cat")
                    {
                        animal = new Cat(name, weight, leivingRegion, breed);
                    }
                    else if (animalType == "Tiger")
                    {
                        animal = new Tiger(name, weight, leivingRegion, breed);
                    }
                }
            }

            return animal;
        }
    }
}
