using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Domain
{
   public class HolidayModel
    {
            public long Id { get; set; }
            public Nullable<System.DateTime> Date { get; set; }
            public string Description { get; set; }
            public long Year { get; set; }
            public Nullable<System.DateTime> CreatedDate { get; set; }
            public Nullable<System.DateTime> ModifiedDate { get; set; }
            public string CreatedBy { get; set; }
            public string ModifiedBy { get; set; }
            public Nullable<bool> IsActive { get; set; }
    }
}
