using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Astronauts.Models;
using SpaceStation.Models.Planets;
using SpaceStation.Repositories.Models;
using SpaceStation.Utilities.Messages;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SpaceStation.Models.Mission;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private AstronautRepository astronautRepository;
        private PlanetRepository planetRepository;
        private int exploredPlanetsCount;
        private readonly IMission mission;
        public Controller()
        {
            this.astronautRepository = new AstronautRepository();
            this.planetRepository = new PlanetRepository();
            this.exploredPlanetsCount = 0;
            this.mission = new Mission();
        }

        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut = null;
            if (type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type== "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type== "Meteorologist")
            {
                astronaut = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }
            astronautRepository.Add(astronaut);

            return string.Format(OutputMessages.AstronautAdded,
                astronaut.GetType().Name,
                astronaut.Name);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);

            planet.AddItems(items);
            this.planetRepository.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var astronauts = astronautRepository.Models.Where(x => x.Oxygen >= 60).ToList();

            if (astronauts.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            IPlanet planet = this.planetRepository.FindByName(planetName);
            this.mission.Explore(planet, astronauts);
            this.exploredPlanetsCount++;

            return string.Format(OutputMessages.PlanetExplored,
                planet.Name,
                astronauts.Where(a => a.CanBreath == false).Count());
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{this.exploredPlanetsCount} planets were explored!");
            sb.AppendLine("Astronauts info:");

            foreach (IAstronaut astronaut in this.astronautRepository.Models)
            {
                sb.AppendLine($"Name: { astronaut.Name}");
                sb.AppendLine($"Oxygen: {astronaut.Oxygen}");
                sb.AppendLine($"Bag items: {(astronaut.Bag.Items.Count == 0 ? "none" : string.Join(", ", astronaut.Bag.Items))} ");
            }

            return sb.ToString().TrimEnd();
        }
        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = astronautRepository.FindByName(astronautName);
            if (astronaut == null)
            {
                var message = string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName);
                throw new InvalidOperationException(message);
            }

            astronautRepository.Remove(astronaut);
            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}
