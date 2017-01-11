using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Domain
{
   public class LeaveTransactionResponse
    {
        public int noOfWorkingDays { get; set; } 

        public int availableLeaveBalance { get; set; }
        public int advanceLeaveBalance { get; set; }

        public int responseCode { get; set; }
        

    }
}
