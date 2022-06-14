// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

namespace BankManagement.Models
{
    public class BankBranch
    {
        public string BranchName { get; set; }
        public long BranchId { get; set; }
        public long BankId { get; set; }
        public Address BranchAddress { get; set; }
    }
}
