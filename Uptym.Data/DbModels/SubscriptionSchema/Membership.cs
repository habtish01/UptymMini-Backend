using System;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.SubscriptionSchema
{
    [Table("Memberships", Schema = "Subscription")]
    public class Membership: BaseEntity
    {
        public int PlanId { get; set; }
        public int? BillingId { get; set; }
        public int CustomerId{ get; set; }
        public string Status { get; set; }  //Request, Active, NotActive, Locked
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ExtraEndDate { get; set; }
        public bool ReminderStatus { get; set; }
        public DateTime ReminderOn { get; set; }
        public bool AutoActive { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual Billing Billing { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
