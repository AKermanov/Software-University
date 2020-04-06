using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Rabbits
{
    public class Cage
    {
        private Cage()
        {
            this.data = new List<Rabbit>();
        }
        public Cage(string name, int capacity)
            : this()
        {
            this.Name = name;
            this.Capacity = capacity;

        }
        public string Name { get; set; }
        public int Capacity { get; set; }
        //public int Count => this.data.Count; същото е като долното
        public int Count 
        {
            get
            {
                return this.data.Count;
            } 
        }
        private readonly List<Rabbit> data;
        public void Add(Rabbit rabbit)
        {
            if (this.data.Count + 1 <= this.Capacity)
            {
               this.data.Add(rabbit);
            }
        }
        public bool RemoveRabbit(string name)
        {
            Rabbit rabbit = this.data
                 .FirstOrDefault(x => x.Name == name);
            if (rabbit != null)
            {
                this.data.Remove(rabbit);
                return true;
            }
            return false;
        }
        public void RemoveSpecies(string species)
        {
            this.data.RemoveAll(x => x.Species == species);
        }
        public Rabbit SellRabbit(string name)
        {
            var rabbitToSell = data.FirstOrDefault(x => x.Name == name); //връща един елемент
            if (rabbitToSell != null)
            {
                rabbitToSell.Available = false;
            }
            
            return rabbitToSell;
        }
        public Rabbit[] SellRabbitsBySpecies(string species)
        {

            Rabbit[] sellSpecies = this.data
                .Where(x => x.Species == species) //връща колекция от елементи и може да ги сложим в масив
                .ToArray();
            foreach (var item in sellSpecies)
            {
                item.Available = false;
            }
            return sellSpecies;
        }
        public string Report()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Rabbits available at {this.Name}:");
            foreach (var item in this.data.Where(r=>r.Available))
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
