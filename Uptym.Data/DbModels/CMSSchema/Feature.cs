using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.CMSSchema
{
    [Table("Features", Schema = "CMS")]
    public class Feature : NullableBaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string LogoPath { get; set; }
    }
}
