// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using System.Threading.Tasks;

namespace BankManagement.Transactions
{
    public class Withdraw
    {
        public Task Transact(BankAccount account, long amount)
        {
            account.Balance -= amount;
            return Task.CompletedTask;
        }
    }
}
