using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uptym.Data.DbModels.sp
{
    [Keyless]
    public class InstrumentTotalSummary
    {

        public string AvgInstrumentUptime { get; set; }

        public string AvgInstrumentDowntime { get; set; }

        //MTBF(mean time between failure)
        public string Mtbf { get; set; }

        //MTTR (Mean time to repair an equipment) specific to the engineer
        public string Mrttr { get; set; }

    }
}
