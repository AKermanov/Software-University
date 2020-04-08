namespace NeedForSpeed
{
   public class Vehicle
    {
        public Vehicle(int horsePower, double fuel)
        {
            this.HorsePower = horsePower;
            this.Fuel = fuel;
        }
        public virtual double FuelConsumption => 1.25; 
        public int HorsePower { get; set; }
        public double Fuel { get; set; }

       public virtual void Drive(double kilometers)
        {
            this.Fuel -= this.FuelConsumption;
        }
    }
}
