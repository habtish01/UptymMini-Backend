using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("WorkOrderTaskLists", Schema = "Maintenance")]
    public class WorkOrderTaskList : BaseEntity
    {
        public string Name { get; set; }
        public int MetricsId { get; set; }
        public string Description { get; set; }
        public int TaskId { get; set; }
        public int WorkOrderId { get; set; }
    }
}
