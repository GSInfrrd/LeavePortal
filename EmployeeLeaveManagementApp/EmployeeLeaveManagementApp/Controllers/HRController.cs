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
        public  async Task<ActionResult> AddEmployeeDetails()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var model = new List<EmployeeDetailsModel>();
                model = await hrOperations.GetManagerListAsync(19);              
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<ActionResult> GetManagerList(int refLevel)
        {
            var model = new List<EmployeeDetailsModel>();
            model = await hrOperations.GetManagerListAsync(refLevel);
            return Json(new { data = model });

        }

        public async Task<JsonResult> GetProjectsList()
        {
            var model = new List<ProjectsList>();
            model = await hrOperations.GetProjectsListAsync();
            return Json(new { data = model });

        }

        public async Task<ActionResult> Reports()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var model = await hrOperations.GetEmployeeListAsync();

                return View(model);
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

        public ActionResult AddMasterData()
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
        public async Task<JsonResult> AddNewMasterDataValues(int masterDataType,string masterDataValue)
        {
            var result =await hrOperations.AddNewMasterDataValuesAsync(masterDataType, masterDataValue);
            return Json(new { result = result });
        }

        public async Task<JsonResult> AddHolidays(DateTime date, string description, bool? active = true)
        {
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var model = new HolidayModel()
                    {
                        Date = date,
                        Year = Convert.ToInt32(date.Year),
                        Description = description,
                        IsActive = active,
                        CreatedBy = data.UserName,
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
            catch (Exception)
            {
                return null;

            }
        }

        public async Task<JsonResult> UpdateHoliday(long Id, DateTime date, string description, bool? active = true)
        {
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var model = new HolidayModel()
                    {
                        Id = Id,
                        Date = date,
                        Year = Convert.ToInt32(date.Year),
                        Description = description,
                        IsActive = active,
                        ModifiedBy = data.UserName
                    };

                    var HolidayList = await holidayManager.UpdateNewHolidayDetailsAsync(model);
                    var resultJson = new { list = HolidayList };
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                    //set error page or login page
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ActionResult> HolidayList()
        {
            try
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
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<JsonResult> DeleteHoliday(int Id)
        {
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var HolidayList = await holidayManager.DeleteHolidayDetailsAsync(Id);
                    var resultJson = new { list = HolidayList };
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                    //set error page or login page
                }
            }
            catch (Exception)
            {
                return null;
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
            empModel.DateOfJoining = model.DateOfJoining;
            empModel.City = model.City;
            empModel.Country = model.Country;
            empModel.ImagePath=model.Imagepath;
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
            List<EmployeeSkillDetails> eSkillList = new List<EmployeeSkillDetails>();
            foreach (var item in model.Skills)
            {
                EmployeeSkillDetails eSkills = new EmployeeSkillDetails();
                eSkills.SkillName = item.SkillName;
                eSkillList.Add(eSkills);
            }
            empModel.Skills = eSkillList;

            var data = await hrOperations.SubmitEmployeeDetailsAsync(empModel);
            return View();
        }

        public async Task<ActionResult> GenerateReports(string employeeId,string leaveType, int exportAs,string fromDate,string toDate)
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var model = new MemoryStream();            

                model = await hrOperations.GenerateReportsAsync(exportAs,employeeId , leaveType,fromDate,toDate);
                if (exportAs == 1)
                {
                    return File(model.GetBuffer(), "application/vnd.ms-excel", "LeaveReport_"+DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year+".xls");
                }
                else
                {
                    CommonMethods.SendMailWithMultipleAttachments("alekya@infrrd.ai", true, "Leave Report", "", model, "LeaveReportFile_"+DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year);
                    return Json(new { result = true });
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<JsonResult> GetChartDetails(int employeeId)
        {
            var model = new ConsolidatedEmployeeLeaveDetailsModel();

            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                 model = await hrOperations.GetChartDetailsAsync(employeeId);
            
            }
            return Json(new { result = model });
        }

        public async Task<JsonResult> AddNewProjectInfo(string projectName,string description,string technology,DateTime startDate,int refManager)
        {
            var model = false;

            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                 model = await hrOperations.AddNewProjectInfoAsync(projectName,description,technology,startDate,refManager);

            }
            return Json(new { result = model });
        }


    }
}