namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    public class AquariumsTests
    {
        [Test]
        public void FishCtorGetterWorkProperty()
        {
            var fish = new Fish("Ivan");

            Assert.That(fish.Name == "Ivan");
        }

        [Test]
        public void FishCtorBoolWorkProperty()
        {
            var fish = new Fish("Ivan");

            Assert.IsTrue(fish.Available);
        }
        [Test]
        public void AquariumCtorNameWorkProperty()
        {
            var fish = new Fish("Ivan");
            var aquarium = new Aquarium("Ivan", 4);

            Assert.That(aquarium.Name == "Ivan");
        }
        [Test]
        public void AquariumCtorCountWorkProperty()
        {
            var fish = new Fish("Ivan");
            var aquarium = new Aquarium("Ivan", 4);

            Assert.That(aquarium.Capacity == 4);
        }

        [Test]
        public void AquariumCtorCountThrowsExceptionWhenBelowZero()
        {
            Assert.Throws<ArgumentException>(()=> new Aquarium("Ivan", -1));
        }

        [Test]
        public void AquariumCtorNameThrowsExceptionWhenNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium(null, 3));
        }

        [Test]
        public void AquariumCtorNameThrowsExceptionWhenEmpty()
        {
            Assert.Throws<ArgumentNullException>(() => new Aquarium("", 3));
        }

        [Test]
        public void AquariumAddMehodWorkProperly()
        {
            var fish = new Fish("Ivan");
            var aquarium = new Aquarium("Ivan", 4);
            aquarium.Add(fish);
            Assert.That(aquarium.Count == 1);
        }

        [Test]
        public void AquariumCountMethodWork()
        {
            var fish = new Fish("Ivan");
            var aquarium = new Aquarium("Ivan", 4);
            Assert.That(aquarium.Count == 0);
        }
        [Test]
        public void AquariumAddMethodThrosExceptionWHenFUll()
        {
            var fish = new Fish("Ivan");
            var fish3 = new Fish("I1van");
            var fish2 = new Fish("I1va2n");
            var aquarium = new Aquarium("Ivan", 2);
            aquarium.Add(fish);
            aquarium.Add(fish3);
            Assert.Throws<InvalidOperationException>(() => aquarium.Add(fish2));
        }

        [Test]
        public void AquriumRemoveThrosExceptionWhenNull()
        {
            var fish = new Fish("Ivan");
            var fish3 = new Fish("I1van");
            var aquarium = new Aquarium("Ivan", 2);
            aquarium.Add(fish);
            aquarium.Add(fish3);
            Assert.Throws<InvalidOperationException>(() => aquarium.RemoveFish("I1va2n"));
        }
        [Test]
        public void AquriumRemoveWOrksProperly()
        {
            var fish = new Fish("Ivan");
            var fish1 = new Fish("I1van");
            var fish2 = new Fish("I1va23n");
            var fish3 = new Fish("I145van");
            var aquarium = new Aquarium("Ivan", 10);
            aquarium.Add(fish);
            aquarium.Add(fish1);
            aquarium.Add(fish2);
            aquarium.Add(fish3);
            aquarium.RemoveFish("Ivan");
            Assert.That(aquarium.Count == 3);
        }

        //sell fish
        [Test]
        public void AquriumSellFishThrosExceptionWhenNull()
        {
            var fish = new Fish("Ivan");
            var fish3 = new Fish("I1van");
            var aquarium = new Aquarium("Ivan", 2);
            aquarium.Add(fish);
            aquarium.Add(fish3);
            Assert.Throws<InvalidOperationException>(() => aquarium.SellFish("I1va2n"));
        }
        [Test]
        public void AquriumSellFishWorksCOrrectly()
        {
            var fish = new Fish("Ivan");
            var fish3 = new Fish("I1van");
            var aquarium = new Aquarium("Ivan", 2);
            aquarium.Add(fish);
            aquarium.Add(fish3);
            aquarium.SellFish("Ivan");
            Assert.IsFalse(fish.Available);
        }

        [Test]
        public void ToStringMethodWors()
        {
            var fish = new Fish("Nemo");
            var fish2 = new Fish("Petar");
            var aquarium = new Aquarium("Ivan", 2);
            aquarium.Add(fish);
            aquarium.Add(fish2);

            var fishToCompare = "Fish available at Ivan: Nemo, Petar";

            Assert.AreEqual(fishToCompare, aquarium.Report());
        }
        //report method should be tested
    }
}
