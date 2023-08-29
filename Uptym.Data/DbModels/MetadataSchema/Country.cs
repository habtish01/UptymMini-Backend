using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("Countries", Schema="Metadata")]
    public  class Country: DynamicLookup
    {
        public Country()
        {
            Regions = new HashSet<Region>();
        }

        public int ContinentId { get; set; }
        public int CountryPeriodId { get; set; }
        public string ShortCode { get; set; }
        public string ShortName { get; set; }
        public string NativeName { get; set; }
        public string Flag { get; set; }
        public string CurrencyCode { get; set; }
        public string CallingCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public decimal Population { get; set; }


        public virtual Continent Continent { get; set; }
        public virtual CountryPeriod CountryPeriod { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
    }
}
