using AquaShop.Models.Decorations.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Decorations.Models
{
    public class Plant : Decoration, IDecoration
    {
        private const int COMFORT_LEVEL = 5;
        private const int PRICE = 10;
        public Plant() 
            :base(COMFORT_LEVEL, PRICE)
        {

        }
    }
}
