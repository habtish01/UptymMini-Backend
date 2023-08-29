using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uptym.Data.DbModels.sp
{
    [Keyless]
    public class InspectionCompliance
    {
        //HealthFacilityId OwnerId EquipmentId NumberOfCompletedPMs    NumberOfScheduledPMs
        public int HealthFacilityId { get; set;}
        public int OwnerId { get; set;}
        public int EquipmentId { get; set; }
        public int NumberOfCompletedIMs { get; set; }
        public int NumberOfScheduledIMs { get; set; }

    }
}
