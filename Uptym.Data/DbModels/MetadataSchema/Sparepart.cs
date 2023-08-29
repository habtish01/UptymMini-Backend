using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MaintenanceSchema;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("Spareparts", Schema = "Metadata")]
    public class Sparepart : DynamicLookup
    {
      
        public string Description { get; set; }

        public int EquipmentLookupId { get; set; }
        public int MinimumQuantity { get; set; }
        public int SparepartSubCategoriesId { get; set; }

        public int SparepartCategoriesId { get; set; }
        //public int Quantity { get; set; }
        //public decimal Amount { get; set; }
        //public string ReceiveIssueType { get; set; }
        //public int AvailableQuantity { get; set; }

        // public virtual WorkOrderSparepart WorkOrderSpareparts { get; set; }
        //public virtual ICollection<WorkOrderSparepart> WorkOrderSpareparts { get; set; }
        public virtual SparepartSubCategories SparepartSubCategories { get; set; }

        public virtual SparepartCategories SparepartCategories { get; set; }

        //public virtual EquipmentLookup EquipmentLookup { get; set; }


    }

}
