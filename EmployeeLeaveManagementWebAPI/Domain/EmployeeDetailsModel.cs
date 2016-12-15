using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Domain
{
 public class EmployeeDetailsModel
    {
        public EmployeeDetailsModel()
        {
            this.Announcements = new List<Announcement>();

        }
        public int Id { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ProjectName { get; set; }
        public String RoleName { get; set; }
        public int? TotalLeaveCount { get; set; }
        public int TotalCountTaken { get; set; }
        public string ManagerName { get; set; }
        public System.DateTime DateOfJoining { get; set; }

        public List<Announcement> Announcements { get; set; }
    }

    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CarouselContent { get; set; }
        public string ImagePath { get; set; }
    }
}
