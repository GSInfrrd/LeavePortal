using LMS_WebAPP_Domain;
using LMS_WebAPP_ServiceHelpers;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class ProfileController : Controller
    {
        UserManagement usrManagement = new UserManagement();
        // GET: Profile
        public async Task<JsonResult> EditEmployeeDetails(EmployeeDetailsModel model)
        {
            var result = usrManagement.EditEmployeeDetailsAsync(model);
            return Json(new { result=result});
        }

        public async Task<JsonResult> EditEmployeeEducationDetails(List<EmployeeEducationDetails> educationDetails)
        {
            foreach(var item in educationDetails)
            {
                var fromDate = item.TimePeriod.Split('-')[0];
                var toDate = item.TimePeriod.Split('-')[1];
                item.FromDate = Convert.ToDateTime(fromDate);
                item.ToDate = Convert.ToDateTime(toDate);
            }
            var employeeId = ((UserAccount)Session[Constants.SESSION_OBJ_USER]).RefEmployeeId;
            var result = usrManagement.EditEmployeeEducationDetailsAsync(educationDetails,employeeId);
            return Json(new { result = result });
        }



        public async Task<JsonResult> EditEmployeeExperienceDetails(List<EmployeeExperienceDetails> experienceDetails)
        {
            foreach (var item in experienceDetails)
            {
                var fromDate = item.TimePeriod.Split('-')[0];
                var toDate = item.TimePeriod.Split('-')[1];
                item.FromDate = Convert.ToDateTime(fromDate);
                item.ToDate = Convert.ToDateTime(toDate);
            }
            var employeeId = ((UserAccount)Session[Constants.SESSION_OBJ_USER]).RefEmployeeId;
            var result = usrManagement.EditEmployeeExperienceDetailsAsync(experienceDetails, employeeId);
            return Json(new { result = result });
        }

    }
}