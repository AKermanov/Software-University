using CarManager;
using NUnit.Framework;
using System;

namespace Tests
{
    public class CarTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructurShouldInitilizeCorrectly()
        {
            var make = "VW";
            var model = "Golf";
            double vuelCOnsup = 2;
            double fuelCapacity = 100;

            Car car = new Car(make, model, vuelCOnsup, fuelCapacity);
            Assert.AreEqual(make, car.Make);
            Assert.AreEqual(vuelCOnsup, car.FuelConsumption);
            Assert.AreEqual(fuelCapacity, car.FuelCapacity);
        }

        [Test]
        public void ModelShouldThrowArgumenExceptionWhenNameIsNull()
        {
            var make = "VW";
            string model = null;
            double vuelCOnsup = 2;
            double fuelCapacity = 100;

            Assert.Throws<ArgumentException>(() => new Car(make, model,
                vuelCOnsup, fuelCapacity));
        }

        [Test]
        public void MakeShouldThrowArgumenExceptionWhenNameIsNull()
        {
            string make = null;
            string model = "Golf";
            double vuelCOnsup = 2;
            double fuelCapacity = 100;

            Assert.Throws<ArgumentException>(() => new Car(make, model,
                vuelCOnsup, fuelCapacity));
        }

        [Test]
        public void FuelCOnsuptionShouldThrowArgumenExceptionWhenIsBelowZero()
        {
            string make = null;
            string model = "Golf";
            double vuelCOnsup = -10;
            double fuelCapacity = 100;

            Assert.Throws<ArgumentException>(() => new Car(make, model,
                vuelCOnsup, fuelCapacity));
        }
        [Test]
        public void FuelCOnsuptionShouldThrowArgumenExceptionWhenIsZero()
        {
            string make = null;
            string model = "Golf";
            double vuelCOnsup = 0;
            double fuelCapacity = 100;

            Assert.Throws<ArgumentException>(() => new Car(make, model,
                vuelCOnsup, fuelCapacity));
        }

        [Test]
        public void FuelCapasityShouldThrowArgumenExceptionWhenIsZero()
        {
            string make = null;
            string model = "Golf";
            double vuelCOnsup = 10;
            double fuelCapacity = 0;

            Assert.Throws<ArgumentException>(() => new Car(make, model,
                vuelCOnsup, fuelCapacity));
        }

        [Test]
        public void FuelCapasityShouldThrowArgumenExceptionWhenIsBelowZero()
        {
            string make = null;
            string model = "Golf";
            double vuelCOnsup = 10;
            double fuelCapacity = -5;

            Assert.Throws<ArgumentException>(() => new Car(make, model,
                vuelCOnsup, fuelCapacity));
        }

        [Test]
        [TestCase(null, "Golf", 10, 20)]
        [TestCase("Vw", null, 10, 20)]
        [TestCase("Vw", "Golf", -10, 20)]
        [TestCase("Vw", "Golf", 0, 20)]
        [TestCase("Vw", "Golf", 10, -20)]
        [TestCase("Vw", "Golf", 10, 0)]
        public void AllPropertirsShouldTrowArgumenExceptionForInvalidValues(string make, string model,
            double fuelCOnsuption, double fuelCapasity)
        {
            Assert.Throws<ArgumentException>(() => new Car(make, model,
                fuelCOnsuption, fuelCapasity));
        }

        [Test]
        public void ShouldRefuleNormally()
        {
            string make = "Vw";
            string model = "Golf";
            double vuelCOnsup = 2;
            double fuelCapacity = 100;

            Car car = new Car(make, model, vuelCOnsup, fuelCapacity);
            car.Refuel(10);

            int expectedFuelAmount = 10;
            double actualFuelAMout = car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAMout);
        }


        [Test]
        public void ShouldRefuleUntilTheTotalFuelCapacity()
        {
            string make = "Vw";
            string model = "Golf";
            double vuelCOnsup = 2;
            double fuelCapacity = 100;

            Car car = new Car(make, model, vuelCOnsup, fuelCapacity);
            car.Refuel(150);

            int expectedFuelAmount = 100;
            double actualFuelAMout = car.FuelAmount;

            Assert.AreEqual(expectedFuelAmount, actualFuelAMout);
        }


        [Test]
        [TestCase(-10)]
        [TestCase(0)]
        public void RefuleShouldThrowAgumentExceptionWhenInputAmouthIsBelowZero(double inputAmouth)
        {
            string make = "Vw";
            string model = "Golf";
            double vuelCOnsup = 2;
            double fuelCapacity = 100;

            Car car = new Car(make, model, vuelCOnsup, fuelCapacity);
            car.Refuel(150);

            Assert.Throws<ArgumentException>(() => car.Refuel(inputAmouth));
        }
        [Test]
        public void ShouldDriveNormally()
        {
            string make = "Vw";
            string model = "Golf";
            double vuelCOnsup = 2;
            double fuelCapacity = 100;

            Car car = new Car(make, model, vuelCOnsup, fuelCapacity);
            car.Refuel(20);
            car.Drive(20);

            double expectedFUelAMount = 19.6;
            double actualFUelAmouth = car.FuelAmount;

            Assert.AreEqual(expectedFUelAMount, actualFUelAmouth);
        }

        [Test]
        public void DrvieShouldThorwInvalidOperationExceptionWhenFuelAmouthIsNoEnoguth()
        {
            string make = "Vw";
            string model = "Golf";
            double vuelCOnsup = 2;
            double fuelCapacity = 100;

            Car car = new Car(make, model, vuelCOnsup, fuelCapacity);

            Assert.Throws<InvalidOperationException>(() => car.Drive(20));
        }
    }
}