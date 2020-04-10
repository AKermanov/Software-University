namespace Vehicles.Models
{
    public class Car : Vehicle
    {
        private const double AirConditionAddConsup = 0.9;
        public Car(double fuelQUantity, double fuelConsuption, double tanCapasity) 
            : base(fuelQUantity, fuelConsuption,tanCapasity)
        {
            this.FuelConsuption += AirConditionAddConsup;
        }
    }
}
