using LMS_WebAPP_Domain;
using LMS_WebAPP_ServiceHelpers;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class HRController : Controller
    {
        HRManagement hrOperations = new HRManagement();
        UserManagement userMgt = new UserManagement();
        HolidayManagement holidayManager = new HolidayManagement();
        // GET: HR
        public ActionResult AddEmployeeDetails()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult CompanyAnnouncements()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Broadcast()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult AddLeaves()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<JsonResult> AddHolidays(DateTime date, string Year, string description, bool active = true)
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var model = new HolidayModel()
                {
                    Date = date,
                    Year = Convert.ToInt32(Year),
                    Description = description,
                    IsActive = active
                  

                };

                var HolidayList = await holidayManager.AddNewHolidayDetailsAsync(model);

                var resultJson = new { list = HolidayList };

              return Json(resultJson, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return null;

                //set error page or login page
            }
        }


        public async Task<ActionResult> HolidayList()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var holidayList = await holidayManager.GetHolidayListAsync();
                return View(holidayList);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<ActionResult> EmployeeDetails()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                var model = new List<EmployeeDetailsModel>();
                model = await hrOperations.GetEmployeeListAsync();
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<ActionResult> SubmitEmployeeDetails(Models.EmployeeModel model)
        {
            LMS_WebAPP_Domain.EmployeeDetailsModel empModel = new LMS_WebAPP_Domain.EmployeeDetailsModel();
            empModel.FirstName = model.FirstName;
            empModel.LastName = model.LastName;
            empModel.DateOfBirth = model.DateOfBirth;
            empModel.Email = model.Email;
            empModel.RefRoleId = model.RoleId;
            empModel.Skills = model.Skills;
            empModel.DateOfJoining = model.DateOfJoining;
            empModel.City = model.City;
            empModel.Country = model.Country;
            empModel.ImagePath = "/Content/Images/EmployeeImages/" + model.Imagepath;
            empModel.EmployeeNumber = model.EmployeeNumber;
            empModel.RefHierarchyLevel = model.RefHierarchyLevel;
            empModel.ManagerName = model.ManagerName;
            empModel.Telephone = model.Telephone;
            List<EmployeeEducationDetails> eEduList = new List<EmployeeEducationDetails>();
            foreach (var item in model.EmployeeEducation)
            {
                EmployeeEducationDetails eEduDetails = new EmployeeEducationDetails();
                eEduDetails.Degree = item.Graduation;
                eEduDetails.Institution = item.Institution;
                eEduDetails.TimePeriod = item.FromDate + "~" + item.ToDate;
                eEduList.Add(eEduDetails);
            }
            empModel.EmployeeEducationDetails = eEduList;
            List<EmployeeExperienceDetails> eExpList = new List<EmployeeExperienceDetails>();
            foreach (var item in model.EmployeeExperience)
            {
                EmployeeExperienceDetails eExpDetails = new EmployeeExperienceDetails();
                eExpDetails.Company = item.CompanyName;
                eExpDetails.Role = item.Role;
                eExpDetails.TimePeriod = item.FromDate + "~" + item.ToDate;
                eExpList.Add(eExpDetails);
            }
            empModel.EmployeeExperienceDetails = eExpList;
            var data = await hrOperations.SubmitEmployeeDetailsAsync(empModel);
            return View();
        }

    }
}