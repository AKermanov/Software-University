using AquaShop.Models.Fish.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Fish.Models
{
    class SaltwaterFish : Fish, IFish
    {
        private const int INITIAL_SIZE = 5;
        private const int INITIAL_SIZE_WHEN_EAT = 2;
        public SaltwaterFish(string name, string species, decimal price)
            : base(name, species, price, INITIAL_SIZE)
        {
        }

        public override void Eat()
        {
            base.Size += INITIAL_SIZE_WHEN_EAT;
        }
    }
}
