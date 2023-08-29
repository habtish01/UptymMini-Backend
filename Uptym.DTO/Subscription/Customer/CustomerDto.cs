using System;
using Uptym.DTO.Common;

namespace Uptym.DTO.Subscription.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string PersonalImagePath { get; set; }
        public string WorkHistory { get; set; }
        public string Organization { get; set; }
        public string Status { get; set; } // Active, NotActive(Membership is not approved), Locked
        public bool IsTrial { get; set; }   //Trial perid comes from Admin Config
        public int ReminderDays { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int? PlanId { get; set; }
        public int? CustomerTypeId { get; set; }
        public LocationDto LocationDto { get; set; }

        // UI
        public string PlanName { get; set; }
        public bool ReomveProfileImage { get; set; }
    }

    public class CustomerMembershipDto
    {
        public int PlanId { get; set; }
        public int? BillingId { get; set; }
        public int CustomerId { get; set; }
        public string Status { get; set; }  //Request, Active, NotActive, Locked
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool AutoActive { get; set; }

        // UI
        public string PlanName { get; set; }
        public string CustomerName { get; set; }

    }

    public class CustomerDrp : DropdownDrp
    {

    }
}
