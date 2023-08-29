using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.SecuritySchema;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("Continents", Schema = "Metadata")]
    public class Continent : StaticLookup
    {
        public Continent()
        {
            Countries = new HashSet<Country>();
            //ApplicationUsers = new HashSet<ApplicationUser>();
        }
        public string ShortCode { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
        //public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
