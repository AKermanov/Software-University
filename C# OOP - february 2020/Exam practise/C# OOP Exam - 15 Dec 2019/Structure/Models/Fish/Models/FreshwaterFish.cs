using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish.Models
{
    class FreshwaterFish : Fish , IFish
    {
        private const int INITIAL_SIZE = 3;
        public FreshwaterFish(string name, string species, decimal price) 
            : base(name, species, price, INITIAL_SIZE)
        {
        }

        public override void Eat()
        {
            base.Size += INITIAL_SIZE;
        }
    }
}
