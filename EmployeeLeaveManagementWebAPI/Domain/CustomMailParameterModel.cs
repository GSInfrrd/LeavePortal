using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Domain
{
    public class CustomMailParameterModel
    {
        public string MailFrom { get; set; }

        public string MailTo { get; set; }

        public string Subject { get; set; }

        public int NubmerOfWorkingDays { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string LeaveComments { get; set; }

    }
}
