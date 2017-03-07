using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Domain
{
    public class ProjectDetails
    {
        public string ProjectName { get; set; }
        public string Description { get; set; }

        public List<TechnologyDetails> Technologies { get; set; }
        public List<TechnologyDescriptions> TechnologyDetails { get; set; }
        public DateTime startDate { get; set; }
        public int RefManager { get; set; }
    }
}
