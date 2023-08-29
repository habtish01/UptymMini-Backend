using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.SubscriptionSchema
{
    [Table("Permissions", Schema = "Subscription")]
    public class Permission : StaticLookup
    {
        public Permission()
        {
            PlanPermissions = new HashSet<PlanPermission>();
        }

        public string Description { get; set; }
        public virtual ICollection<PlanPermission> PlanPermissions { get; set; }

    }
}
