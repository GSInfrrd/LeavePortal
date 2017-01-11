using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Domain
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public int? ManagerId { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> Lastlogin { get; set; }
        public int RefEmployeeId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ProjectName { get; set; }
        public int RefRoleId { get; set; }
        public int? TotalLeaveCount { get; set; }
        public int? TotalAdvanceLeaveTotake { get; set; }
        public int? TotalCasualLeave { get; set; }
        public int? TotalLOPLImit { get; set; }
        public int TotalCountTaken { get; set; }
        public string ManagerName { get; set; }
        public string ManagerEmail { get; set; }
        public System.DateTime DateOfJoining { get; set; }

        public string Imagepath { get; set; }

    }
}
