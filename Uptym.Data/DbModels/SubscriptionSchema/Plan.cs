using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.ConfigurationSchema;

namespace Uptym.Data.DbModels.SubscriptionSchema
{
    [Table("Plans", Schema = "Subscription")]
    public class Plan : DynamicLookup
    {
        public Plan()
        {
            Customers = new HashSet<Customer>();
            Memberships = new HashSet<Membership>();
            Discounts = new HashSet<Discount>();
            PlanPermissions = new HashSet<PlanPermission>();
            MenuPlans = new HashSet<MenuPlan>();
        }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int PlanMonths { get; set; }
        public int ExtraDays { get; set; }
        public string PaypalPlanId { get; set; }
        public int PlanTypeId { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Membership> Memberships { get; set; }
        public virtual ICollection<Discount> Discounts { get; set; }
        public virtual ICollection<PlanPermission> PlanPermissions { get; set; }
        public virtual ICollection<MenuPlan> MenuPlans { get; set; }
    }
}
