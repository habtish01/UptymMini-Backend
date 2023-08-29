using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Uptym.Data.BaseModeling;

namespace Uptym.Data.DbModels.EquipmentSchema
{
    [Table("AutoScheduleAssignedEngineers", Schema = "Equipment")]
    public class AutoScheduleAssignedEngineers : BaseEntity
    {
        public int AutoScheduleId { get; set; }
        public int AssignedEngineerId { get; set; }
    }
}
