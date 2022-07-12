// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using System.Threading.Tasks;

namespace BankManagement.Transactions
{
    public class InsertBankTransaction : IBankTransaction
    {
        public BankTransactionType GetName() => BankTransactionType.Insert;

        public Task Transact(BankAccount account, double amount)
        {
            account.Balance += amount;
            return Task.CompletedTask;
        }
    }
}
