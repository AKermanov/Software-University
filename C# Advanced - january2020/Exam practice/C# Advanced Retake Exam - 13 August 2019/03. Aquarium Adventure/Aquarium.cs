using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AquariumAdventure
{
    public class Aquarium
    {
        private HashSet<Fish> fishInPool;
        private Aquarium()
        {
            this.fishInPool = new HashSet<Fish>();
        }
        public Aquarium(string name, int capacity, int size)
            : this()
        {
            this.Name = name;
            this.Capacity = capacity;
            this.Size = size;
        }

        public string Name { get; set; }
        public int Capacity { get; set; }
        public int Size { get; set; }

        public void Add(Fish fish)
        {
            if (!this.fishInPool.Any(f => f.Name == fish.Name) &&
                !(this.fishInPool.Count >= this.Capacity))
            {
                this.fishInPool.Add(fish);
            }
        }
        public bool Remove(string name)
        {
            int removeEl = 0;
            if (fishInPool.Any())
            {
                removeEl = fishInPool.RemoveWhere(x => x.Name == name);
            }
            bool isRemoved = removeEl == 1 ? true : false;
            return isRemoved;
        }

        public Fish FindFish(string name)
        {

            Fish fish = fishInPool.FirstOrDefault(x => x.Name == name);
            return fish;
        }
        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Aquarium: {this.Name} ^ Size: {this.Size}");
            foreach (var item in fishInPool)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
