using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {
        [SetUp]
        public void Setup()
        {
        }
        //this.Model = model;
        // this.HorsePower = horsePower;
        //this.CubicCentimeters = cubicCentimeters;
        [Test]
        public void MotorcycleModelPropertyWorks()
        {
            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);

            Assert.AreEqual("BMW", motorcycle.Model);
        }

        [Test]
        public void MotorcycleHorsePowerlPropertyWorks()
        {
            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);

            Assert.AreEqual(80, motorcycle.HorsePower);
        }

        [Test]
        public void MotorcycleCubicSmPropertyWorks()
        {
            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);

            Assert.AreEqual(2341.12, motorcycle.CubicCentimeters);
        }

        //this.Name = name;
        // this.Motorcycle = motorcycle;

        [Test]
        public void UnitRiderClassThrowExceptionWhenInvalidName()
        {
            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);
            /* UnitRider rider;
             Assert.That(() =>
             {
                rider = new UnitRider(null, motorcycle);
             },
             //Assert
             Throws
                .Exception.InstanceOf<ArgumentNullException>()
                .With.Message.EqualTo("Name cannot be null!"));
                */
            Assert.Throws<ArgumentNullException>(() => new UnitRider(null, motorcycle), "Name cannot be null!");
        }
        [Test]
        public void UnitRiderClassWorksProperly()
        {
            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);

            var rider = new UnitRider("Pesho", motorcycle);

            Assert.AreEqual("Pesho", rider.Name);
        }

        [Test]
        public void UnitRiderMotorcyclePopretyWorksProperly()
        {
            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);

            var rider = new UnitRider("Pesho", motorcycle);

            Assert.AreEqual(motorcycle, rider.Motorcycle);
        }

        [Test]
        public void RaceEntryConstructorWorkProperly()
        {
            var race = new RaceEntry();

            Assert.That(race.Counter == 0);
        }

        [Test]
        public void AddRiderThrowsExceptionWhenRiderIsNull()
        {
            var race = new RaceEntry();

            Assert.That(() =>
            {
                race.AddRider(null);
            },
              Throws
                 .Exception.InstanceOf<InvalidOperationException>()
                 .With.Message.EqualTo("Rider cannot be null."));
        }

        [Test]
        public void AddRiderExistingRiderThrowException()
        {
            var race = new RaceEntry();

            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);

            var rider = new UnitRider("Pesho", motorcycle);

            race.AddRider(rider);

            Assert.That(() =>
            {
                race.AddRider(rider);
            },
              Throws
                 .Exception.InstanceOf<InvalidOperationException>()
                 .With.Message.EqualTo("Rider Pesho is already added."));
        }


        [Test]
        public void AddRiderWorkProperly()
        {
            var race = new RaceEntry();

            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);

            var rider = new UnitRider("Pesho", motorcycle);

            race.AddRider(rider);

            Assert.That(race.Counter == 1);
        }

        [Test]
        public void AddRiderGivesRightMessage()
        {
            var race = new RaceEntry();

            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);

            var rider = new UnitRider("Pesho", motorcycle);

            race.AddRider(rider);

            var result = "Rider Pesho added in race.";

            Assert.AreEqual("Rider Pesho added in race.", result);
        }


        [Test]
        public void  CalculateAverageHorsePowerThrowsExceptionWhenTherAreLessThenTwoPlayers()
        {
            var race = new RaceEntry();

            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);


            var rider = new UnitRider("Pesho", motorcycle);

            race.AddRider(rider);

            Assert.That(() =>
            {
                race.CalculateAverageHorsePower();
            },
               Throws
                  .Exception.InstanceOf<InvalidOperationException>()
                  .With.Message.EqualTo("The race cannot start with less than 2 participants."));
        }

        [Test]
        public void CalculateAverageHorseGetAvaregeHpWhenThereAreMoreThanTwoRaicers()
        {
            var race = new RaceEntry();

            var motorcycle = new UnitMotorcycle("BMW", 80, 2341.12);
            var motorcycle1 = new UnitMotorcycle("Yamaha", 120, 1020.12);
            var motorcycle2 = new UnitMotorcycle("KTM", 104, 1241.12);

            var rider = new UnitRider("Pesho", motorcycle);
            var rider1 = new UnitRider("Gosho", motorcycle1);
            var rider2 = new UnitRider("Kurti", motorcycle2);

            race.AddRider(rider);
            race.AddRider(rider1);
            race.AddRider(rider2);

            var list = new List<UnitRider>();
            list.Add(rider);
            list.Add(rider1);
            list.Add(rider2);

            var value = list.Select(x => x.Motorcycle.HorsePower)
                .Average();

            Assert.AreEqual(value, race.CalculateAverageHorsePower());
        }

    }
}