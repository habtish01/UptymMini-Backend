using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.EquipmentSchema;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("Manufacturer", Schema = "Metadata")]
    public partial class Manufacturer : DynamicLookup
    {
        public Manufacturer()
        {
            EquipmentLookups = new HashSet<EquipmentLookup>();
        }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string ContactPerson { get; set; }
        public string Description { get; set; }
        public int? CustomerID { get; set; }
        public virtual ICollection<EquipmentLookup> EquipmentLookups { get; set; }
    }
}
