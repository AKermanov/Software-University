using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViceCity.Core.Contracts;
using ViceCity.Models.Guns;
using ViceCity.Models.Guns.Contracts;
using ViceCity.Models.Neghbourhoods;
using ViceCity.Models.Neghbourhoods.Contracts;
using ViceCity.Models.Players;
using ViceCity.Models.Players.Contracts;

namespace ViceCity.Core
{
    public class Controller : IController
    {
        private readonly IPlayer mainPlayer;
        private readonly ICollection<IPlayer> civilPlayers;
        private readonly ICollection<IGun> guns;
        private readonly INeighbourhood GangNeighbourhood;
        public Controller()
        {
            this.mainPlayer = new MainPlayer();
            this.civilPlayers = new List<IPlayer>();
            this.guns = new List<IGun>();
            this.GangNeighbourhood = new GangNeighbourhood();
        }

        public string AddGun(string type, string name)
        {
            IGun gun = null;
            if (nameof(Pistol) == type)
            {
                gun = new Pistol(name);
            }
            else if (nameof(Rifle) == type)
            {
                gun = new Rifle(name);
            }
            else
            {
                return "Invalid gun type!";
            }

            this.guns.Add(gun);
            return $"Successfully added {name} of type: {type}";
        }

        public string AddGunToPlayer(string name)
        {
            if (this.guns.Count == 0)
            {
                return "There are no guns in the queue!";
            }
            IGun gun = this.guns.FirstOrDefault();
            IPlayer civilPlayer = this.civilPlayers.FirstOrDefault(p => p.Name == name);
            string mesage = string.Empty;
            if (name == "Vercetti")
            {
                this.mainPlayer.GunRepository.Add(gun);
                mesage = $"Successfully added {gun.Name} to the Main Player: Tommy Vercetti";
            }
            else if (civilPlayer != null)
            {
                civilPlayer.GunRepository.Add(gun);
                mesage = $"Successfully added {gun.Name} to the Civil Player: {name}";
            }
            else
            {
                return "Civil player with that name doesn't exists!";
            }
            this.guns.Remove(gun);

            return mesage;
        }

        public string AddPlayer(string name)
        {
            IPlayer player = new CivilPlayer(name);
            this.civilPlayers.Add(player);
            return $"Successfully added civil player: {name}!";
        }

        public string Fight()
        {
            int mainPlayrLifePoints = this.mainPlayer.LifePoints;
            int totalSumLifePoints = this.civilPlayers.Sum(p => p.LifePoints);

            this.GangNeighbourhood.Action(this.mainPlayer, this.civilPlayers);


            int aliveCivilPlayersCOunt = this.civilPlayers.Count(p => p.IsAlive);
            int mainPlayrLifePointsAfterFight = this.mainPlayer.LifePoints;
            int totalSumLifePointsAfterFight = this.civilPlayers.Sum(p => p.LifePoints);
            var message = string.Empty;

            //TODO are civil players injured

            if (mainPlayrLifePoints == mainPlayrLifePointsAfterFight
                && totalSumLifePoints == aliveCivilPlayersCOunt)
            {
                message = "Everything is okay!";
            }
            else
            {
                message = "A fight happened:" + Environment.NewLine;
                message += $"Tommy live points: {this.mainPlayer.LifePoints}!" + Environment.NewLine;
                message += $"Tommy has killed: {this.civilPlayers.Count - aliveCivilPlayersCOunt} players!" + Environment.NewLine;
                message += $"Left Civil Players: {aliveCivilPlayersCOunt}!";

            }
            return message;
        }
    }
}
