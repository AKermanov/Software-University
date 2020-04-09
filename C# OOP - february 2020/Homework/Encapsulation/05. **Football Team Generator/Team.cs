using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FootballTeamGenerator
{
  public class Team
    {
        private List<Player> players;
        private string name;
        public Team(string name)
        {
            this.Name = name;
            this.players = new List<Player>();
        }

        public string Name {
            get
            {
                return name;
            }
            private set
            {

                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("A name should not be empty.");
                }
                this.name = value;
            } }
        public void AddPlayer(Player player)
        {
            this.players.Add(player);

        }
        public int Rating => CalculateTeamRating();
        private int CalculateTeamRating()
        {
            if (this.players.Any())
            {
                return (int)Math.Round(this.players.Average(p => p.Stats));
            }
            else
            {
                return 0;
            }
        }
        public void RemovePlayer(string player)
        {
            if (!this.players.Any(p => p.Name == player))
            {
                throw new ArgumentException($"Player {player} is not in {this.Name} team. ");
            }

            Player pl = this.players.FirstOrDefault(p => p.Name == player);
            this.players.Remove(pl);
        }

        public override string ToString()
        {
            return $"{this.name} - {this.Rating}";
        }
    }
}
