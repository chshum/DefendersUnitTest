// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using System.Threading.Tasks;

namespace BankManagement.Utils
{
    public interface ICurrencyConverter
    {
        Task<double> GetCurrencyValue(Currency from, Currency to);
    }
}