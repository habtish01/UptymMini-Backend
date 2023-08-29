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
    [Table("Tasks", Schema = "Equipment")]
    public class Tasks : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? AutoScheduleId { get; set; }
        public int MetricsId { get; set; }


        public virtual Metrics Metrices { get; set; }

        public virtual AutoSchedule AutoSchedules { get; set; }



    }
}
