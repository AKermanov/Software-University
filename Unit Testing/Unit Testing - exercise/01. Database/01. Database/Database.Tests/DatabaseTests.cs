using NUnit.Framework;

namespace Tests
{
   using Database;
    using System;

    public class DatabaseTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConstructorShouldBeInitializeWith16Element()
        {
            int[] arr = new int[16];
            Database dataNew = new Database(arr);

            int expetedCount = 16;
            int actualCount = dataNew.Count;

            Assert.AreEqual(expetedCount, actualCount);
        }


        [Test]
        public void ConstructorShouldThrowInvalidOpeationException()
        {
            int[] arr = new int[17];
            Assert.Throws<InvalidOperationException>(() => new Database(arr));
        }
        [Test]
        public void AddMethodShoudlAddCOrrectly()
        {
            Database dataNew = new Database();

            dataNew.Add(1);
            int expetedCount = 1;
            int actualCount = dataNew.Count;

            Assert.AreEqual(expetedCount, actualCount);
        }
        [Test]
        public void AddMethodShouldAddOnTheNExtEmptyIndex()
        {
            int[] arr = { 1,2,3,4,5};
            Database dataNew = new Database(arr);
            dataNew.Add(6);

            int expectedResult = 6;
            int actrualResult = dataNew.Fetch()[5];

            Assert.AreEqual(expectedResult, actrualResult);

        }
        [Test]
        public void AddMethodShouldThyrowInvalidOperationException()
        {
            int[] arr = new int[16];
            Database dataNew = new Database(arr);

            Assert.Throws<InvalidOperationException>(() => dataNew.Add(1));
        }
        [Test]
        public void ShouldRemoveCOrrextrlyLast()
        {
            int[] arr = new int[3];
            Database dataNew = new Database(arr);
            dataNew.Remove();

            int expectedCount = 2;
            int actualCount = dataNew.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void ShouldRemoveCOrrextrlyLastElement()
        {
            int[] arr = new int[3];
            Database dataNew = new Database(arr);
            dataNew.Remove();

            int expectedCount = 0;
            int actualCount = dataNew.Fetch()[dataNew.Count -1];

            Assert.AreEqual(expectedCount, actualCount);
         
        }

        [Test]
        public void RemoveShouldThrowInvalidOperationException()
        {
            Database dataNew = new Database();
            Assert.Throws<InvalidOperationException>(() => dataNew.Remove());
        }
        [Test]
        public void FechMethodShouldReturnAllElementsAsArray()
        {
            int[] array = { 1, 2, 3, 4, 5 };
            Database dataNew = new Database(array);
         
            int[] expectedValues = { 1, 2, 3, 4, 5 };
            int[] actualValue = dataNew.Fetch();

            CollectionAssert.AreEqual(expectedValues, actualValue);
        }
    }
}