using Chainblock.Common;
using Chainblock.Contracts;
using Chainblock.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chainblock.Tests
{
    [TestFixture]
    public class ChainblockTests
    {
        private IChainblock chainblock;
        private ITransaction testTransaction;

        [SetUp]
        public void Setup()
        {
            this.chainblock = new Core.Chainblock();
            this.testTransaction = new Transaction(1, TransactionStatus.Unauthorized, "Pesho", "Gosho", 10);
        }
        [Test]
        public void TestIfConstructorWorksCorrectly()
        {
            int expectedInitialCount = 0;
            IChainblock chainblock = new Chainblock.Core.Chainblock();

            Assert.AreEqual(expectedInitialCount, chainblock.Count);
        }

        [Test]
        public void AddShouldIncreeseCountWhenOk()
        {
            //Arrange
            int expectedCount = 1;
            ITransaction transaction = new Transaction(1, TransactionStatus.Successfull, "Pesho", "Gosho", 10);

            //Act
            this.chainblock.Add(transaction);

            //Assert

            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }


        [Test]
        public void AddingExistentTransactionShouldThrowException()
        {
            //Arrange
            ITransaction transaction = new Transaction(1, TransactionStatus.Failed, "Pesho", "Gosho", 10);

            //Act
            this.chainblock.Add(transaction);

            //Assert

            Assert.That(() =>
            {
                this.chainblock.Add(transaction);
            }, Throws.InvalidOperationException
            .With.Message
            .EqualTo(ExceptionMessages.AddingExitingTransactionMessage));
        }


        [Test]
        public void AddingSameTransactionWIthAnoutherIdSgouldPass()
        {
            //Arrange
            int expectedCount = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(1, ts, from, to, amount);
            ITransaction transactionCoppy = new Transaction(2, ts, from, to, amount);

            //Act
            this.chainblock.Add(transaction);
            this.chainblock.Add(transactionCoppy);

            //Assert

            Assert.AreEqual(expectedCount, this.chainblock.Count);
        }

        [Test]
        public void ContainseShouldReturnTrueWithExistingTransaction()
        {
            //Arrange
            int expectedCount = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(1, ts, from, to, amount);

            //Act
            this.chainblock.Add(transaction);

            //Assert

            Assert.IsTrue(this.chainblock.Contains(transaction));
        }

        [Test]
        public void ContainseShouldReturnFalseWIthNonExistingTransaction()
        {
            //Arrange
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(1, ts, from, to, amount);

            //Act
            //this.chainblock.Add(transaction);

            //Assert

            Assert.IsFalse(this.chainblock.Contains(transaction));
        }


        [Test]
        public void ContainsByIdShouldRetunrTrueWhenExistingTransaction()
        {
            //Arrange
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            //Act
            //this.chainblock.Add(transaction);

            //Assert

            Assert.IsFalse(this.chainblock.Contains(id));
        }

        [Test]
        public void ContainsByIdShouldRetunrFalseWithNonExistingTransaction()
        {
            //Arrange
            int id = 1;
            TransactionStatus ts = TransactionStatus.Failed;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            //Act
            //this.chainblock.Add(transaction);

            //Assert

            Assert.IsFalse(this.chainblock.Contains(id));
        }

        [Test]
        public void ChangeChangingTransactionStatusOfExsitignTransaction()
        {
            //Arrange
            int id = 1;
            TransactionStatus ts = TransactionStatus.Unauthorized;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;

            TransactionStatus newStatus = TransactionStatus.Successfull;
            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            //Act
            this.chainblock.Add(transaction);
            this.chainblock.ChangeTransactionStatus(id, newStatus);

            //Assert
            Assert.AreEqual(newStatus, transaction.Status);
        }

        [Test]
        public void ChangingStatusOfNonExistingTransactionShouldThrowException()
        {
            //Arrange
            int id = 1;
            TransactionStatus ts = TransactionStatus.Unauthorized;
            string from = "Pesho";
            string to = "Gosho";
            double amount = 10;
            int fakeId = 13;

            TransactionStatus newStatus = TransactionStatus.Successfull;
            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            //Act
            this.chainblock.Add(transaction);
            ;

            //Assert
            Assert.That(() =>
            {
                this.chainblock.ChangeTransactionStatus(fakeId, newStatus);
            }, Throws.ArgumentException.With.Message.EqualTo(ExceptionMessages.ChangingStatusOfNonExistitngTransaction));
        }

        [Test]
        public void GetByIdShouldReturnCorrectTransactio()
        {
            //Arrange
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Reciver";
            double amount = 20;

            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            //Act
            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);

            ITransaction ReturndTransaction = this.chainblock.GetById(id);

            //Assert
            Assert.AreEqual(transaction, ReturndTransaction);
            //AreEqual може да сравнява обекти !
        }

        [Test]
        public void GetByIdShouldShouldThrowExeptionWHenTryingToFindeNonExistingTransaction()
        {
            //Arrange
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Sender";
            string to = "Reciver";
            double amount = 20;

            int fakeId = 13;
            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            //Act
            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);


            //Assert
            Assert.That(() =>
            {
                this.chainblock.GetById(fakeId);
            }, Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages.NonExistingTransactionMessage));
        }

        [Test]
        public void RemoveingTransactionShouldDecreeseCount()
        {
            //Arrange
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Gosho";
            string to = "Pesho";
            double amount = 250;
            int expetedCount = 1;
            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            //Act
            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);
            this.chainblock.RemoveTransactionById(id);

            //Assert
            Assert.AreEqual(expetedCount, this.chainblock.Count);
        }

        [Test]
        public void RemoveingTransactionShouldPhisicalyRemoveTheTransaction()
        {
            //Arrange
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Gosho";
            string to = "Pesho";
            double amount = 250;
            int expetedCount = 1;
            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            //Act
            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);
            this.chainblock.RemoveTransactionById(id);

            //Assert
            Assert.That(() =>
            {
                this.chainblock.GetById(id);
            }, Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages.NonExistingTransactionMessage));
        }

        [Test]
        public void RemoveingNonExistingTransactionShouldThorwException()
        {
            //Arrange
            int id = 2;
            TransactionStatus ts = TransactionStatus.Successfull;
            string from = "Gosho";
            string to = "Pesho";
            double amount = 250;
            int fakeId = 13;
            ITransaction transaction = new Transaction(id, ts, from, to, amount);

            //Act
            this.chainblock.Add(this.testTransaction);
            this.chainblock.Add(transaction);

            //Assert
            Assert.That(() =>
            {
                this.chainblock.RemoveTransactionById(fakeId);
            }, Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages.RemovingNonExistingTransactionMessage));
        }

        [Test]
        public void GetByTransactionStatusShouldReturnOrderedCollectionWithRightTransactions()
        {
            //Arrange
            ICollection<ITransaction> expTransaction = new List<ITransaction>();
            for (int i = 0; i < 4; i++)
            {
                int id = 1 + i;
                TransactionStatus ts = (TransactionStatus)i;
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                if (ts == TransactionStatus.Successfull)
                {
                    expTransaction.Add(currentTransaction);
                }
                this.chainblock.Add(currentTransaction);
            }

            ITransaction seccTr = new Transaction(5, TransactionStatus.Successfull, "Pesho4", "Gosho4", 15);

            //Act
            expTransaction.Add(seccTr);
            this.chainblock.Add(seccTr);
            IEnumerable<ITransaction> actTransaction = this.chainblock.GetByTransactionStatus(TransactionStatus.Successfull);

            expTransaction = expTransaction.OrderByDescending(tx => tx.Amount).ToList();

            //Assert
            CollectionAssert.AreEqual(expTransaction, actTransaction);
        }

        [Test]
        public void GettingTransactionWIthNoExistingStatus()
        {
            //Arrange
            //ICollection<ITransaction> expTransaction = new List<ITransaction>();
            for (int i = 0; i < 10; i++)
            {
                int id = 1 + i;
                TransactionStatus ts = TransactionStatus.Failed;
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            //

            //Assert & Act
            Assert.That(() =>
            {
                this.chainblock.GetByTransactionStatus(TransactionStatus.Successfull);
            }, Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages.EmtryStatusTransactionCollection));
        }

        [Test]
        public void AllsendersByStatusShouldReturnCorrectOrderedCollection()
        {
            //Arrange
            ICollection<ITransaction> expTransactions = new List<ITransaction>();
            for (int i = 0; i < 10; i++)
            {
                int id = 1 + i;
                TransactionStatus ts = (TransactionStatus)i;
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                if (ts == TransactionStatus.Successfull)
                {
                    expTransactions.Add(currentTransaction);
                }
                this.chainblock.Add(currentTransaction);
            }

            ITransaction seccTr = new Transaction(20, TransactionStatus.Successfull, "Pesho4", "Gosho4", 15);

            //Act
            expTransactions.Add(seccTr);
            this.chainblock.Add(seccTr);
            IEnumerable<string> actTransaction = this.chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Successfull);

            IEnumerable<string> expectedTransactionOut = expTransactions
                 .OrderByDescending(tx => tx.Amount)
                 .Select(tx => tx.From);

            //Assert
            CollectionAssert.AreEqual(expectedTransactionOut, actTransaction);
        }

        [Test]
        public void AllSendersByStatusSHouldThrowExceptionWhenThereAreNoTransactionsWIthGivenStatus()
        {
            //Arrange
            //ICollection<ITransaction> expTransaction = new List<ITransaction>();
            for (int i = 0; i < 10; i++)
            {
                int id = 1 + i;
                TransactionStatus ts = TransactionStatus.Failed;
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            //Assert & Act
            Assert.That(() =>
            {
                this.chainblock.GetAllSendersWithTransactionStatus(TransactionStatus.Successfull);
            }, Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages.EmtryStatusTransactionCollection));
        }

        [Test]
        public void AllReceiversByStatusShouldReturnCorrectOrderedCollection()
        {
            //Arrange
            ICollection<ITransaction> expTransactions = new List<ITransaction>();
            for (int i = 0; i < 10; i++)
            {
                int id = 1 + i;
                TransactionStatus ts = (TransactionStatus)i;
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                if (ts == TransactionStatus.Successfull)
                {
                    expTransactions.Add(currentTransaction);
                }
                this.chainblock.Add(currentTransaction);
            }

            ITransaction seccTr = new Transaction(20, TransactionStatus.Successfull, "Pesho4", "Gosho4", 15);

            //Act
            expTransactions.Add(seccTr);
            this.chainblock.Add(seccTr);
            IEnumerable<string> actTransaction = this.chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Successfull);

            IEnumerable<string> expectedTransactionOut = expTransactions
                 .OrderByDescending(tx => tx.Amount)
                 .Select(tx => tx.To);

            //Assert
            CollectionAssert.AreEqual(expectedTransactionOut, actTransaction);
        }

        [Test]
        public void AllReciversByStatusSHouldThrowExceptionWhenThereAreNoTransactionsWIthGivenStatus()
        {
            //Arrange
            //ICollection<ITransaction> expTransaction = new List<ITransaction>();
            for (int i = 0; i < 10; i++)
            {
                int id = 1 + i;
                TransactionStatus ts = TransactionStatus.Failed;
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            //Assert & Act
            Assert.That(() =>
            {
                this.chainblock.GetAllReceiversWithTransactionStatus(TransactionStatus.Successfull);
            }, Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages.EmtryStatusTransactionCollection));
        }

        [Test]
        public void TestGetAllOrderedByAmountTheByIdWithNoDUplicatedAmounth()
        {
            //Arrange
            ICollection<ITransaction> expTransactions = new List<ITransaction>();
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = (TransactionStatus)(i% 4);
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
                expTransactions.Add(currentTransaction);
            }

            IEnumerable<ITransaction> actTransaction = this.chainblock.GetAllOrderedByAmountDescendingThenById();

            expTransactions = expTransactions.OrderByDescending(tx => tx.Amount)
                 .ThenBy(tx => tx.Id)
                 .ToList();

            //Assert
            CollectionAssert.AreEqual(expTransactions, actTransaction);
        }

        [Test]
        public void TestGetAllOrderedByAmountTheByIdWIthDuplicatedAmounth()
        {
            //Arrange
            ICollection<ITransaction> expTransactions = new List<ITransaction>();
            for (int i = 0; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = (TransactionStatus)(i % 4);
                string from = "Gosho" + i;
                string to = "Pesho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
                expTransactions.Add(currentTransaction);
            }

            ITransaction transaction = new Transaction(11, TransactionStatus.Successfull, "Gosho11", "Pesho11", 10);
            this.chainblock.Add(transaction);
            expTransactions.Add(transaction);

            IEnumerable<ITransaction> actTransaction = this.chainblock.GetAllOrderedByAmountDescendingThenById();

            expTransactions = expTransactions.OrderByDescending(tx => tx.Amount)
                 .ThenBy(tx => tx.Id)
                 .ToList();

            //Assert
            CollectionAssert.AreEqual(expTransactions, actTransaction);
        }

        [Test]
        public void TestGetAllOrderedByAmountTheByIdWithEmptyCollection()
        {
            //Arrange
            IEnumerable<ITransaction> actTransaction = this.chainblock.GetAllOrderedByAmountDescendingThenById();

            //Assert
            CollectionAssert.IsEmpty(actTransaction);
        }

        [Test]
        public void GetAllBySendersDescendingByAmouthShouldReturnWorksCorrectly()
        {
            //Arrange
            ICollection<ITransaction> expTransactions = new List<ITransaction>();
            string wantedSander = "Pesho";
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = wantedSander;
                string to = "Gosho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
                expTransactions.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + 1;
                string to = "Gosho" + i;
                double amount = 20 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            IEnumerable<ITransaction> actTransaction = this.chainblock.GetBySenderOrderedByAmountDescending(wantedSander);

            expTransactions = expTransactions.OrderByDescending(tx => tx.Amount)
                 .ToList();

            //Assert
            CollectionAssert.AreEqual(expTransactions, actTransaction);
        }

        [Test]
        public void GetAllByNonExistingSenderDescendingByAmounth()
        {
            //Arrange
            //ICollection<ITransaction> expTransactions = new List<ITransaction>();
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
                //expTransactions.Add(currentTransaction);
            }

            string wantedSander = "Pesho";

            //Assert
            Assert.That(() =>
            {
                this.chainblock.GetBySenderOrderedByAmountDescending(wantedSander);
            }, Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages.NoTransactionForGivenSender));
        }

        [Test]
        public void GetByReceiverWOrksCorrectlyWIthoudDubplicatedAmounthWorksCorrectWithoudDuplicatedAmounth()
        {
            //Arrange
            ICollection<ITransaction> expTransactions = new List<ITransaction>();
            string wantedReciver = "Gosho";
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to =wantedReciver;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                expTransactions.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }

            IEnumerable<ITransaction> actTransaction = this.chainblock.GetByReceiverOrderedByAmountThenById(wantedReciver);

            expTransactions = expTransactions.OrderByDescending(tx => tx.Amount).ThenBy(tx => tx.Id).ToList();

            //Assert
            CollectionAssert.AreEqual(expTransactions, actTransaction);
        }

        [Test]
        public void GetByReceiverWOrksCorrectlyWIthoudDubplicatedAmounthWorksCorrectWithDuplicatedAmounth()
        {
            //Arrange
            ICollection<ITransaction> expTransactions = new List<ITransaction>();
            string wantedReciver = "Gosho";
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = wantedReciver;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                expTransactions.Add(currentTransaction);
            }

            for (int i = 4; i < 10; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 20 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
            }
            ITransaction transaction = new Transaction(11, TransactionStatus.Successfull, "Pesho11", wantedReciver, 10);
            IEnumerable<ITransaction> actTransaction = this.chainblock.GetByReceiverOrderedByAmountThenById(wantedReciver);

            expTransactions = expTransactions.OrderByDescending(tx => tx.Amount).ThenBy(tx => tx.Id).ToList();

            //Assert
            CollectionAssert.AreEqual(expTransactions, actTransaction);
        }

        [Test]
        public void GetByReceiverDescendignThenByAmouthThenByIdShouldThrowExceptinWhenNoTransaction()
        {
            //Arrange
            //ICollection<ITransaction> expTransactions = new List<ITransaction>();
            for (int i = 0; i < 4; i++)
            {
                int id = i + 1;
                TransactionStatus ts = TransactionStatus.Successfull;
                string from = "Pesho" + i;
                string to = "Gosho" + i;
                double amount = 10 + i;
                ITransaction currentTransaction = new Transaction(id, ts, from, to, amount);

                this.chainblock.Add(currentTransaction);
                //expTransactions.Add(currentTransaction);
            }

            string wantedReceiver = "Gosho";

            //Assert
            Assert.That(() =>
            {
                this.chainblock.GetByReceiverOrderedByAmountThenById(wantedReceiver);
            }, Throws.InvalidOperationException.With.Message.EqualTo(ExceptionMessages.NoTransactionForGivenReciver));
        }
        //Arrange

        //Act

        //Assert
    }
}
