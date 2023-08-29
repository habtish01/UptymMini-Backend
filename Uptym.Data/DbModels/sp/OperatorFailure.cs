using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uptym.Data.DbModels.sp
{
    [Keyless]
    public class OperatorFailure
    {
        //HealthFacilityId OwnerId EquipmentId NumberOfCompletedPMs    NumberOfScheduledPMs
        public int HealthFacilityId { get; set;}
        public int OwnerId { get; set;}
        
        public string Name { get; set; }
        public int NumberOfReportedFailures { get; set; }

    }
}
