using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double AirConditionAddConsup = 1.6;
        public Truck(double fuelQUantity, double fuelConsuption)
            : base(fuelQUantity, fuelConsuption)
        {
            this.FuelConsuption += AirConditionAddConsup;
        }

        public override void Refuel(double fuel)
        {
            fuel *= 0.95;
            base.Refuel(fuel);
        }
    }
}
