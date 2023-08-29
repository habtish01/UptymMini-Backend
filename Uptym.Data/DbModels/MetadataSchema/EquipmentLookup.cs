using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.EquipmentSchema;

#nullable disable

namespace Uptym.Data.DbModels.MetadataSchema
{
    [Table("EquipmentLookups", Schema = "Metadata")]
    public class EquipmentLookup : BaseEntity
    {
        public EquipmentLookup()
        {
            ErrorCodes = new HashSet<ErrorCode>();
            Equipments = new HashSet<Equipment>();
            MaintenanceCheckLists = new HashSet<MaintenanceCheckList>();
            ProblemTypes = new HashSet<ProblemType>();
            Spareparts = new HashSet<Sparepart>();
            EquipmentLookupDocumentations = new HashSet<EquipmentLookupDocumentation>();
            EquipmentSchedules = new HashSet<EquipmentLookupSchedule>();
        }
        public string Name { get; set; }
        public int EquipmentCategoryId { get; set; }
        public int ManufacturerId { get; set; }

        public int EquipmentTypeId { get; set; }
        public int? CustomerID { get; set; }
        public virtual EquipmentType EquipmentType{ get; set; }
        public virtual EquipmentCategory EquipmentCategory { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<EquipmentLookupSchedule> EquipmentSchedules { get; set; }
        public virtual ICollection<ErrorCode> ErrorCodes { get; set; }
        public virtual ICollection<Sparepart> Spareparts { get; set; }

        public virtual ICollection<EquipmentLookupDocumentation> EquipmentLookupDocumentations { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }
        public virtual ICollection<MaintenanceCheckList> MaintenanceCheckLists { get; set; }
        public virtual ICollection<ProblemType> ProblemTypes { get; set; }

    }
}
