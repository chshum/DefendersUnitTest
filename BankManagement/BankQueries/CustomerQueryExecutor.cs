// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using BankManagement.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.BankQueries
{
    public class CustomerQueryExecutor
    {
        public int c_priorityAccountCalueNis = 20000;
        private ICurrencyConverter _converter;

        public CustomerQueryExecutor(ICurrencyConverter currencyConverter)
        {
            _converter = currencyConverter;
        }

        public async Task<IEnumerable<BankCustomer>> GetPriorityCustomers(BankDb bankDb)
        {
            var priorityCustomersTasks = bankDb.BankCustomers
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
