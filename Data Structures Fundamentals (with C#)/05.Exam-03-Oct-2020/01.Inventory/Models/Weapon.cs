namespace _01.Inventory.Models
{
    using _01.Inventory.Interfaces;

    public abstract class Weapon : IWeapon
    {
        public Weapon(int id, int maxCapacity, int ammunition)
        {
            this.Id = id;
            this.MaxCapacity = maxCapacity;
            this.Ammunition = ammunition;
        }

        public int Id { get; private set; }
        public int Ammunition { get; set; }
        public int MaxCapacity { get; set; }
        public Category Category { get; set; }

        public int CompareTo(object obj)
        {
            var current = (IWeapon)obj;

            return current.Id - this.Id;
        }
        public override bool Equals(object obj)
        {
            IWeapon other = (IWeapon)obj;

            return other.Id == this.Id;
        }
    }
}
