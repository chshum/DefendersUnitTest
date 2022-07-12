// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

using BankManagement.Models;
using System.Collections.Generic;

namespace BankManagement.Storage
{
    public interface IBankDb
    {
        Bank Bank { get; set; }
        List<BankBranch> BankBranches { get; set; }
        List<BankCustomer> BankCustomers { get; set; }
        List<BankCustomer> BankEmployees { get; set; }
    }
}