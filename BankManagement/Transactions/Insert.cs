using BankManagement.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BankManagement.Transactions
{
    public class Insert : IBankTransaction
    {
        public Task Transact(BankAccount account, long amount)
        {
            account.Balance += amount;
            return Task.CompletedTask;
        }
    }
}
