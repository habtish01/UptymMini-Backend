using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.SubscriptionSchema;
using Uptym.Data.Enums;

#nullable disable

namespace Uptym.Data.DbModels.EquipmentSchema
{
    [Table("EquipmentOperators", Schema = "Equipment")]
    public partial class EquipmentOperator : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public int EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
