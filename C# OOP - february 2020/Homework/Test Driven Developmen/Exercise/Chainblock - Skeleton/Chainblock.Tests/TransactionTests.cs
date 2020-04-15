using Chainblock.Common;
using Chainblock.Contracts;
using Chainblock.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chainblock.Tests
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public void TestCOnstructorWorksCorrectly()
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;
            ITransaction transaction = new Transaction(id,ts,from,to,amount);

            Assert.AreEqual(id, transaction.Id); //Тестваме един и същи юнит, стига да са логически обвързани.
            Assert.AreEqual(ts, transaction.Status);
            Assert.AreEqual(from, transaction.From);
            Assert.AreEqual(to, transaction.To);
            Assert.AreEqual(amount, transaction.Amount);
        }

        [Test]
        [TestCase(-10)]
        [TestCase(0)]
        public void TestWithInvalidId(int id)
        {
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 15;

            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            },Throws.ArgumentException.With.Message.EqualTo(ExceptionMessages.InvalidIdMessage));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void ThestWithLikeInvalidSenderName(string from)
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string to = "Gosho";
            double amount = 15;

            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.EqualTo(ExceptionMessages.InvalidSendarUsernameMessage));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("   ")]
        public void TestWithInvalidReceiverName(string to)
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            double amount = 15;

            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.EqualTo(ExceptionMessages.InvalidReceiverUsernameMessage));
        }

        [Test]
        [TestCase(null)]
        [TestCase(-5.0)]
        [TestCase(-0.0000000001)]
        [TestCase(0.0)]
        public void TestWithLikeInvalidAmount(double amount)
        {
            int id = 1;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";

            Assert.That(() =>
            {
                ITransaction transaction = new Transaction(id, ts, from, to, amount);
            }, Throws.ArgumentException.With.Message.EqualTo(ExceptionMessages.InvalidTransactionAmountMessage));
        }


    }
}
