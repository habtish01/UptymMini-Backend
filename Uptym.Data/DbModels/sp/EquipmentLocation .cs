using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uptym.Data.DbModels.sp
{
    [Keyless]
    public class EquipmentLocation
    {
        //HealthFacilityId OwnerId EquipmentId NumberOfCompletedPMs    NumberOfScheduledPMs
        
        public  string Equipment { get; set; }
        public string Equipment_Category { get; set; }
        public string Functionality_Status { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

    }
}
