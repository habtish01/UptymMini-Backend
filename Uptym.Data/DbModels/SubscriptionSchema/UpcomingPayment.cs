using System;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.SubscriptionSchema
{
    [Table("UpcomingPayments", Schema = "Subscription")]
    public class UpcomingPayment : BaseEntity
    {
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TargetDate { get; set; }
        public string Details { get; set; }
        public string EmailReminderStatus { get; set; } //Sent, NotSent
        public string PhoneReminderStatus { get; set; }  //Sent, NotSent
        public string Status { get; set; } // Active, NotActive, Locked
        public virtual Customer Customer { get; set; }
    }
}
