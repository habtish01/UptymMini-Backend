using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("ErrorCodes", Schema = "Metadata")]
    public class ErrorCode : DynamicLookup
    {
        public ErrorCode()
        {
         }

        public string Description { get; set; }

        public int EquipmentLookupId { get; set; }
         public virtual EquipmentLookup EquipmentLookup { get; set; }
    }
}
