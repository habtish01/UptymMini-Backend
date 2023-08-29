using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.SubscriptionSchema
{
    [Table("Billings", Schema = "Subscription")]
    public class Billing : BaseEntity
    {
        public Billing()
        {
            Memberships = new HashSet<Membership>();
        }
        public string UniqueId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentTypeId { get; set; }
        public string CustomerCardNumber { get; set; }
        public string PaymentIntentId { get; set; }
        public string Status { get; set; } // Active, NotActive, Locked
        public string Details { get; set; }
        public DateTime BillingDate { get; set; }
        public decimal Amount { get; set; }
        public string CardLast { get; set; }
        public string LocationIP { get; set; }
        public int? DiscountID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual PaymentType PaymentType { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
