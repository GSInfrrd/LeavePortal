using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class HRController : ApiController
    {
        HRManagement hrOperations = new HRManagement();

        public bool Post(EmployeeDetailsModel model)
        {
            var result = hrOperations.SubmitEmployeeDetails(model);
            return result;
        }
    }
}
