using SpaceStation.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private readonly List<string> items;

        public Planet(string name)
        {
            this.Name = name;
            this.items = new List<string>();
        }

        public ICollection<string> Items => this.items.AsReadOnly();

        public string Name
        {
            get => this.name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidPlanetName);
                }

                this.name = value;
            }
        }

        public void AddItems(params string[] planetItems)
        {
            foreach (var item in planetItems)
            {
                this.items.Add(item);
            }
        }

        public void RemoveItem(string item)
        {
            this.items.Remove(item);
        }
    }
}
