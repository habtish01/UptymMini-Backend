using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MaintenanceSchema;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("MaintenanceCheckList", Schema = "Metadata")]
    public class MaintenanceCheckList : BaseEntity
    {
        public MaintenanceCheckList()
        {
            MaintenanceCheckListDetails = new HashSet<MaintenanceCheckListDetail>();
        }
        public string Title { get; set; }
        public int EquipmentLookupId { get; set; }
        public int WorkOrderTypeId { get; set; }

        public WorkOrderType WorkOrderType { get; set; }
        public EquipmentLookup EquipmentLookup { get; set; }

        public virtual ICollection<MaintenanceCheckListDetail> MaintenanceCheckListDetails { get; set; }
    }
}
