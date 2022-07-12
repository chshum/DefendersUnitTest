// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using System.Threading.Tasks;

namespace BankManagement.Transactions
{
    public class WithdrawBankTransaction : IBankTransaction
    {
        public BankTransactionType GetName() => BankTransactionType.Withdraw;

        public Task Transact(BankAccount account, double amount)
        {
            account.Balance -= amount;
            return Task.CompletedTask;
        }
    }
}
