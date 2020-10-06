namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.Tracing;
    using System.Linq;
    using System.Threading;

    public class Inventory : IHolder
    {
        private List<IWeapon> weapons;

        public Inventory()
        {
            this.weapons = new List<IWeapon>();
        }
        public int Capacity => this.weapons.Count;

        public void Add(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public void Clear()
        {
            this.weapons.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            return this.GetById(weapon.Id) != null;
        }

        public void EmptyArsenal(Category category)
        {
            foreach (var weapon in weapons)
            {
                if (weapon.Category == category)
                {
                    weapon.Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            var currentWeapon = this.GetById(weapon.Id);

            if (currentWeapon is null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (currentWeapon.Ammunition >= ammunition)
            {
                currentWeapon.Ammunition -= ammunition;
                return true;
            }
            return false;
        }

        public IWeapon GetById(int id)
        {
            for (int i = 0; i < this.Capacity; i++)
            {
                var currentWeapon = this.weapons[i];

                if (currentWeapon.Id == id)
                {
                    return currentWeapon;
                }
            }

            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return this.weapons.GetEnumerator();
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            var currentWeapon = this.GetById(weapon.Id);

            if (currentWeapon is null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (weapon.MaxCapacity <= ammunition)
            {
                currentWeapon.Ammunition = ammunition;
                return ammunition;
            }
            else
            {
                currentWeapon.Ammunition = currentWeapon.MaxCapacity;
                return currentWeapon.MaxCapacity;
            }
        }

        public IWeapon RemoveById(int id)
        {
            var currentWeapon = this.GetById(id);

            if (currentWeapon is null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            weapons.RemoveAt(id);

            return currentWeapon;
        }

        //performing test may fail
        public int RemoveHeavy()
        {
           return weapons.RemoveAll(x => x.Category == Category.Heavy);
        }

        public List<IWeapon> RetrieveAll()
        {
            if (weapons.Count == 0)
            {
                return new List<IWeapon>();
            }
            return weapons.ToList();
        }

        //copyed!
        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            var result = new List<IWeapon>(this.Capacity);
            int lowerBoundIndex = (int)lower;
            int upperBoundIndex = (int)upper;

            for (int i = 0; i < this.Capacity; i++)
            {
                var entity = this.weapons[i];
                int entityStatusIndex = (int)entity.Category;

                if (entityStatusIndex >= lowerBoundIndex &&
                    entityStatusIndex <= upperBoundIndex)
                {
                    result.Add(entity);
                }
            }

            return result;
        }
        
        //copyed
        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            int indexOfFIrst = this.weapons.IndexOf(firstWeapon);
            int indexOfSecond = this.weapons.IndexOf(secondWeapon);
            this.ValidateEntity(indexOfFIrst);
            this.ValidateEntity(indexOfSecond);

            if (firstWeapon.Category == secondWeapon.Category)
            {
                var temp = this.weapons[indexOfFIrst];
                this.weapons[indexOfFIrst] = this.weapons[indexOfSecond];
                this.weapons[indexOfSecond] = temp;
            }
           
        }

        private void ValidateEntity(int index)
        {
            if (index == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }
    }
}
