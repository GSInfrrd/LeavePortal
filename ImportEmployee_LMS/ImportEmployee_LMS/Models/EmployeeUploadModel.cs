using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImportEmployee_LMS.Models
{
    public class EmployeeUploadModel
    {
        public HttpPostedFileBase EmployeeFile { get; set; }
    }
}