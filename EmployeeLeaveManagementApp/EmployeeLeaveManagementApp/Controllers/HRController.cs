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
            Logger.Info("Entering in HRController APP AddEmployeeDetails method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var model = new List<EmployeeDetailsModel>();
                model = await hrOperations.GetManagerListAsync(19);
                Logger.Info("Successfully exiting from HRController APP AddEmployeeDetails method");
                return View(model);
            }
            else
            {
                    Logger.Info("Successfully exiting from HRController APP AddEmployeeDetails method");
                    return RedirectToAction("Login", "Account");
            }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP AddEmployeeDetails method.", ex);
                return View("Error");
            }
        }

        public async Task<ActionResult> GetManagerList(int refLevel)
        {
            Logger.Info("Entering in HRController APP GetManagerList method");
            try
            {
                var model = new List<EmployeeDetailsModel>();
                model = await hrOperations.GetManagerListAsync(refLevel);
                Logger.Info("Successfully exiting from HRController APP GetManagerList method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetManagerList method.", ex);
                return View("Error");
            }

        }

        public async Task<JsonResult> GetProjectsList()
        {
            Logger.Info("Entering in HRController APP GetProjectsList method");
            try
            {
                var model = new List<ProjectsList>();
                model = await hrOperations.GetProjectsListAsync();
                Logger.Info("Successfully exiting from HRController APP GetProjectsList method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetProjectsList method.", ex);
                return null;
            }

        }

        public async Task<ActionResult> Reports()
        {
            Logger.Info("Entering in HRController APP Reports method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var model = await hrOperations.GetEmployeeListAsync();
                    Logger.Info("Successfully exiting from HRController APP Reports method");
                    return View(model);
            }
            else
            {
                    Logger.Info("Successfully exiting from HRController APP Reports method");
                    return RedirectToAction("Login", "Account");
            }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP Reports method.", ex);
                return View("Error");
            }
        }
        public ActionResult CompanyAnnouncements()
        {
            Logger.Info("Entering in HRController APP CompanyAnnouncements method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
            {
                Logger.Info("Successfully exiting from HRController APP CompanyAnnouncements method");
                return View();
            }
            else
            {
                Logger.Info("Successfully exiting from HRController APP CompanyAnnouncements method");
                return RedirectToAction("Login", "Account");
            }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP CompanyAnnouncements method.", ex);
                return View("Error");
            }
        }

        public ActionResult Broadcast()
        {
            Logger.Info("Entering in HRController APP Broadcast method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
            {
                Logger.Info("Successfully exiting from HRController APP Broadcast method");
                return View();
            }
            else
            {
                Logger.Info("Successfully exiting from HRController APP Broadcast method");
                return RedirectToAction("Login", "Account");
            }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP Broadcast method.", ex);
                return View("Error");
            }
        }

        public ActionResult AddMasterData()
        {
            Logger.Info("Entering in HRController APP AddMasterData method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
            {
                    Logger.Info("Successfully exiting from HRController APP AddMasterData method");
                    return View();
            }
            else
            {
                    Logger.Info("Successfully exiting from HRController APP AddMasterData method");
                    return RedirectToAction("Login", "Account");
            }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP AddMasterData method.", ex);
                return View("Error");
            }
        }
        public async Task<JsonResult> AddNewMasterDataValues(int masterDataType,string masterDataValue)
        {
            Logger.Info("Entering in HRController APP AddNewMasterDataValues method");
            try
            {
                var result =await hrOperations.AddNewMasterDataValuesAsync(masterDataType, masterDataValue);
                Logger.Info("Successfully exiting from HRController APP AddNewMasterDataValues method");
                return Json(new { result = result });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP AddNewMasterDataValues method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> AddHolidays(DateTime date, string description, bool? active = true)
        {
            Logger.Info("Entering in HRController APP AddHolidays method");
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
                    Logger.Info("Successfully exiting from HRController APP AddHolidays method");
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Logger.Info("Successfully exiting from HRController APP AddHolidays method");
                    return null;
                    //set error page or login page
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP AddHolidays method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> UpdateHoliday(long Id, DateTime date, string description, bool? active = true)
        {
            Logger.Info("Entering in HRController APP UpdateHoliday method");
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
                    Logger.Info("Successfully exiting from HRController APP UpdateHoliday method");
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Logger.Info("Successfully exiting from HRController APP UpdateHoliday method");
                    return null;
                    //set error page or login page
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP UpdateHoliday method.", ex);
                return null;
            }
        }

        public async Task<ActionResult> HolidayList()
        {
            Logger.Info("Entering in HRController APP HolidayList method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var holidayList = await holidayManager.GetHolidayListAsync();
                    Logger.Info("Successfully exiting from HRController APP HolidayList method");
                    return View(holidayList);
                }
                else
                {
                    Logger.Info("Successfully exiting from HRController APP HolidayList method");
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP HolidayList method.", ex);
                return View("Error");
            }
        }

        public async Task<JsonResult> DeleteHoliday(int Id)
        {
            Logger.Info("Entering in HRController APP DeleteHoliday method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var HolidayList = await holidayManager.DeleteHolidayDetailsAsync(Id);
                    var resultJson = new { list = HolidayList };
                    Logger.Info("Successfully exiting from HRController APP DeleteHoliday method");
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Logger.Info("Successfully exiting from HRController APP DeleteHoliday method");
                    return null;
                    //set error page or login page
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP DeleteHoliday method.", ex);
                return null;
            }
        }


        public async Task<ActionResult> EmployeeDetails()
        {
            Logger.Info("Entering in HRController APP EmployeeDetails method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
            {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var model = new List<EmployeeDetailsModel>();
                    model = await hrOperations.GetEmployeeListAsync();
                    Logger.Info("Successfully exiting from HRController APP EmployeeDetails method");
                    return View(model);
            }
            else
            {
                    Logger.Info("Successfully exiting from HRController APP EmployeeDetails method");
                    return RedirectToAction("Login", "Account");
            }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP EmployeeDetails method.", ex);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> SubmitEmployeeDetails(Models.EmployeeModel model)
        {
            Logger.Info("Entering in HRController APP SubmitEmployeeDetails method");
            try
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
                Logger.Info("Successfully exiting from HRController APP SubmitEmployeeDetails method");
                return View();
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP SubmitEmployeeDetails method.", ex);
                return View("Error");
            }
        }

        public async Task<ActionResult> GenerateReports(string employeeId,string leaveType, int exportAs,string fromDate,string toDate)
        {
            Logger.Info("Entering in HRController APP GenerateReports method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var model = new MemoryStream();            

                model = await hrOperations.GenerateReportsAsync(exportAs,employeeId , leaveType,fromDate,toDate);
                if (exportAs == 1)
                {
                        Logger.Info("Successfully exiting from HRController APP GenerateReports method");
                        return File(model.GetBuffer(), "application/vnd.ms-excel", "LeaveReport_"+DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year+".xls");
                }
                else
                {
                        CommonMethods.SendMailWithMultipleAttachments("alekya@infrrd.ai", true, "Leave Report", "", model, "LeaveReportFile_"+DateTime.Now.Day+"/"+DateTime.Now.Month+"/"+DateTime.Now.Year);
                        Logger.Info("Successfully exiting from HRController APP GenerateReports method");
                        return Json(new { result = true });
                }
            }
            else
            {
                    Logger.Info("Successfully exiting from HRController APP GenerateReports method");
                    return RedirectToAction("Login", "Account");
            }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GenerateReports method.", ex);
                return View("Error");
            }
        }

        public async Task<JsonResult> GetChartDetails(int employeeId)
        {
            Logger.Info("Entering in HRController APP GetChartDetails method");
            try
            {
                var model = new ConsolidatedEmployeeLeaveDetailsModel();

            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                 model = await hrOperations.GetChartDetailsAsync(employeeId);
            
            }
                Logger.Info("Successfully exiting from HRController APP GetChartDetails method");
                return Json(new { result = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetChartDetails method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> AddNewProjectInfo(string projectName,string description,string technology,DateTime startDate,int refManager)
        {
            Logger.Info("Entering in HRController APP AddNewProjectInfo method");
            try
            {
                var model = false;

            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                 model = await hrOperations.AddNewProjectInfoAsync(projectName,description,technology,startDate,refManager);

            }
                Logger.Info("Successfully exiting from HRController APP AddNewProjectInfo method");
                return Json(new { result = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP AddNewProjectInfo method.", ex);
                return null;
            }
        }


    }
}