using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SecuritySchema;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("CountryPeriods", Schema = "Metadata")]
    public class CountryPeriod : StaticLookup
    {
        // Weekly, Monthly, Quarterly, Annualy
        public CountryPeriod()
        {
            Countries = new HashSet<Country>();
        }
        public virtual ICollection<Country> Countries { get; set; }
    }
}
