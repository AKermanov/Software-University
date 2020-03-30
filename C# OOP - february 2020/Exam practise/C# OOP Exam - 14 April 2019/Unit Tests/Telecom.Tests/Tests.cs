namespace Telecom.Tests
{
    using NUnit.Framework;
    using System;

    public class Tests
    {
        [Test]
        public void Count_Should_Return_Correct_Result()
        {
            var phone = new Phone("Vodaphone", "MP-41");
            Assert.AreEqual(0, phone.Count);
        }
        [Test]
        public void Make_Should_Return_Correct_Result()
        {
            var phone = new Phone("Vodaphone", "MP-41");
            Assert.AreEqual("Vodaphone", phone.Make);
        }
        [Test]
        public void Make_Should_Throw_Execpiton_Correct_Result()
        {
            Assert.Throws<ArgumentException>(() => new Phone(null, "MP-41"));
        }

        [Test]
        public void Model_Should_Return_Correct_Result()
        {
            var phone = new Phone("Vodaphone", "MP-41");
            Assert.AreEqual("MP-41", phone.Model);
        }
        [Test]
        public void Model_Should_Throw_Execpiton_Correct_Result()
        {

            Assert.Throws<ArgumentException>(() => new Phone("Vodaphone", null));
        }

        [Test]
        public void AddMethod_Exist_Name_phoneNumber_Thors_Exception()
        {
            var phone = new Phone("Vodaphone", "MP-41");
            phone.AddContact("Ivan", "MG42");
            Assert.Throws<InvalidOperationException>(() => phone.AddContact("Ivan", "MG42"));
        }
        [Test]
        public void AddMethod_Correct_Add_Name_phoneNumber_Thors_Exception()
        {
            var phone = new Phone("Vodaphone", "MP-41");
            phone.AddContact("Ivan", "MG42");
            Assert.AreEqual(1, phone.Count);
        }

        [Test]
        public void CallMehod_Thors_Exception_When_Name_Does_Not_Exist()
        {
            var phone = new Phone("Vodaphone", "MP-41");
            phone.AddContact("Ivan", "MG42");
            Assert.Throws<InvalidOperationException>(() => phone.Call("Stalin"));
        }


        [Test]
        public void CallMehod_Works()
        {
            var phone = new Phone("Vodaphone", "MP-41");
            phone.AddContact("Ivan", "MG42");
          
            var actualResult = phone.Call("Ivan"); ;
            var expectedResult = "Calling Ivan - MG42...";

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}