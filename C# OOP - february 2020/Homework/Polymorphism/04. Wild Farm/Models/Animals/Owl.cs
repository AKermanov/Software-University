using System;
using System.Collections.Generic;
using System.Text;
using WildFarm.Models.Foods;
using WildFarm.Models.Foods.Contracts;

namespace WildFarm.Models.Animals
{
    public class Owl : Bird
    {
        public Owl(string name, double weight, double wingSize) 
            : base(name, weight, wingSize)
        {
        }

        public override double WeightMultiplayer => 0.25;
        //разликата
        //get.Type - ползва се в/у променливи
        //typeof - когато нямаме променлива, подаваме статично името на класа и му взимаме типа
        public override ICollection<Type> PrefferedFoods => new List<Type> { typeof(Meat) };

        public override string ProduseSund()
        {
            return $"Hoot Hoot";
        }
    }
}
