using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp31.FactoryPattern
{
    public class RogueFactory : HeroFactory
    {
        public RogueFactory(string name)
            :base(name)
        {
        }

        public override BaseHero GetHero()
        {
            return new Rogue(this.Name);
        }
    }
}
