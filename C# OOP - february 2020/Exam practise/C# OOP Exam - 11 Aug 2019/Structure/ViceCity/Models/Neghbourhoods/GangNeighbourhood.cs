using System;
using System.Collections.Generic;
using System.Text;
using ViceCity.Models.Neghbourhoods.Contracts;
using ViceCity.Models.Players.Contracts;

namespace ViceCity.Models.Neghbourhoods
{
    public class GangNeighbourhood : INeighbourhood
    {
        public void Action(IPlayer mainPlayer, ICollection<IPlayer> civilPlayers)
        {
            foreach  (var currentGun in mainPlayer.GunRepository .Models)
            {
                foreach (var currentCivilPlayr in civilPlayers)
                {
                    while (currentCivilPlayr.IsAlive && currentGun.CanFire)
                    {
                        currentCivilPlayr.TakeLifePoints(currentGun.Fire());
                    }

                    if (!currentGun.CanFire)
                    {
                        break;
                    }
                }
            }

            foreach (var currentCivilPlayr in civilPlayers)
            {
                if (!currentCivilPlayr.IsAlive)
                {
                    continue;
                }
                foreach (var curretGun in currentCivilPlayr.GunRepository .Models)
                {
                    while (mainPlayer.IsAlive && curretGun.CanFire)
                    {
                        mainPlayer.TakeLifePoints(curretGun.Fire());

                    }

                    if (!mainPlayer.IsAlive)
                    {
                        break;
                    }
                }

                if (!mainPlayer.IsAlive)
                {
                    break;
                }
            }
        }
    }
}
