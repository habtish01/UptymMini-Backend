using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("HealthFacilityType", Schema = "Metadata")]
    public class HealthFacilityType : DynamicLookup
    {
        public HealthFacilityType()
        {
            HealthFacilties = new HashSet<HealthFacility>();
        }
        public string Description { get; set; }
        public int? CustomerID { get; set; }
        public virtual ICollection<HealthFacility> HealthFacilties { get; set; }
    }
}
