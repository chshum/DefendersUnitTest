// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using BankManagement.Storage;
using System;
using System.Linq;

namespace BankManagement
{
    /// <summary>
    /// One option to test code is to run the actual program. however, the more the programs grows, the more complicated it will be to simulate a specific scenario, 
    /// and it will not be possible to reposudce in sterile environments in a consistent manner.
    /// its also much harder to test large numbers of cases.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var bank = new Bank()
            {
                BankName = "unitTestBank",
                InternationalBankCode = 1111,
                LocalBankCode = 10
            };

            var bankDb = new BankDb();
            var bankBranches = Enumerable.Range(0, 10)
                .Select(i => new BankBranch() 
                { 
                    BranchAddress = CreateAddress(i), 
                    BranchId = i, 
                    BranchName = $"Branch_{i}",
                    BankId = bank.LocalBankCode                    
                });       
        }

        private static Address CreateAddress(int i)
        {
            return new Address
            {
                Country = "Israel",
                City = "Herzlia",
                HouseNumber = $"i"
            };
        }
    }
}
