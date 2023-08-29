using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.EquipmentSchema;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Data.Enums;

#nullable disable

namespace Uptym.Data.DbModels.MaintenanceSchema
{
    [Table("WorkOrderHeader", Schema = "Maintenance")]
    public partial class WorkOrderHeader : BaseEntity

    {
        public WorkOrderHeader()
        {
            WorkOrders = new HashSet<WorkOrder>();
        }

        public string Title { get; set; }
        public DateTime ReportedDate {
            get; set; }

        
public DateTime EquipmentFailureDate { get; set; }
        public string? Description { get; set; }
        public int EquipmentId { get; set; }
        public int? ReportedById { get; set; }
        public int? ErrorCodeId { get; set; }
        public int? ProblemTypeId { get; set; }
        public int ReporterPriorityId { get; set; }
        public int WorkorderTypeId { get; set; }
        public bool IsClosed { get; set; }
        public int? AutoScheduleId { get; set; }
        public int? AutoScheduleStatusId { get; set; }
        public DateTime? NextScheduleDate { get; set; }
        public string FailureTime { get; set; }
        public int? ScheduleApproval { get; set; }

        // public int ReportCheckListId { get; set; } emmited for this version


        public virtual ErrorCode ErrorCode { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual WorkOrderType WorkOrderType { get; set; }
        public virtual ProblemType ProblemType { get; set; }
        public virtual ApplicationUser ReportedBy { get; set; }
        public virtual AutoSchedule AutoSchedule { get; set; }
        public virtual PriorityEnum ReporterPriority { get; set; }
        public virtual AutoScheduleStatus AutoScheduleStatus { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
        public virtual DateTime GetWSDate
        {
            get
            {
                DateTime WSD = DateTime.Today;
                foreach (WorkOrder wo in WorkOrders)
                {
                    WSD = wo.WorkorderStatusesdate;
                }
                return WSD;
            }
        }
    }
}
