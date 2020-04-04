
using System;

namespace _06._Speed_Racing
{
    public class Car
    {
        private string model;
        private double fuelAmount;
        private double fuelConsumptionPerKilometer;
        private double travelledDistance;

        public Car(string model, double fuealAmount, double fuelConsumptionPerKilometer, double travelledDistance)
        {
            this.Model = model;
            this.FuelAmount = fuealAmount;
            this.FuelConsumptionPerKilometer = fuelConsumptionPerKilometer;
            this.TravelledDistance = travelledDistance;
        }

        public double TravelledDistance
        {
            get { return travelledDistance; }
            set { travelledDistance = value; }
        }

        public double FuelConsumptionPerKilometer
        {
            get { return fuelConsumptionPerKilometer; }
            set { fuelConsumptionPerKilometer = value; }
        }

        public double FuelAmount
        {
            get { return fuelAmount; }
            set { fuelAmount = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        public void Move(double distance)
        {
            double consumption = this.FuelConsumptionPerKilometer * distance;

            if (consumption <= this.FuelAmount)
            {
               this.FuelAmount -= distance * this.FuelConsumptionPerKilometer;
                Math.Round(FuelAmount);
                this.TravelledDistance += distance;
            }
            else
            {
                Console.WriteLine("Insufficient fuel for the drive");
            }
        }
        public override string ToString()
        {
            return $"{this.Model} {this.FuelAmount:f2} {this.TravelledDistance}"; 
        }
    }
}
