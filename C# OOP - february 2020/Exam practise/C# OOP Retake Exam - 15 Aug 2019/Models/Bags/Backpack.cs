using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceStation.Models.Bags
{
    class Backpack : IBag
    {
        private readonly List<string> item;

        public Backpack()
        {
            this.item = new List<string>();
        }

        public ICollection<string> Items => this.item.AsReadOnly();

        public void AddItem(string item)
        {
            this.item.Add(item);
        }

    }
}
