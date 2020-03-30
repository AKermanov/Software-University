namespace SpaceStation.Models.Planets
{
    using System.Collections.Generic;
    using Repositories;

    public interface IPlanet
    {
        ICollection<string> Items { get; }

        string Name { get; }

        void AddItems(params string[] planetItems);

        void RemoveItem(string item);
    }
}