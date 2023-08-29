using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MaintenanceSchema;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("TaskList", Schema = "Metadata")]
    public class TaskList : BaseEntity
    {
        public TaskList()
        {
            TaskListDetails = new HashSet<TaskListDetail>();
        }
        public string Title { get; set; }
        public int EquipmentLookupId { get; set; }
        public int WorkOrderTypeId { get; set; }

        public WorkOrderType WorkOrderType { get; set; }
        public EquipmentLookup EquipmentLookup { get; set; }

        public virtual ICollection<TaskListDetail> TaskListDetails { get; set; }
    }
}
