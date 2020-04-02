using System;
using System.Collections.Generic;
using System.Text;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Players.Contracts;
using ViceCity.Repositories;
using ViceCity.Repositories.Contracts;

namespace ViceCity.Models.Players
{
	public abstract class Player : IPlayer
	{
		private string name;
		private int lifePoints;
		private IRepository<IGun> gunRepository;

		protected Player(string name, int lifePoints)
		{
			this.Name = name;
			this.LifePoints  = lifePoints;
			this.gunRepository  = new GunRepository();
		}

		public string Name
		{
			get
			{
				return name;
			}
			private set
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new ArgumentNullException("Player's name cannot be null or a whitespace!");
				}
				name = value;
			}
		}

		public int LifePoints 
		{
			get { return lifePoints; }
		private	set
			{
				if (value < 0)
				{
					throw new ArgumentNullException("Player life points cannot be below zero!");
				}
				lifePoints = value;
			}

		}
		public void TakeLifePoints(int points)
		{
			this.lifePoints -= points;
			if (this.lifePoints < 0)
			{
				this.lifePoints = 0;
			}
		}
		public IRepository<IGun> GunRepository => this.gunRepository;

		public bool IsAlive => this.LifePoints  > 0;

	
	}
}
