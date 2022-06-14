// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.BankQueries
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private Dictionary<Currency, double> CurrencyMappingsToNis = new Dictionary<Currency, double>
        {
            { Currency.NIS, 1 },
            { Currency.USD, 3.5 }
        };

        private Dictionary<Currency, double> CurrencyMappingsFromNis = new Dictionary<Currency, double>();

        public CurrencyConverter()
        {
            CurrencyMappingsToNis = new Dictionary<Currency, double>
            {
                { Currency.NIS, 1 },
                { Currency.USD, 3.5 }
            };

            CurrencyMappingsFromNis = CurrencyMappingsToNis.ToDictionary(key => key.Key, kvp => 1 / kvp.Value);
        }

        /// <summary>
        /// Gets the currency value.
        /// </summary>
        public async Task<double> GetCurrencyValue(Currency from, Currency to)
        {
            //Simulate querying a remote endpoint for currency values: 
            await Task.Delay(500);

            var inNis = CurrencyMappingsToNis[from];
            var inTo = CurrencyMappingsFromNis[to];

            return inNis * inTo;
        }

    }
}
