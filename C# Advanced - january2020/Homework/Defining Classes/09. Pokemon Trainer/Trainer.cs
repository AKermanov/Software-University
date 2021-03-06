﻿using System.Collections.Generic;

namespace PokemonTrainer
{
   public class Trainer
	{
		private string name;
		private int badges;
		private List<Pokemon> pokemons;

		public Trainer(string name)
		{
			this.Name = name;
			this.badges = 0;
			this.Pokemons = new List<Pokemon>();
		}
		public List<Pokemon> Pokemons
		{
			get { return pokemons; }
			set { pokemons = value; }
		}

		public int Badges
		{
			get { return badges; }
			set { badges = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public override string ToString()
		{
			return $"{Name} {Badges} {Pokemons.Count}";
		}

	}
}
