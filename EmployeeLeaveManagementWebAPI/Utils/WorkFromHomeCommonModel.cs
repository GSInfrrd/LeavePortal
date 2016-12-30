using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Utils
{
  public class WorkFromHomeCommonModel
    {
        public long Id { get; set; }
        public int RefEmployeeId { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int RefStatus { get; set; }
        public string Reason { get; set; }
        public string StatusName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public int RefReason { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
