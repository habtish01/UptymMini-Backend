using System.ComponentModel.DataAnnotations.Schema;

using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("EquipmentScheduleTypes", Schema = "Metadata")]
    public class EquipmentScheduleType : StaticLookup
    {
    }
}
