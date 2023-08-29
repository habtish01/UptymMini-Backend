using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.SubscriptionSchema;
using Uptym.Data.Enums;

#nullable disable

namespace Uptym.Data.DbModels.EquipmentSchema
{
    [Table("EquipmentContracts", Schema = "Equipment")]
    public partial class EquipmentContract : BaseEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? ContractProviderId { get; set; }
        public string? OfflineContractor { get; set; }
        public int EquipmentId { get; set; }
        public string DocumentPath { get; set; }

        public virtual Customer ContractProvider { get; set; }
        public virtual Equipment Equipment { get; set; }
        public virtual ContractTypeEnum ContractType { get; set; }
    }
}
