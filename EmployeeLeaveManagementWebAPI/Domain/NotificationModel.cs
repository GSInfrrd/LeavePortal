using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Domain
{
    public class NotificationModel
    {
        public int Id { get; set; }
        public int RefEmployeeId { get; set; }
        public string Text { get; set; }
        public System.DateTime CreatedDate { get; set; }

        public int RefNotificationType { get; set; }

        //public virtual EmployeeDetail EmployeeDetail { get; set; }
    }
}
