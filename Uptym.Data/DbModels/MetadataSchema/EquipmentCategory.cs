using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.EquipmentSchema;
#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("EquipmentCategories", Schema = "Metadata")]
    public class EquipmentCategory : DynamicLookup
    {
        public EquipmentCategory()
        {
            EquipmentLookups = new HashSet<EquipmentLookup>();
        }

        public string Description { get; set; }

        public int EquipmentTypeId { get; set; }

        public int? CustomerID { get; set; }
        public virtual EquipmentType EquipmentType { get; set; }
        public virtual ICollection<EquipmentLookup> EquipmentLookups { get; set; }
    }
}
