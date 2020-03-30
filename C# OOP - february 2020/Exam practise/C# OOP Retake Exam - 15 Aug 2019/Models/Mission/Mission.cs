using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            while (true)
            {
                IAstronaut currentAstronauth = astronauts.FirstOrDefault(a => a.CanBreath);

                if (currentAstronauth== null)
                {
                    break;
                }

                while (planet.Items.Count != 0)
                {
                    var currentItem = planet.Items.FirstOrDefault();
                    currentAstronauth.Breath();

                    currentAstronauth.Bag.AddItem(currentItem);
                    planet.RemoveItem(currentItem);

                    if (!currentAstronauth.CanBreath)
                    {
                        break;
                    }
                }

                if (planet.Items.Count ==0)
                {
                    break;
                }
            }
        }
    }
}
