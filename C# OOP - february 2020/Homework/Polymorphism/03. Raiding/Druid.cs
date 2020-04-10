using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp31
{
    public class Druid : BaseHero
    {
        private const int DruidPower = 80;
        public Druid(string name) 
            : base(name, DruidPower)
        {
        }

        public override string CastAbility()
        {
            return $"{nameof(Druid)} - {this.Name} healed for {DruidPower}";
        }
    }
}
