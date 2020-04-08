using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Models.Aquariums.Models
{
   public class Aquarium : IAquarium
    {
        private string name;
        private readonly ICollection<IFish> fish;
        private readonly ICollection<IDecoration> decoration;

        public Aquarium(string name, int capacity)
        {
            this.Name = name;
            this.Capacity = capacity;
            this.fish = new List<IFish>();
            this.decoration = new List<IDecoration>();
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidAquariumName);
                }
                this.name = value;
            }
        }
       
        public int Capacity { get; private set; }

        public int Comfort => this.decoration.Select(x => x.Comfort).Sum();

        public ICollection<IDecoration> Decorations => this.decoration.ToList().AsReadOnly();

        public ICollection<IFish> Fish => this.fish.ToList().AsReadOnly();

        public void AddDecoration(IDecoration decoration)
        {
            this.decoration.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
            if (this.Fish.Count == Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.NotEnoughCapacity);
            }

            this.fish.Add(fish);
        }

        public void Feed()
        {
            foreach (var fish in this.Fish)
            {
                fish.Eat();
            }
        }

        public string GetInfo()
        {
            if (this.Fish.Count == 0)
            {
                return "none";
            }

            var sb = new StringBuilder();
            sb.AppendLine($"{this.Name} ({this.GetType().Name}):");
            var fishInAquarium = this.Fish.Count == 0 ? "none" : string.Join(", ", this.Fish.Select(x=>x.Name).ToList());
            sb.AppendLine($"Fish: {fishInAquarium}");
            sb.AppendLine($"Decorations: {this.Decorations.Count}");
            sb.AppendLine($"Comfort: {this.Comfort}");

            return sb.ToString().TrimEnd();
        }

        public bool RemoveFish(IFish fish)
        {
            return this.fish.Remove(fish);
        }
    }
}
