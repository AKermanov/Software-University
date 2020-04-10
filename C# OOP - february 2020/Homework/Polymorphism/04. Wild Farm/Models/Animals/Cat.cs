using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Cat : Feline
    {
        public Cat(string name, double weight, string livingRegion, string breed) 
            : base(name, weight, livingRegion, breed)
        {
        }

        public override double WeightMultiplayer => 0.30;

        public override ICollection<Type> PrefferedFoods => new List<Type> { typeof(Vegetable), typeof(Meat) };

        public override string ProduseSund()
        {
            return "Meow";
        }
    }
}
