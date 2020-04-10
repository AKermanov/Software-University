using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp31.FactoryPattern
{
    public class DruidFactory : HeroFactory
    {
        public DruidFactory(string name) 
            :base(name)
        {
        }

        public override BaseHero GetHero()
        {
            return new Druid(this.Name);
        }
    }
}
