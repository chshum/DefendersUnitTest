using System;
using System.Collections.Generic;
using System.Text;

namespace BankManagement.Models
{
    public class BankCustomer
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public Address Address { get; set; }
        
        public long AssignedBranchId { get; set; }
        
        public List<BankAccount> BankAccounts { get; set; }
    }
}
