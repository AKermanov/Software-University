using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    public class Bus : Vehicle
    {
        private double ACAddCOsn = 1.4;
        private double deffFuelConsup;
        public Bus(double fuelQUantity, double fuelConsuption, double tanCapasity) 
            : base(fuelQUantity, fuelConsuption, tanCapasity)
        {
            this.deffFuelConsup = fuelConsuption;
            this.ACAddCOsn += fuelConsuption;
        }
        public bool IsEmpty { get; set; }

        public override bool Drive(double distance)
        {
            if (!this.IsEmpty)
            {
                this.FuelConsuption = this.ACAddCOsn;
            }
            else
            {
                this.FuelConsuption = this.deffFuelConsup;
            }
           return base.Drive(distance);
           
        }
    }
}
