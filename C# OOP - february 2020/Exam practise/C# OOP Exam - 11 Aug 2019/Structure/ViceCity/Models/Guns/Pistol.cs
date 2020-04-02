using System;
using System.Collections.Generic;
using System.Text;

namespace ViceCity.Models.Guns
{
    public class Pistol : Gun
    {
        private const int InitialBulletPerBarrel = 10;
        private const int InitialTotalBulets = 100;
        private const int BulletsPerFire = 1;

        public Pistol(string name) 
            : base(name, InitialBulletPerBarrel, InitialTotalBulets)
        {
        }

        public override int Fire()
        {
            if (this.BulletsPerBarrel < BulletsPerFire)
            {
                this.Reload(InitialBulletPerBarrel);
            }

            int firedBullets = this.DecreaseBullets(BulletsPerFire);
            return firedBullets;
            
        }

      
    }
}
