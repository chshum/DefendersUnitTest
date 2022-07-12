// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankManagement.BankQueries
{
    public interface ICustomerQueryExecutor
    {
        Task<BankAccount> GetBankAccount(int accountNumber);
        Task<IEnumerable<BankCustomer>> GetPriorityCustomers();
    }
}