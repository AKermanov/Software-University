using System;

namespace FootballTeamGenerator
{
   public class Player
    {
		private string name;
		private int endurance;
		private int sprint;
		private int dribble;
		private int passing;
		private int shooting;
		public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
		{
			this.Name = name;
			this.Endurance = endurance;
			this.Sprint = sprint;
			this.Dribble = dribble;
			this.Passing = passing;
			this.Shooting = shooting;
		}
		
		public int Shooting
		{
			get { return this.shooting; }
		private	set { this.shooting = value; }
		}

		public int Passing
		{
			get { return this.passing; }
			private set {
				if (value < 1 || value > 100)
				{
					throw new Exception("Passing should be between 0 and 100.");
				}
				this.passing = value; 
			}
		}

		public int Dribble
		{
			get { return this.dribble; }
			private set {
				if (value < 1 || value > 100)
				{
					throw new Exception("Dribble should be between 0 and 100.");
				}
				this.dribble = value; }
		}

		public int Sprint
		{
			get { return this.sprint; }
			private set {
				if (value < 1 || value > 100)
				{
					throw new Exception("Sprint should be between 0 and 100.");
				}
				this.sprint = value; }
		}

		public int Endurance
		{
			get { return this.endurance; }
			private set {
				if (value < 1 || value > 100)
				{
					throw new Exception("Endurance should be between 0 and 100.");
				}
				this.endurance = value; 
			}
		}

		public string Name
		{
			get { return this.name; }
			private set 
			{
				if (value == null || value == string.Empty)
				{
					throw new Exception("A name should not be empty.");
				}
				this.name = value;
			}
		}

		public int Stats => (int)Math.Round((this.Dribble + this.Endurance + this.Passing + this.Shooting + this.Sprint) / (double)5);
	}
}
