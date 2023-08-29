using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MaintenanceSchema;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Data.Enums;

#nullable disable

namespace Uptym.Data.DbModels.EquipmentSchema
{
    [Table("AutoSchedules", Schema = "Equipment")]
    public class AutoSchedule : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public int EquipmentId { get; set; }
        public int? AutoSchedulePerformerId { get; set; }
        public int AutoSchedulePriorityId { get; set; }
        public DateTime? EndDate { get; set; }
        public TimeSpan? EndTime { get; set; }

        //  public int MetricsId { get; set; }

        public virtual Equipment Equipment { get; set; }
        public int IntervalId { get; set; }
        public int AssignedToId { get; set; }
        public virtual EquipmentScheduleInterval Interval { get; set; }
        public int EquipmentScheduleTypeId { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public TimeSpan? ScheduledTime { get; set; }
        // public virtual Metrics Metrices { get; set; }

        public virtual AssignedtoEnum AssignedTo { get; set; }

        public virtual EquipmentScheduleType EquipmentScheduleType { get; set; }

        public virtual ICollection<WorkOrderHeader> WorkOrderHeader { get; set; }
        public virtual ICollection<AutoScheduleAssignedEngineers> AutoScheduleAssignedEngineers { get; set; }
        public virtual ICollection<Tasks> TaskList { get; set; }

    }
}
