using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.EquipmentSchema;
using Uptym.Data.DbModels.SecuritySchema;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("HealthFacilities", Schema = "Metadata")]
    public partial class HealthFacility : DynamicLookup
    {
        public HealthFacility()
        {
            Instruments = new HashSet<Equipment>();
            Users = new HashSet<ApplicationUser>();
        }
        public string Code { get; set; }
        public string City { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public int RegionId { get; set; }
        public int HealthFacilityTypeId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int? CustomerID { get; set; }
        public virtual Region Region { get; set; }
        public virtual HealthFacilityType HealthFacilityType { get; set; }
        public virtual ICollection<Equipment> Instruments { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}
