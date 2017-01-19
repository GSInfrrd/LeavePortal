using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class RewardLeaveModel
    {
        public int ManagerId { get; set; }
        public string ManagerName { get; set; }
        public int ProjectId { get; set; }
        public int EmplooyeeId { get; set; }
        public int NumberofDays { get; set; }
        public List<Details> Projects { get; set; }
        public List<Details> Employees { get; set; } 
        
    }

    public class Details
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
