using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("SparepartEquipmentDetail", Schema = "Metadata")]
    public class SparepartEquipmentDetail 
    {
        public int Id { get; set; }

        public int SparePartId { get; set; }
        public int EquipmentLookupId { get; set; }

        public bool IsDeleted { get; set; }

        public virtual Sparepart Sparepart { get; set; }
        public virtual EquipmentLookup EquipmentLookup { get; set; }
    }
}
