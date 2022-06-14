// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using System.Threading.Tasks;

namespace BankManagement.BankQueries
{
    public interface ICurrencyConverter
    {
        Task<double> GetCurrencyValue(Currency from, Currency to);
    }
}