using AquaShop.Models.Aquariums.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AquaShop.Models.Aquariums.Models
{
   public class SaltwaterAquarium : Aquarium, IAquarium
    {
        private const int CAPACITY = 25;
        public SaltwaterAquarium(string name)
            : base(name, CAPACITY)
        {
        }
    }
}
