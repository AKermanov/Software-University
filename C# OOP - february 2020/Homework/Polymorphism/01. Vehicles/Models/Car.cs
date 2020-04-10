namespace Vehicles.Models
{
    public class Car : Vehicle
    {
        private const double AirConditionAddConsup = 0.9;
        public Car(double fuelQUantity, double fuelConsuption) 
            : base(fuelQUantity, fuelConsuption)
        {
            this.FuelConsuption += AirConditionAddConsup;
        }
    }
}
