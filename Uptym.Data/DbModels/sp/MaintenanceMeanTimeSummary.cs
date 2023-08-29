using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uptym.Data.DbModels.sp
{
    [Keyless]
    public class MaintenanceMeanTimeSummary
    {
        //HealthFacilityId OwnerId EquipmentId NumberOfFailure OfflineHours HoursSinceActive    OperationalHours

        public int HealthFacilityId { get; set; }
        public int OwnerId { get; set; }
        public int EquipmentId { get; set; }
        public int NumberOfFailure { get; set; }
        public int OfflineHours { get; set; }
        public int HoursSinceActive { get; set; }
        public int OperationalHours { get; set; }

    }
}
