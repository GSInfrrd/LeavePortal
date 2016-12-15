using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeaveManagementApp.Models
{
    public class LeaveTypeModel
    {
        public int LeaveType { get; set; }
        public System.DateTime FromDate  { get; set; }
        public System.DateTime ToDate { get; set; }
        public string Comment { get; set; }
    }
}