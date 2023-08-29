using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MaintenanceSchema;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("SparepartCategories", Schema = "Metadata")]
    public class SparepartCategories : DynamicLookup
    {
        public SparepartCategories()
        {
            SparepartSubCategories = new HashSet<SparepartSubCategories>();
        }
        public string Code { get; set; }
        public string Description { get; set; }

        public virtual ICollection<SparepartSubCategories> SparepartSubCategories { get; set; }
    }
}
