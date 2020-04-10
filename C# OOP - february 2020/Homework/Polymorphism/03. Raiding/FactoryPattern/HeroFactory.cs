using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp31.FactoryPattern
{
   public abstract class HeroFactory
    {
        protected HeroFactory(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
        public abstract BaseHero GetHero();
    }
}
