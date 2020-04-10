using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp31.FactoryPattern
{
    public class WarriorFactory : HeroFactory
    {
        public WarriorFactory(string name) 
            : base(name)
        {
        }

        public override BaseHero GetHero()
        {
            return new Warrior(this.Name);
        }
    }
}
