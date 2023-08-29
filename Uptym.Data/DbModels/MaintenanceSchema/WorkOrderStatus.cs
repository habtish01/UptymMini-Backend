using System;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

#nullable disable

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("WorkOrderStatus", Schema = "Maintenance")]
    public class WorkOrderStatus : BaseEntity
    {
        public string Title { get; set; }
        //public int StatusType { get; set; }
       
        public DateTime StatusDate { get; set; }
        public string Remark { get; set; }
        public int WorkOrderId { get; set; }
        public int WorkOrderStatusTypeId { get; set; }
        public int? EquipmentStatusId { get; set; }

        public virtual WorkOrderStatusType WorkOrderStatusType { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
