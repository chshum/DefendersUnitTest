// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

namespace BankManagement.Models
{
    public class BankEmployee
    {
        public string EmployeeName { get; set; }
        
        public string EmployeeLastName { get; set; }
        
        public long EmployeeId { get; set; }
        
        public long EmployeeBranchId { get; set; }
        
        public string EmployeePosition { get; set; }
        
        public long EmployeeManagerId { get; set; }
    }
}
