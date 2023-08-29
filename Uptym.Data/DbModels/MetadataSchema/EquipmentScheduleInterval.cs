using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("EquipmentScheduleIntervals", Schema = "Metadata")]
    public class EquipmentScheduleInterval : StaticLookup
    {
    }
}
