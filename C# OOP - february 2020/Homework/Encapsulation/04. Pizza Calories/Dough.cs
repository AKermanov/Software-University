using System;
namespace Pizza
{
   public class Dough
	{
        private string flowerType;
        private string backingTechnique;
        private double weight;

        public Dough(string flowerType, string bakingTechType, double weight)
        {
            this.FlowerType = flowerType;
            this.BackingTechnique = bakingTechType;
            this.Weight = weight;
        }

        public string FlowerType
        {
            get
            {
                return flowerType;
            }
           private set
            {
                if (!DoughValidator.IsValidFlourType(value.ToLower()))
                {
                    throw new Exception("Invalid type of dough.");
                }
                flowerType = value;
            }
        }

        public string BackingTechnique
        {
            get
            {
                return backingTechnique;
            }
            private set
            {
                if (!DoughValidator.IsValidBackingTechnique(value.ToLower()))
                {
                    throw new Exception("Invalid type of backing technique.");
                }
                backingTechnique = value;
            }
        }

        public double Weight
        {
            get { return weight; }
            private set
            {
                if (value < 1 || value > 200)
                {
                    throw new Exception("Dough weight should be in the range [1..200].");
                }
                weight = value;
            }
        }

        public double GetTotalCalories()
        {
            return this.Weight
                * 2
                * DoughValidator.GetBackingTechniqueModifier(this.BackingTechnique)
                * DoughValidator.GetFlourModifier(this.FlowerType);
        }


    }
}
