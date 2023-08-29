using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("MaintenanceCheckListDetails", Schema = "Metadata")]
    public class MaintenanceCheckListDetail : BaseEntity
    {
        public string Task { get; set; }
        public int MaintenanceCheckListId { get; set; }

        public virtual MaintenanceCheckList MaintenanceCheckList { get; set; }
    }
}
