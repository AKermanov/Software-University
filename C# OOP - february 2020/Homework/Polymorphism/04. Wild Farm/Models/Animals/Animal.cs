using System;
using System.Collections.Generic;
using WildFarm.Exceptions;
using WildFarm.Models.Animals.Contracts;
using WildFarm.Models.Foods.Contracts;

namespace WildFarm.Models.Animals
{
    public abstract class Animal : IAnimal
    {
        private const string UneatableFoodMessage = "{0} does not eat {1}!";
        protected Animal(string name, double weight)
        {
            this.Name = name;
            this.Weight = weight;
        }

        public string Name { get; private set; }

        public double Weight { get; private set; }

        public int FoodEaten { get; private set; }
        //Asbtract because they are different for every type of animal
        public abstract double WeightMultiplayer { get; }
        public abstract ICollection<Type> PrefferedFoods { get; }  //всеки клас е някакъв тип данни и може да се вземе типа с getType.
        //тук можем да слижим Type Meat, Type Veretable. Не може да бъде от IFood, понеже класовете са референтни типове

        public void Feed(IFood food)
        {
            if (!this.PrefferedFoods.Contains(food.GetType())) //съдържа ли типа тази храна
            {
                throw new UneatableFoodException
                    (string.Format(UneatableFoodMessage, this.GetType().Name, food.GetType().Name));
            }
            this.Weight += this.WeightMultiplayer * food.Quantity;
            this.FoodEaten += food.Quantity;
        }

        public abstract string ProduseSund();

        public override string ToString()
        {
            return $"{this.GetType().Name} [{this.Name}, ";
        }

    }
}
