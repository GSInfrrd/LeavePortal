using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Domain
{
    public class ResourceRequestDetailModel
    {
        public int RequestFromId { get; set; }
        public string RequestFromName { get; set; }
        public int RequestToId { get; set; }
        public string RequestToName { get; set; }
        public string ResourceRequestTitle { get; set; }
        public int NumberRequestedResources { get; set; }
        public string Skills { get; set; }
        public string Ticket { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int Status { get; set; }
        public string StatusValue { get; set; }
    }
}
