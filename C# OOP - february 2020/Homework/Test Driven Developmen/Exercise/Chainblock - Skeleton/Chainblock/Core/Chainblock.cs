using Chainblock.Common;
using Chainblock.Contracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chainblock.Core
{//1:52:07
    //
    public class Chainblock : IChainblock
    {
        private ICollection<ITransaction> transactions;
        public Chainblock()
        {
            this.transactions = new List<ITransaction>();
        }
        public int Count => this.transactions.Count;

        public void Add(ITransaction tx)
        {
            if (this.Contains(tx))
            {
                throw new InvalidOperationException(ExceptionMessages.AddingExitingTransactionMessage);
            }
            this.transactions.Add(tx);
        }

        public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
        {
            ITransaction transaction = this.transactions.FirstOrDefault(tx => tx.Id == id);

            if (transaction == null)
            {
                throw new ArgumentException(ExceptionMessages.ChangingStatusOfNonExistitngTransaction);
            }

            transaction.Status = newStatus;
        }

        public bool Contains(ITransaction tx)
        {
            return this.Contains(tx.Id); // методите са overload но вършат едно и също нещо,
                                         // просто подаваме параметри на другия метод
        }

        public bool Contains(int id)
        {
            bool isContained = this.transactions.Any(tx => tx.Id == id);
            return isContained;
        }

        public IEnumerable<ITransaction> GetAllInAmountRange(double lo, double hi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITransaction> GetAllOrderedByAmountDescendingThenById()
        {
            IEnumerable<ITransaction> transactions = this.transactions
                .OrderByDescending(tx => tx.Amount)
                .ThenBy(tx => tx.Id);

            return transactions;
        }

        public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
        {
            IEnumerable<string> recivers = this.GetByTransactionStatus(status) //преизползваме метод, който знаем че работи
                .Select(tx => tx.To);

            return recivers;
        }

        public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
        {
            IEnumerable<string> senders = this.GetByTransactionStatus(status) //преизползваме метод, който знаем че работи
                .Select(tx => tx.From);

            return senders;
        }

        public ITransaction GetById(int id)
        {
            ITransaction transaction = this.transactions.FirstOrDefault(tx => tx.Id == id);
            if (transaction == null)
            {
                throw new InvalidOperationException(ExceptionMessages.NonExistingTransactionMessage);
            }

            return transaction;
        }

        public IEnumerable<ITransaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITransaction> GetByReceiverOrderedByAmountThenById(string receiver)
        {
            IEnumerable<ITransaction> transactions = this.transactions
                 .Where(tx => tx.To == receiver)
                 .OrderByDescending(tx => tx.Amount)
                 .ThenBy(tx => tx.Id);

            if (transactions.Count() == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.NoTransactionForGivenReciver);
            }

            return transactions;
        }

        public IEnumerable<ITransaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITransaction> GetBySenderOrderedByAmountDescending(string sender)
        {
            IEnumerable<ITransaction> senders = this.transactions
                .Where(tx => tx.From == sender)
                .OrderByDescending(tx => tx.Amount);

            if (senders.Count()== 0)
            {
                throw new InvalidOperationException(ExceptionMessages.NoTransactionForGivenSender);
            }

            return senders;
        }

        public IEnumerable<ITransaction> GetByTransactionStatus(TransactionStatus status)
        {
            IEnumerable<ITransaction> ordTransactions = this.transactions.Where(tx => tx.Status == status)
                .OrderByDescending(tx => tx.Amount);

            if (ordTransactions.Count()==0)
            {
                throw new InvalidOperationException(ExceptionMessages.EmtryStatusTransactionCollection);
            }

            return ordTransactions;
        }

        public IEnumerable<ITransaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ITransaction> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void RemoveTransactionById(int id)
        {
            try
            {
                ITransaction transaction = this.GetById(id);
                this.transactions.Remove(transaction);
            }
            catch (InvalidOperationException)
            {
                throw new InvalidOperationException(ExceptionMessages.RemovingNonExistingTransactionMessage);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
