using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MaintenanceSchema;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Data.Enums;

#nullable disable

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("MaintenanceTasks", Schema = "Maintenance")]
    public class MaintenanceTasks : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int WorkOrderID { get; set; }
        public int MetricsId { get; set; }


        public virtual Metrics Metrices { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }




    }
}
