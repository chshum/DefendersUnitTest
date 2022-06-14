// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

namespace BankManagement.Models
{
    public class BankAccount
    {
        public long AccountNumber { get; set; }
        public AccountType AccountType { get; set; }
        
        public Currency Currency { get; set; }
        
        public double Balance { get; set; }
    }

    public enum Currency
    {
        NIS,
        USD
    }

    public enum AccountType
    {
        Individual,
        Business,
        Corporation
    }
}
