using System;
using System.Collections.Generic;
using System.Text;

namespace ViceCity.Models.Guns
{
   public class Rifle : Gun
    {
        private const int InitialBulletPerBarrel = 50;
        private const int InitialTotalBulets = 500;
        private const int BulletsPerFire = 5;

        public Rifle(string name)
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
