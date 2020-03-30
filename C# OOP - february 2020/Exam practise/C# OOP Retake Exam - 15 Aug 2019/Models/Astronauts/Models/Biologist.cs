using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Bags;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Astronauts.Models
{
    public class Biologist : Astronaut
    {
        private const double DECREASES_ASTRONAUTS_OXYGEN = 5;
        private const double INITIAL_OXYGEN = 70;
        public Biologist(string name) 
            : base(name, INITIAL_OXYGEN)
        {
        }

        public override void Breath()
        {

            if (this.Oxygen - DECREASES_ASTRONAUTS_OXYGEN < 0)
            {
                this.Oxygen = 0;
            }
            else
            {
                this.Oxygen -= DECREASES_ASTRONAUTS_OXYGEN;
            }
        }
    }
}
