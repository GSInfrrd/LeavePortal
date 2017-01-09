using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Utils
{
    public class EmployeeCommon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String RoleName { get; set; }
        public Nullable<System.DateTime> DateOfJoining { get; set; }
        public double? Experience { get; set; }
        public Nullable<int> ManagerId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public String ManagerName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> Lastlogin { get; set; }
        public string ProjectName { get; set; }
        public int? TotalLeaveCount { get; set; }
        public double TotalApplied { get; set; }
        public double TotalSpent { get; set; }
        public string ManagerEmailId { get; set; }
        public int? TotalAdvanceLeaveToTake { get; set; }
        public int? TotalCasualLeave { get; set; }
        public IDictionary<string,int> MonthlyLeaveReport { get; set; }
    }
}