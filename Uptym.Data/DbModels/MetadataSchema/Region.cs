using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("Regions", Schema="Metadata")]
    public partial class Region: DynamicLookup
    {
        public Region()
        {
            HealthFacilties = new HashSet<HealthFacility>();
        }

        public int CountryId { get; set; }
        public string ShortName { get; set; }

        public virtual Country Country { get; set; }

        
        public virtual ICollection<HealthFacility> HealthFacilties { get; set; }
    }
}
