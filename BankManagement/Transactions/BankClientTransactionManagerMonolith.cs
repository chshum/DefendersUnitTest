// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.BankQueries;
using BankManagement.Models;
using BankManagement.Storage;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.Transactions
{
    /// <summary>
    /// An example of how Not to create a class
    /// </summary>
    public class BankClientTransactionManagerMonolith
    {
        public BankClientTransactionManagerMonolith()
        {
        }

        public async Task<bool> PerformAction(int accountId, BankTransactionType type, double amount)
        {
            // We are mixing multiple responsibilities here. the class: 
            // - gets resources
            // - performs queries
            // - performs transactions
            // this makese it non-SOLID and harder to test. the more the code is complicated, the more test scnarios we have - cartesion multiplying of all cases, 
            // and when tests fail, it could be for many reasons outside the actual test. much harder to isolate logic.
            var bankAccount = await GetBankAccount(accountId);
            if (bankAccount == null)
            {
                return false;
            }

            var transaction = CreateRealTransaction(type);
            await transaction.Transact(bankAccount, amount);

            return true;
        }

        // Managing the creation of resources is also an added responsibility, does not allow for mocking dependencies and adds another fail point in the code.
        private IBankTransaction CreateRealTransaction(BankTransactionType type)
        {
            return null;
        }

        private Task<IBankDb> GetRealBankDB()
        {
            IBankDb bankDb = null;
            return Task.FromResult(bankDb);
        }


        public async Task<BankAccount> GetBankAccount(int accountNumber)
        {
            // Getting concrete resources within the code limits us and does not allow for mocking external dependencies/
            var bankDb = await GetRealBankDB();
            var bankAccount = bankDb.BankCustomers
                        .SelectMany(x => x.BankAccounts)
                        .FirstOrDefault(x => x.AccountNumber == accountNumber);

            return bankAccount;
        }

    }
}
