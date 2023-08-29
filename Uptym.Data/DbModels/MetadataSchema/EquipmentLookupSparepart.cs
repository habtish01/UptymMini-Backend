using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("EquipmentLookupSparepart", Schema = "Metadata")]
    public class EquipmentLookupSparepart
    {
        public int Id { get; set; }

        public int SparepartId { get; set; }
        public int EquipmentLookupId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Sparepart Sparepart { get; set; }
        public virtual EquipmentLookup EquipmentLookup { get; set; }
    }
}
