using Chainblock.Common;
using Chainblock.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chainblock.Models
{
    public class Transaction : ITransaction
    {
        private int id;
        private string from;
        private string to;
        private double amount;
        private TransactionStatus ts;

        public Transaction(int id, TransactionStatus status, string from, string to, double amount)
        {
            this.Id = id;
            this.Status = status;
            this.From = from;
            this.To = to;
            this.Amount = amount;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidIdMessage);
                }
                this.id = value;
            }
        }
        public TransactionStatus Status 
        {
            get
            {
                return this.ts;
            }
            set
            {
                this.ts = value;
            }
        }
        public string From 
        { 
            get 
            {
                return this.from; 
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidSendarUsernameMessage);
                }
                this.from = value;
            }
        }
        public string To
        {
            get
            {
                return this.to;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidReceiverUsernameMessage);
                }
                this.to = value;
            }
        }
        public double Amount {
            get { return this.amount; }
            set 
            {
                if (value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTransactionAmountMessage);
                }
                this.amount = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ITransaction transaction)
            {
                return this.Id == transaction.Id;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
