using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ParkingSystem.Tests
{

    public class SoftParkTest
    {
        private SoftPark softPark;

        [SetUp]
        public void Setup()
        {
            this.softPark = new SoftPark();
        }

        [Test]
        public void ParkingOnInvalidSpotShouldThrowException()
        {
            Assert.Throws<ArgumentException>(
                () => this.softPark.ParkCar("D1", new Car("", "")));
        }

        [Test]
        public void ParkingOnTakenPlaceShouldThrowException()
        {
            this.softPark.ParkCar("A1", new Car("BMW", "ABC123"));

            Assert.Throws<ArgumentException>(
                () => this.softPark.ParkCar("A1", new Car("Audi", "DEF456")));
        }

        [Test]
        public void TryParkingSameCarOnAnotherSpotShouldThrowException()
        {
            var car = new Car("BMW", "ABC123");

            this.softPark.ParkCar("A1", car);

            Assert.Throws<InvalidOperationException>(
                () => this.softPark.ParkCar("B1", car));
        }

        [Test]
        public void SuccessfulParkedCarShouldReturnMessage()
        {
            var regNumber = "ABC123";
            var car = new Car("BMW", regNumber);

            var actualResult = this.softPark.ParkCar("A1", car);
            var expectedResult = $"Car:{regNumber} parked successfully!";

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void TryRemoveCardFromNonExistingSpotShouldThrowException()
        {
            Assert.Throws<ArgumentException>(
                () => this.softPark.RemoveCar("D1", new Car("", "")));
        }

        [Test]
        public void TryRemoveCardFromWrongExistingSpotShouldThrowException()
        {
            this.softPark.ParkCar("A1", new Car("sad", "asdasd"));

            Assert.Throws<ArgumentException>(
                () => this.softPark.RemoveCar("A1", new Car("", "")));
        }

        [Test]
        public void SuccessfulRemoveCarShouldReturnExpectedMessage()
        {
            var car = new Car("BMW", "ABC123");

            this.softPark.ParkCar("A1", car);

            var actualResult = this.softPark.RemoveCar("A1", car);
            var expectedResult = $"Remove car:{car.RegistrationNumber} successfully!";

            Assert.That(actualResult, Is.EqualTo(expectedResult));
            Assert.That(softPark.Parking["A1"], Is.EqualTo(null));
        }
    }
}