using System;

namespace Vehicles.Models
{
    public abstract class Vehicle
    {
        private double fuelQUantity;
        private double fuleConsupthin;
        private double tankCpacity;



        protected Vehicle(double fuelQUantity, double fuelConsuption, double tanCapasity)
        {
            this.TankCpacity = tanCapasity;
            this.FuelQUantity = fuelQUantity;
            this.FuelConsuption = fuelConsuption;
        }

        public double FuelQUantity // има грешка, занулява горивото
        {
            get
            {
                return fuelQUantity;
            }
            protected set
            {
                if (value > this.TankCpacity)
                {
                    fuelQUantity = 0;
                }
                else
                {
                    fuelQUantity = value;
                }

            }
        }
        public double TankCpacity
        {
            get
            {
                return tankCpacity;
            }
            private set
            {
                tankCpacity = value;
            }
        }


        public virtual double FuelConsuption
        {
            get { return fuleConsupthin; }
            protected set { fuleConsupthin = value; }
        }

        public virtual bool Drive(double distance)
        {
            bool canDrive = this.FuelQUantity - this.FuelConsuption * distance >= 0;
            if (canDrive)
            {
                this.FuelQUantity -= this.FuelConsuption * distance;
                return true;
            }
            return false;
        }


        public virtual void Refuel(double fuel)
        {
            if (fuel <= 0)
            {
                throw new InvalidOperationException("Fuel must be a positive number");
            }
            else if (fuel > this.TankCpacity)
            {
                throw new InvalidOperationException($"Cannot fit {fuel} fuel in the tank");
            }
            this.FuelQUantity += fuel;
        }
    }
}
