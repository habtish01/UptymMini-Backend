using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.SecuritySchema;

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("UserTransactionTypes", Schema = "Metadata")]
    public class UserTransactionType : StaticLookup
    {
        public UserTransactionType()
        {
            UserTransactionHistories = new HashSet<UserTransactionHistory>();
        }
        public virtual ICollection<UserTransactionHistory> UserTransactionHistories { get; set; }
    }
}
