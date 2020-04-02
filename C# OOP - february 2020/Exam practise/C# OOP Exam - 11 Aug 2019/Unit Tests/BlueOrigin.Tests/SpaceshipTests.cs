namespace BlueOrigin.Tests
{
    using System;
    using NUnit.Framework;

    public class SpaceshipTests
    {
       [Test]
       public void Count_Should_ReturnCOrrectRes()
        {
            Spaceship spaship = new Spaceship("Name One", 10);

            Assert.AreEqual(0,spaship.Count);
        }

        [Test]
        public void Name_Should_ReturnCOrrectRes()
        {
            Spaceship spaship = new Spaceship("Name One", 10);

            Assert.AreEqual("Name One", spaship.Name);
        }

        [Test]
        public void Name_Null_Should_ThrowExcepiton()
        {
            Assert.Throws<ArgumentNullException>(() => new Spaceship(null, 10));
        }

        [Test]
        public void Capacity_Should_ReturnCOrrectRes()
        {
            Spaceship spaship = new Spaceship("Name One", 10);

            Assert.AreEqual(10, spaship.Capacity);
        }

        [Test]
        public void Capacity_LessTheZero_Should_ThrowExcepiton()
        {
            Assert.Throws<ArgumentException>(() => new Spaceship("Name One", -1));
        }


        [Test]
        public void AddMethod_WithoutCapacity_Should_ThrowExcepiton()
        {
            var spaceship = new Spaceship("Name One", 1);
            spaceship.Add(new Astronaut("One", 10));
            
            Assert.Throws<InvalidOperationException>(() => spaceship.Add(new Astronaut("Two", 10)));
        }
        [Test]
        public void AddMethod_AlreadyExistAustorinauth_Should_ThrowExcepiton()
        {
            var spaceship = new Spaceship("Name One", 10);
            spaceship.Add(new Astronaut("One", 10));

            Assert.Throws<InvalidOperationException>(() => spaceship.Add(new Astronaut("One", 10)));
        }

        [Test]
        public void AddMethod_Should_SuxxefullyAdd()
        {
            var spaceship = new Spaceship("Name One", 10);
            spaceship.Add(new Astronaut("One", 10));

            Assert.AreEqual(1, spaceship.Count);
        }
        //Remove - минимум 3 теста

        [Test]
        public void Remoove_WithInvalidName_ReturnFalse()
        {
            var spaceship = new Spaceship("Name One", 10);
            spaceship.Add(new Astronaut("One", 10));

            Assert.False(spaceship.Remove("Name Two"));
        }

        [Test]
        public void Remoove_WithIValidName_ReturnTrue()
        {
            var spaceship = new Spaceship("Name One", 10);
            spaceship.Add(new Astronaut("One", 10));

            Assert.True(spaceship.Remove("One"));
        }

        [Test]
        public void Remoove_WithIValidName_SUccessfullyRemoveAstronauth()
        {
            var spaceship = new Spaceship("Name One", 10);
            spaceship.Add(new Astronaut("One", 10));
            spaceship.Remove("One");
            Assert.AreEqual(0, spaceship.Count);
        }
    }
}