using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.SubscriptionSchema
{
    [Table("CustomerTypes", Schema = "Subscription")]
    public class CustomerType : DynamicLookup
    {
        public CustomerType()
        {
            Customers = new HashSet<Customer>();
        }
        public virtual ICollection<Customer> Customers { get; set; }

    }
}
