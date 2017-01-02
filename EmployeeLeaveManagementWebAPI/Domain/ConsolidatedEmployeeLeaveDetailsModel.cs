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
        public int Id { get; set; }

        [Display(Name = "Employee Id")]
        public int RefEmployeeId { get; set; }

        [Display(Name ="Employee Name")]
        public string EmployeeName { get; set; }

        [Display(Name = "Earned Leaves")]
        public Nullable<int> EarnedLeavesCount { get; set; }

        [Display(Name = "Applied Leaves")]
        public Nullable<int> AppliedLeavesCount { get; set; }
        [Display(Name = "Work from Home")]
        public Nullable<int> WorkFromHomeCount { get; set; }

        [Display(Name = "Loss of Pay")]
        public Nullable<int> LossofPayCount { get; set; }
        public System.DateTime CreatedBy { get; set; }

    }
}
