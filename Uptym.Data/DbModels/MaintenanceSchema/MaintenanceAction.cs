using System;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.SecuritySchema;

#nullable disable

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("MaintenanceActions", Schema = "Maintenance")]
    public partial class MaintenanceAction : BaseEntity
    
    
    {
        public DateTime MaintenanceActionDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public int EngineerId { get; set; }
        public int WorkOrderId { get; set; }

        public virtual ApplicationUser Engineer { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
