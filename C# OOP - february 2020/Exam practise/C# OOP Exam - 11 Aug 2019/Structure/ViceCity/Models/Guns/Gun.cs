using System;
using System.Collections.Generic;
using System.Text;
using ViceCity.Models.Guns.Contracts;

namespace ViceCity.Models.Guns
{
    public abstract class Gun : IGun
    {
        private string name;
        public int bulletsPerBarrel;
        public int totalBullets;

        protected Gun(string name, int bulletsPerBarrel, int totalBullets)
        {
            this.Name = name;
            this.BulletsPerBarrel = bulletsPerBarrel;
            this.TotalBullets = totalBullets;
        }

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name cannot be null or a white space!");
                }
                name = value;
            }

        }
        public int BulletsPerBarrel
        {
            get
            {
                return bulletsPerBarrel;
            }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException("Bullets cannot be below zero!");
                }
                bulletsPerBarrel = value;
            }

        }
        public int TotalBullets
        {
            get
            {
                return totalBullets;
            }
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentNullException("Total bullets cannot be below zero!");
                }
                totalBullets = value;
            }
        }

        public bool CanFire => this.BulletsPerBarrel > 0 || this.totalBullets > 0;

        public abstract int Fire();

        protected void Reload(int barrelCapacity)
        {
            if (this.TotalBullets >= barrelCapacity)
           {
                this.BulletsPerBarrel = barrelCapacity;
               this.TotalBullets -= barrelCapacity;
           }
        }

        protected int DecreaseBullets(int bullets)
        {
           int firedBullets = 0;
            if (this.BulletsPerBarrel - bullets >= 0)
          {
              this.BulletsPerBarrel -= bullets;
               firedBullets = bullets;
           }
           return firedBullets;
       }

    }
}
