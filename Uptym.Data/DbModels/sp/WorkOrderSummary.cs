using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uptym.Data.DbModels.sp
{
    [Keyless]
    public class WorkOrderSummary
    {
        //NumberOfNewWorkOrder	NumberOfWorkOrderAwaitingConfirmation	
        //NumberOfUnhandledInspection	NumberOfOverdueWorkOrder	NumberOfOpenWorkOrder

        public int NumberOfNewWorkOrder { get; set; }

        public int NumberOfWorkOrderAwaitingConfirmation { get; set; }

        public int NumberOfUnhandledInspection { get; set; }

        public int NumberOfOverdueWorkOrder { get; set; }

        public int NumberOfOpenWorkOrder { get; set; }
        public int NumberOfClosedWorkOrder { get; set; }

    }
}
