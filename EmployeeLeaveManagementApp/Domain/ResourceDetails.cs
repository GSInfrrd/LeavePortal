using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Domain
{
    public class ResourceDetails
    {
        public int Id { get; set; }
        public int RequestFromId { get; set; }
        public int RequestToId { get; set; }
        public string ResourceRequestTitle { get; set; }
        public int NumberRequestedResources { get; set; }
        public List<string> Skills { get; set; }
        public List<string> ListOfHR { get; set; }
        public string Ticket { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Status { get; set; }

    }
}
