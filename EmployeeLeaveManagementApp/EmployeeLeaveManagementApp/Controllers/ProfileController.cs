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
            Logger.Info("Entering in ProfileController APP EditEmployeeDetails method");
            try
            {
                var result =await usrManagement.EditEmployeeDetailsAsync(model);
                Logger.Info("Successfully exiting from ProfileController APP EditEmployeeDetails method");
                return Json(new { result=result});
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController APP EditEmployeeDetails method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> EditEmployeeEducationDetails(List<EmployeeEducationDetails> educationDetails)
        {
            Logger.Info("Entering in ProfileController APP EditEmployeeEducationDetails method");
            try
            {
                foreach (var item in educationDetails)
            {
                var fromDate = item.TimePeriod.Split('-')[0];
                var toDate = item.TimePeriod.Split('-')[1];
                item.FromDate = Convert.ToDateTime(fromDate);
                item.ToDate = Convert.ToDateTime(toDate);
            }
                var employeeId = ((UserAccount)Session[Constants.SESSION_OBJ_USER]).RefEmployeeId;
                var result = await usrManagement.EditEmployeeEducationDetailsAsync(educationDetails,employeeId);
                Logger.Info("Successfully exiting from ProfileController APP EditEmployeeEducationDetails method");
                return Json(new { result = result });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController APP EditEmployeeEducationDetails method.", ex);
                return null;
            }
        }



        public async Task<JsonResult> EditEmployeeExperienceDetails(List<EmployeeExperienceDetails> experienceDetails)
        {
            Logger.Info("Entering in ProfileController APP EditEmployeeExperienceDetails method");
            try
            {
                foreach (var item in experienceDetails)
            {
                var fromDate = item.TimePeriod.Split('-')[0];
                var toDate = item.TimePeriod.Split('-')[1];
                item.FromDate = Convert.ToDateTime(fromDate);
                item.ToDate = Convert.ToDateTime(toDate);
            }
                var employeeId = ((UserAccount)Session[Constants.SESSION_OBJ_USER]).RefEmployeeId;
                var result = await usrManagement.EditEmployeeExperienceDetailsAsync(experienceDetails, employeeId);
                Logger.Info("Successfully exiting from ProfileController APP EditEmployeeExperienceDetails method");
                return Json(new { result = result });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController APP EditEmployeeExperienceDetails method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> EditEmployeeSkills(List<EmployeeSkillDetails> skills)
        {
            Logger.Info("Entering in ProfileController APP EditEmployeeSkills method");
            try
            {
                var employeeId = ((UserAccount)Session[Constants.SESSION_OBJ_USER]).RefEmployeeId;
                var result = await usrManagement.EditEmployeeSkillsAsync(skills, employeeId);
                Logger.Info("Successfully exiting from ProfileController APP EditEmployeeSkills method");
                return Json(new { result = result });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController APP EditEmployeeSkills method.", ex);
                return null;
            }
        }
    }
}