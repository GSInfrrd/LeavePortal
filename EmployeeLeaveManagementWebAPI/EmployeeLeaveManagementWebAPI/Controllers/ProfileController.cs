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
    [RoutePrefix("api/profile")]
    public class ProfileController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private UserManagement userManager = new UserManagement();
        [AllowAnonymous]
        [HttpGet]
        public EmployeeDetailsModel GetUserProfileDetails(int empId)
        {
            var accController = new AccountController();
            try
            {
                var empData = userManager.GetUserProfileDetails(empId);
             // empData.ImagePath = accController.GetFile(empData.ImagePath);
                if (null != empData)
                {
                    return empData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
        }

        public bool Post(EmployeeDetailsModel model)
        {
           var result = userManager.EditEmployeeDetails(model);
            return result;
        }

        [HttpPost]
        [Route("EditEmployeeEducationDetails")]
        public bool EditEmployeeEducationDetails(List<EmployeeEducationDetails> educationDetails,int employeeId)
        {
           var result = userManager.EditEmployeeEducationDetails(educationDetails,employeeId);
            return true;
        }

        [HttpPost]
        [Route("EditEmployeeExperienceDetails")]
        public bool EditEmployeeExperienceDetails(List<EmployeeExperienceDetails> experienceDetails, int employeeId)
        {
            var result = userManager.EditEmployeeExperienceDetails(experienceDetails, employeeId);
            return true;
        }
    }
}
