using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uptym.Data.DbModels.sp
{
    [Keyless]
    public class MaintenanceCost
    {
        //HealthFacilityId OwnerId EquipmentId NumberOfCompletedPMs    NumberOfScheduledPMs
        public int HealthFacilityId { get; set;}
        public int OwnerId { get; set;}
        
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public int TotalCosts { get; set; }

    }
}
