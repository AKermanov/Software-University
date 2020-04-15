using System;
using System.Collections.Generic;
using System.Text;

namespace Chainblock.Common
{
    public static class ExceptionMessages
    {
        public static string InvalidIdMessage = "IDs cannot be zero or negative.";
        public static string InvalidSendarUsernameMessage = "Sendar name ccanot be empty or witespace.";
        public static string InvalidReceiverUsernameMessage = "Receiver name ccanot be empty or witespace.";
        public static string InvalidTransactionAmountMessage = "Transaction amount should be greater than zero.";
       
        
        public static string AddingExitingTransactionMessage = "Transaction already exist.";
        public static string ChangingStatusOfNonExistitngTransaction = "You can't change status of none exist transaction.";
        public static string NonExistingTransactionMessage = "Transaction with given ID not found.";
        public static string RemovingNonExistingTransactionMessage = "Cannot remove none exsting transaction.";
        public static string EmtryStatusTransactionCollection = "There are no transaction maching providet transaction status.";
        public static string NoTransactionForGivenReciver = "There are no coresponding transaction for given reciver name.";
        public static string NoTransactionForGivenSender = "There are no coresponding transaction for given sender name.";
    }
}
