using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.SecuritySchema;

namespace Uptym.Data.DbModels.SubscriptionSchema
{
    [Table("Customers", Schema = "Subscription")]
    public class Customer : BaseEntity
    {
        public Customer()
        {
            Users = new HashSet<ApplicationUser>();
            Billings = new HashSet<Billing>();
            Membership = new HashSet<Membership>();
            UpcomingPayments = new HashSet<UpcomingPayment>();
        }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public Location Location { get; set; }
        public string PhoneNumber { get; set; }
        public string PersonalImagePath { get; set; }
        public string WorkHistory { get; set; }
        public string Organization { get; set; }
        public string Status { get; set; } // Active, NotActive, Locked
        public bool IsTrial { get; set; }   //Trial perid comes from Admin Config
        public int ReminderDays { get; set; }
        public int PlanId { get; set; }
        public int? CustomerTypeId { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
        public ICollection<Billing> Billings { get; set; }
        public ICollection<Membership> Membership { get; set; }
        public ICollection<UpcomingPayment> UpcomingPayments { get; set; }

    }


}
