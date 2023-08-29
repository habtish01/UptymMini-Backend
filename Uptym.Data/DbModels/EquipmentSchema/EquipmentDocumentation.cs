using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.MetadataSchema;
using Uptym.Data.DbModels.SubscriptionSchema;

namespace Uptym.Data.DbModels.EquipmentSchema
{
    [Table("EquipmentDocumentations", Schema = "Equipment")]
    public class EquipmentDocumentation : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string DocumentPath { get; set; }

        public int CustomerId { get; set; }

        public int EquipmentId { get; set; }

        public int DocumentationTypeId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual DocumentationType DocumentationType { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
