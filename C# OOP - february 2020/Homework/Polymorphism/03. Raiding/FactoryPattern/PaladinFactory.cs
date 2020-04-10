using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp31.FactoryPattern
{
    public class PaladinFactory : HeroFactory
    {
        public PaladinFactory(string name) 
            : base(name)
        {
           
        }

        public override BaseHero GetHero()
        {
           return new Paladin(this.Name);
        }
    }
}
