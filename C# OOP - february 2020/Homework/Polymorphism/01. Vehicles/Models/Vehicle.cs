namespace Vehicles.Models
{
    public abstract class Vehicle
    {
        private double fuelQUantity;
        private double fuleConsupthin;

        protected Vehicle(double fuelQUantity, double fuelConsuption)
        {
            this.FuelQUantity = fuelQUantity;
            this.FuelConsuption = fuelConsuption;
        }

        public double FuelQUantity
        {
            get { return fuelQUantity; }
            protected set { fuelQUantity = value; }
        }


        public double FuelConsuption
        {
            get { return fuleConsupthin; }
           protected set { fuleConsupthin = value; }
        }

        public bool Drive(double distance)
        {
            bool canDrive = this.fuelQUantity - this.FuelConsuption * distance >= 0;
            if (canDrive)
            {
                this.fuelQUantity -= this.fuleConsupthin * distance;
                return true;
            }
            return false;
        }

        public virtual void Refuel(double fuel)
        {
            this.FuelQUantity += fuel;
        }
    }
}
