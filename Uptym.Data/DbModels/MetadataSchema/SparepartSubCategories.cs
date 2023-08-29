using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MaintenanceSchema;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("SparepartSubCategories", Schema = "Metadata")]
    public class SparepartSubCategories : DynamicLookup
    {
        public SparepartSubCategories()
        {
            Spareparts = new HashSet<Sparepart>();
        }
        public string Code { get; set; }
        public string Description { get; set; }
        public int SparepartCategoriesId { get; set; }
        public virtual SparepartCategories SparepartCategories { get; set; }
        public virtual ICollection<Sparepart> Spareparts { get; set; }
    }
}
