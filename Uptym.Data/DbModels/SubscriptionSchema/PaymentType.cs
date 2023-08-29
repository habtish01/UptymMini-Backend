using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.SubscriptionSchema
{
    [Table("PaymentTypes", Schema = "Subscription")]
    public class PaymentType : StaticLookup
    {
        public PaymentType()
        {
            Billings = new HashSet<Billing>();
        }
        public string Address { get; set; }
        public string SK { get; set; }
        public string PK { get; set; }

        public virtual ICollection<Billing> Billings { get; set; }

    }
}
