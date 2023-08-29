using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.DbModels.SubscriptionSchema;

namespace Uptym.Data.DbModels.ConfigurationSchema
{
    [Table("MenuPlans", Schema = "Configuration")]
    public class MenuPlan
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int PlanId { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual Plan Plan { get; set; }
                 
    }
}
