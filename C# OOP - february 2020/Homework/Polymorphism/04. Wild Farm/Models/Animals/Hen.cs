using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals
{
    public class Hen : Bird
    {
        public Hen(string name, double weight, double wingSize) 
            : base(name, weight, wingSize)
        {
        }

        public override double WeightMultiplayer => 0.35;

        public override ICollection<Type> PrefferedFoods => new List<Type> { typeof(Vegetable), typeof(Meat), typeof(Seeds), typeof(Fruit) };

        public override string ProduseSund()
        {
            return "Cluck";
        }
    }
}
