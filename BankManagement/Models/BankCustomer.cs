// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------
using System.Collections.Generic;

namespace BankManagement.Models
{
    public class BankCustomer
    {
        public string CustomerId { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public Address Address { get; set; }
        
        public long AssignedBranchId { get; set; }
        
        public List<BankAccount> BankAccounts { get; set; }
    }
}
