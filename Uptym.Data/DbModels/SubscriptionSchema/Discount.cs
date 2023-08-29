using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.SubscriptionSchema
{
    [Table("Discounts", Schema = "Subscription")]
    public class Discount : StaticLookup
    {
        public Discount()
        {
            Billings = new HashSet<Billing>();
        }
        public int PlanID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public decimal DiscountAmount { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual ICollection<Billing> Billings { get; set; }

    }
}
