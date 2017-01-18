using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Domain
{
    public class ResourceDetails
    {
        public List<string> Skills { get; set; }
        public List<EmployeeDetailsModel> ListOfHR { get; set; }
        public List<ResourceRequestDetailModel> ResourceRequestHistory { get; set; }

        public int Count { get; set; }
        public bool Result { get; set; }
    }
}
