using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("SparepartInventoryDetail", Schema = "Metadata")]
    public class SparepartInventoryDetail
    {
        public int Id { get; set; }

        public int SparePartId { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string ReceiveIssueType { get; set; }
        public string Location { get; set; }
        public int AvailableQuantity { get; set; }

        public int ReceiveId { get; set; }

        public DateTime DeliveryDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Sparepart Sparepart { get; set; }
    }
}
