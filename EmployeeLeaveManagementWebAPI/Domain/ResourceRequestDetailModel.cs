using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ResourceRequestDetailModel
    {
        public long Id { get; set; }
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

        public int Count { get; set; }

        public bool Result { get; set; }
    }
}
