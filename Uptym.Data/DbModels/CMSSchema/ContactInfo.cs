using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.CMSSchema
{
    [Table("ContactInfos", Schema = "CMS")]
    public class ContactInfo : NullableBaseEntity
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        // Manual Bank Info
        public string BankName { get; set; }
        public string AccName { get; set; }
        public string AccNo { get; set; }
        // Social Links
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string LinkedIn { get; set; }
    }
}
