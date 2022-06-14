// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using System.Threading.Tasks;

namespace BankManagement.Transactions
{
    public interface IBankTransaction
    {
        Task Transact(BankAccount Account, long amount);
    }
}