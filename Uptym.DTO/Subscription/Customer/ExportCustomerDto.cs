using System;
using Uptym.DTO.Common;

namespace Uptym.DTO.Subscription.Customer
{
    public class ExportCustomerDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Status { get; set; } // Active, NotActive, Locked

    }
}
