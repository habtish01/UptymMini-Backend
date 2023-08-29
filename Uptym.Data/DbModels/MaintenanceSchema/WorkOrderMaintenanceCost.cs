using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MetadataSchema;

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("WorkOrderMaintenanceCosts", Schema = "Maintenance")]
    public class WorkOrderMaintenanceCost : BaseEntity
    {
        public decimal Cost { get; set; }

        public int WorkOrderId { get; set; }
        public string MaintenanceType{ get; set; }
     //   public int? SparePartId { get; set; }
        public string Description { get; set; }

        //  public virtual Sparepart Sparepart { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
       // public virtual Sparepart Sparepart { get; set; }
    }

}
