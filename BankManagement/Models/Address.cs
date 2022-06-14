// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved. 
// ----------------------------------------------------------------------------

namespace BankManagement.Models
{
    public class Address
    {
        public string Country { get; set; }
        
        public string City { get; set; }
        
        public string StreetName { get; set; }
        
        public string HouseNumber { get; set; }
        
        public string ApartmentNumber { get; set; }

        public string ZipCode { get; set; }
    }
}