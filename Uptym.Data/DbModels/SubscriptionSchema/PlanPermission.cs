using System.ComponentModel.DataAnnotations.Schema;

namespace Uptym.Data.DbModels.SubscriptionSchema
{
    [Table("PlanPermissions", Schema = "Subscription")]
    public class PlanPermission
    {
        public int PlanId { get; set; }
        public int PermissionId { get; set; }
        public int? LimitDays { get; set; }
        public int? LimitJobs { get; set; }
        public virtual Plan Plan { get; set; }
        public virtual Permission Permission { get; set; }

    }

    public class PlanPermissionDrp
    {
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public decimal PlanPrice { get; set; }
        public int PlanPlanMonths { get; set; }
        public int PlanExtraDays { get; set; }
        public string PlanPaypalPlanId { get; set; }
        public string PlanStatus { get; set; }

        public int PermissionId { get; set; }
        public string PermissionName { get; set; }
        public int? LimitDays { get; set; }
        public int? LimitJobs { get; set; }

    }

}
