using System;
using System.Collections.Generic;
using System.Text;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double AirConditionAddConsup = 1.6;
        public Truck(double fuelQUantity, double fuelConsuption, double tanCapasity)
            : base(fuelQUantity, fuelConsuption, tanCapasity)
        {
            this.FuelConsuption += AirConditionAddConsup;
        }

        public override void Refuel(double fuel)
        {
            if (this.FuelQUantity + fuel > this.TankCpacity)
            {
                throw new InvalidOperationException($"Cannot fit {fuel} fuel in the tank");
            }
            else if (fuel == 0)
            {
                throw new InvalidOperationException("Fuel must be a positive number");
            }
                fuel *= 0.95;
               base.Refuel(fuel);
        }
    }
}
