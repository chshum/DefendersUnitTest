// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using BankManagement.Storage;
using BankManagement.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.BankQueries
{
    public class CustomerQueryExecutor : ICustomerQueryExecutor
    {
        public int c_priorityAccountCalueNis = 20000;
        private ICurrencyConverter _converter;
        private readonly IBankDb _bankDb;

        public CustomerQueryExecutor(ICurrencyConverter currencyConverter, IBankDb bankDb)
        {
            _converter = currencyConverter;
            _bankDb = bankDb;
        }

        public Task<BankAccount> GetBankAccount(int accountNumber)
        {
            var bankAccount = _bankDb.BankCustomers
                        .SelectMany(x => x.BankAccounts)
                        .FirstOrDefault(x => x.AccountNumber == accountNumber);

            return Task.FromResult(bankAccount);
        }

        public async Task<IEnumerable<BankCustomer>> GetPriorityCustomers()
        {
            var priorityCustomersTasks = _bankDb.BankCustomers
                .Select(async customer =>
                {
                    var isPriority = await IsPriorityCustomer(customer);
                    return new
                    {
                        Customer = customer,
                        IsPriority = isPriority
                    };
                });
            var priorityCustomers = await Task.WhenAll(priorityCustomersTasks);
            return priorityCustomers.Where(x => x.IsPriority).Select(x => x.Customer);
        }

        private async Task<bool> IsPriorityCustomer(BankCustomer customer)
        {
            var priorityAccounts = await Task.WhenAll(customer.BankAccounts.Select(async account => await IsPriorityAccount(account)));
            return priorityAccounts.Any(x => x);
        }

        private async Task<bool> IsPriorityAccount(BankAccount account)
        {
            return await GetValueNis(account) > c_priorityAccountCalueNis;
        }

        private async Task<double> GetValueNis(BankAccount account)
        {
            return await _converter.GetCurrencyValue(account.Currency, Currency.NIS) * account.Balance;
        }
    }
}
