using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uptym.Data.DbModels.sp
{
    [Keyless]
    public class EquipmentSummary
    {
        //NumberOfNotFunctionalEquipment
        public int NumberOfNotFunctionalEquipment { get; set; }
        public int NumberOfEquipment { get; set; }
        public int EquipmentUnderWarranty { get; set; }
        public int EquipmentUnderService { get; set; }
        public int EquipmentWithWarranty { get; set; }



    }
}
