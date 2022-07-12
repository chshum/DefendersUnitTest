// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.BankQueries;
using BankManagement.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.Transactions
{
    public class BankClientTransactionManager
    {
        private List<IBankTransaction> _transactions;
        private IBankDb _bankDb;
        private readonly ICustomerQueryExecutor _queryExecutor;

        public BankClientTransactionManager(
            IBankDb BankDb,
            ICustomerQueryExecutor customerQueryExecutor,
            List<IBankTransaction> transactions)
        {
            _transactions = transactions;
            _bankDb = BankDb;
            _queryExecutor = customerQueryExecutor;
        }

        public async Task<bool> performAction(int accountId, BankTransactionType type, double amount)
        {
            var bankAccount = await _queryExecutor.GetBankAccount(accountId);
            if (bankAccount == null)
            {
                return false;
            }

            var transaction = _transactions.FirstOrDefault(x => x.GetName() == type);
            await transaction.Transact(bankAccount, amount);

            return true;
        }
    }
}
