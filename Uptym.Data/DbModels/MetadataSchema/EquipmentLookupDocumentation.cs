using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("EquipmentDocumentations", Schema = "Metadata")]
    public class EquipmentLookupDocumentation : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string DocumentPath { get; set; }

        public int DocumentationTypeId { get; set; }

        public int EquipmentLookupId { get; set; }

        public virtual EquipmentLookup EquipmentLookup { get; set; }
        public virtual DocumentationType DocumentationType { get; set; }
    }
}
