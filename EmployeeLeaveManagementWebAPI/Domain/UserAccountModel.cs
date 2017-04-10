using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Domain
{
    public class UserAccountModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> Lastlogin { get; set; }
        public int RefEmployeeId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }           
        public int RefRoleId { get; set; }   

        public string Imagepath { get; set; }
        public DateTime DateOfJoining { get; set; }
        public int RefProfileType { get; set; }

        public Nullable<int> IsHelpDeskMember { get; set; }
    }
}
