using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.Enums;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("EquipmentLookupSchedules", Schema = "Metadata")]
    public class EquipmentLookupSchedule : BaseEntity
    {

        public EquipmentLookupSchedule()
        {
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ScheduleDuration { get; set; }
        public int EquipmentLookupId { get; set; }
        public int MetricsId { get; set; }
        public virtual EquipmentLookup EquipmentLookup { get; set; }
        public int EquipmentScheduleIntervalId { get; set; }
        public virtual EquipmentScheduleInterval EquipmentScheduleInterval { get; set; }
        public virtual Metrics Metrics { get; set; }
        public int EquipmentScheduleTypeId { get; set; }
        public virtual EquipmentScheduleType EquipmentLookupScheduleType { get; set; }
        public virtual ICollection<MetadataTasks> Tasks { get; set; }


    }
}
