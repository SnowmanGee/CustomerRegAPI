using System;
using System.Collections.Generic;

namespace CustomerRegAPI.Models
{
    public partial class CustomerDetail
    {
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? PolicyReference { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? EmailAddress { get; set; }
    }
}
