using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp31
{
  public abstract class BaseHero
    {
		private int power;
		private string name;

		protected BaseHero(string name, int power)
		{
			this.Name = name;
			this.Power = power;
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}
		public int Power
		{
			get { return power; }
			private set { power = value; }
		}
		public abstract string CastAbility();
	
	}
}
