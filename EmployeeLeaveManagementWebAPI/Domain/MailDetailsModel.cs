using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Domain
{
    public class MailDetailsModel
    {
        public string TemplatePath { get; set; }
        public string ToMailId { get; set; }
        public string CcMailId { get; set; }
        public int EmployeeId { get; set; }
        public string ManagerName { get; set; }
        public string NewManagerName { get; set; }
        public string NewManagerMailId { get; set; }
        public string EmployeeName { get; set; }
        public string LeaveFromDate { get; set; }
        public string LeaveToDate { get; set; }
        public int NumberOfWorkingDays { get; set; }
        public string ManagerComments { get; set; }
        public string EmployeeComments { get; set; }

    }
}
