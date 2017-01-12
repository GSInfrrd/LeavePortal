using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
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
                Logger.Info("Entering in ProfileController API GetUserProfileDetails method");
                var empData = userManager.GetUserProfileDetails(empId);
                // empData.ImagePath = accController.GetFile(empData.ImagePath);
                Logger.Info("Successfully exiting from ProfileController API GetUserProfileDetails method");
                return empData;
                
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController API GetUserProfileDetails method.", ex);
                return null;
            }
        }

        public bool Post(EmployeeDetailsModel model)
        {
            try
            {
                Logger.Info("Entering in ProfileController API Post method");
                var result = userManager.EditEmployeeDetails(model);
                Logger.Info("Successfully exiting from ProfileController API Post method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController API Post method.", ex);
                return false;
            }
        }

        [HttpPost]
        [Route("EditEmployeeEducationDetails")]
        public bool EditEmployeeEducationDetails(List<EmployeeEducationDetails> educationDetails,int employeeId)
        {
            try
            {
                Logger.Info("Entering in ProfileController API EditEmployeeEducationDetails method");
                var result = userManager.EditEmployeeEducationDetails(educationDetails,employeeId);
                Logger.Info("Successfully exiting from ProfileController API EditEmployeeEducationDetails method");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController API EditEmployeeEducationDetails method.", ex);
                return false;
            }
        }

        [HttpPost]
        [Route("EditEmployeeExperienceDetails")]
        public bool EditEmployeeExperienceDetails(List<EmployeeExperienceDetails> experienceDetails, int employeeId)
        {
            try
            {
                Logger.Info("Entering in ProfileController API EditEmployeeExperienceDetails method");
                var result = userManager.EditEmployeeExperienceDetails(experienceDetails, employeeId);
                Logger.Info("Successfully exiting from ProfileController API EditEmployeeExperienceDetails method");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController API EditEmployeeExperienceDetails method.", ex);
                return false;
            }
        }


        [HttpPost]
        [Route("EditEmployeeSkills")]
        public bool EditEmployeeSkills(List<EmployeeSkillDetails> skills, int employeeId)
        {
            try
            {
                Logger.Info("Entering in ProfileController API EditEmployeeSkills method");
                var result = userManager.EditEmployeeSkills(skills, employeeId);
                Logger.Info("Successfully exiting from ProfileController API EditEmployeeSkills method");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController API EditEmployeeSkills method.", ex);
                return false;
            }
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("getProfileImage")]
        public string getProfileImage(int empId)
        {
            try
            {
                Logger.Info("Entering in ProfileController API getProfileImage method");
                var profileImage = userManager.getUserProfileImage(empId);
                Logger.Info("Successfully exiting from ProfileController API getProfileImage method");
                return profileImage;
                
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController API getProfileImage method.", ex);
                return null;
            }
        }
    }
}
