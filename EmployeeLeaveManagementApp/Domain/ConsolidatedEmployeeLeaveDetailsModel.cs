using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Domain
{
    public class ConsolidatedEmployeeLeaveDetailsModel
    {
        public int Id { get; set; }
        public int RefEmployeeId { get; set; }
        public Nullable<int> EarnedLeavesCount { get; set; }
        public Nullable<int> AppliedLeavesCount { get; set; }
        public Nullable<int> WorkFromHomeCount { get; set; }
        public Nullable<int> LossofPayCount { get; set; }
        public System.DateTime CreatedBy { get; set; }
    }
}
