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
        [DisplayAttribute(Name = "Employee Id")]
        public int RefEmployeeId { get; set; }
        [DisplayAttribute(Name = "Employee Name")]
        public string EmployeeName { get; set;}
        [DisplayAttribute(Name = "Earned Leaves")]
        public Nullable<int> EarnedLeavesCount { get; set; }
        [DisplayAttribute(Name = "Applied Leaves")]
        public Nullable<int> AppliedLeavesCount { get; set; }
        [DisplayAttribute(Name = "Work from Home")]
        public Nullable<int> WorkFromHomeCount { get; set; }
        [DisplayAttribute(Name = "Loss of Pay")]
        public Nullable<int> LossofPayCount { get; set; }
        public System.DateTime CreatedDate { get; set; }


    }

}

