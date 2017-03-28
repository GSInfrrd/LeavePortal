using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Domain
{
   public class ConsolidatedEmployeeLeaveDetailsModel
    {
        public  ConsolidatedEmployeeLeaveDetailsModel()
            {
            this.DetailedLeaveReports = new List<DetailedLeaveReport>();
            }
       
        public int Id { get; set; }
        [DisplayAttribute(Name = "Employee Id")]
        public int RefEmployeeId { get; set; }
        [DisplayAttribute(Name = "Employee Name")]
        public string EmployeeName { get; set;}

        [DisplayAttribute(Name = "Applied Leaves")]
        public Nullable<int> AppliedLeavesCount { get; set; }

        [DisplayAttribute(Name = "Casual Leaves")]
        public Nullable<int> AppliedCasualLeavesCount { get; set; }

        [DisplayAttribute(Name = "Sick Leaves")]
        public Nullable<int> AppliedSickLeavesCount { get; set; }
        [DisplayAttribute(Name = "Work from Home")]
        public Nullable<int> WorkFromHomeCount { get; set; }
        [DisplayAttribute(Name = "Loss of Pay")]
        public Nullable<int> LossofPayCount { get; set; }

        [DisplayAttribute(Name = "Advanced Leaves")]
        public Nullable<int> AdvancedLeavesCount { get; set; }

        [DisplayAttribute(Name = "Comp Off")]
        public Nullable<int> CompOffCount { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public List<DetailedLeaveReport> DetailedLeaveReports { get; set; }
    }

    public class DetailedLeaveReport
    {
        public string EmpoyeeName { get; set; }
        public string LeaveType { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class EmployeeLeaveMasterDetails
    {
        public int Id { get; set; }
        public int RefEmployeeId { get; set; }
        public Nullable<int> RewardedLeaveCount { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> SpentAdvanceLeave { get; set; }
        public Nullable<int> TakenLossOfPay { get; set; }
        public Nullable<double> EarnedCasualLeave { get; set; }
        public Nullable<int> TakenCompOff { get; set; }
    }

}

