using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("Operators", Schema = "Metadata")]
    public class Operator : DynamicLookup
    {
        public Operator()
        {
         }

        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public int EquipmentLookupId { get; set; }
         public virtual EquipmentLookup EquipmentLookup { get; set; }
    }
}
