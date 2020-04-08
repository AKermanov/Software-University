using AquaShop.Models.Decorations.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Decorations.Models
{
   public class Ornament : Decoration, IDecoration
    {
        private const int COMFORT_LEVEL = 1;
        private const int PRICE = 5;
        public Ornament() 
            : base(COMFORT_LEVEL, PRICE)
        {
        }
    }
}
