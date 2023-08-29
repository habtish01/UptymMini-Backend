using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Uptym.Data.BaseModeling;
using Uptym.Data.DbModels.SubscriptionSchema;
using Uptym.Data.Enums;

#nullable disable

namespace Uptym.Data.DbModels.EquipmentSchema
{
    [Table("Timesheets", Schema = "Equipment")]
    public partial class Timesheet : BaseEntity
    {
        public string OperatorName { get; set; }
        public string LocationName { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int EquipmentId { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}
