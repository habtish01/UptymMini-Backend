using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("EquipmentTypes", Schema = "Metadata")]
    public class EquipmentType : DynamicLookup
    {
        public EquipmentType()
        {
        }
        public string Description { get; set; }
        public int? CustomerID { get; set; }

    }
}
