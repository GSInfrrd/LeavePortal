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
        [Route("EditEmployeeEmergencyContactDetails")]
        public bool EditEmployeeEmergencyContactDetails(EmployeeEmergencyContactDetail model)
        {
            try
            {
                Logger.Info("Entering in ProfileController API EditEmployeeEmergencyContactDetails method");
                var result = userManager.EditEmployeeEmergencyContactDetails(model);
                Logger.Info("Successfully exiting from ProfileController API EditEmployeeEmergencyContactDetails method");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController API EditEmployeeEmergencyContactDetails method.", ex);
                return false;
            }
        }

        [HttpPost]
        [Route("EditEmployeeCurrentAddressDetails")]
        public bool EditEmployeeCurrentAddressDetails(EmployeeCurrentAddressDetail model)
        {
            try
            {
                Logger.Info("Entering in ProfileController API EditEmployeeCurrentAddressDetails method");
                var result = userManager.EditEmployeeCurrentAddressDetails(model);
                Logger.Info("Successfully exiting from ProfileController API EditEmployeeCurrentAddressDetails method");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController API EditEmployeeCurrentAddressDetails method.", ex);
                return false;
            }
        }

        [HttpPost]
        [Route("EditEmployeePermanentAddressDetails")]
        public bool EditEmployeePermanentAddressDetails(EmployeePermanentAddressDetail model)
        {
            try
            {
                Logger.Info("Entering in ProfileController API EditEmployeePermanentAddressDetails method");
                var result = userManager.EditEmployeePermanentAddressDetails(model);
                Logger.Info("Successfully exiting from ProfileController API EditEmployeePermanentAddressDetails method");
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ProfileController API EditEmployeePermanentAddressDetails method.", ex);
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

        //[HttpPost]
        //[Route("EditEmployeeExperienceDetailsAndroid")]
        //public bool EditEmployeeExperienceDetailsAndroid(List<EmployeeExperienceDetails> experienceDetails, int employeeId)
        //{
        //    try
        //    {
        //        Logger.Info("Entering in ProfileController API EditEmployeeExperienceDetails method");
        //        foreach (EmployeeExperienceDetails employeeExperienceDetails in experienceDetails)
        //        {
        //            DateTime fromDate = new DateTime(1970, 1, 1).AddMilliseconds(employeeExperienceDetails.fromDateLong);
        //            fromDate = fromDate.ToLocalTime();
        //            employeeExperienceDetails.FromDate = fromDate;
        //            DateTime toDate = new DateTime(1970, 1, 1).AddMilliseconds(employeeExperienceDetails.toDateLong);
        //            toDate = toDate.ToLocalTime();
        //            employeeExperienceDetails.ToDate = toDate;
        //        }


        //        var result = userManager.EditEmployeeExperienceDetails(experienceDetails, employeeId);
        //        Logger.Info("Successfully exiting from ProfileController API EditEmployeeExperienceDetailsAndroid method");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("Error at ProfileController API EditEmployeeExperienceDetailsAndroid method.", ex);
        //        return false;
        //    }
        //}


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
