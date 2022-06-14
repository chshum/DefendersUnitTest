// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using System.Collections.Generic;

namespace BankManagement.Storage
{
    public class BankDb
    {
        public Bank Bank { get; set; }

        public List<BankBranch> BankBranches { get; set; }
        
        public List<BankCustomer> BankEmployees { get; set; }

        public List<BankCustomer> BankCustomers { get; set; }
    }
}
