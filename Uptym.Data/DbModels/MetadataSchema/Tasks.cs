using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MaintenanceSchema;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SecuritySchema;
using Uptym.Data.Enums;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("MetadataTasks", Schema = "Metadata")]
    public class MetadataTasks : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int EquipmentLookupScheduleId { get; set; }
        public int MetricsId { get; set; }


        public virtual Metrics Metrices { get; set; }

        public virtual EquipmentLookupSchedule EquipmentLookupSchedule { get; set; }



    }
}
