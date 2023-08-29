using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MaintenanceSchema;

namespace Uptym.Data.DbModels.SecuritySchema
{
    [Table("UserTransactionHistories", Schema = "Security")]
    public class UserTransactionHistory : BaseEntity
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int UserTransactionTypeId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual UserTransactionType UserTransactionType { get; set; }
    }
}
