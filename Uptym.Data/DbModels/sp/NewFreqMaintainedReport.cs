using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Uptym.Data.DbModels.sp
{
    [Keyless]
    public class NewFreqMaintainedReport
    {

        public string OperatorName { get; set; }
       
        public int NumberOfFailure { get; set; }
       
       
        

    }


}
