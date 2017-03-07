using LMS_WebAPP_Domain;
using LMS_WebAPP_ServiceHelpers;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public async Task<ActionResult> AddEmployeeDetails()
        {
            Logger.Info("Entering in HRController APP AddEmployeeDetails method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var model = new List<EmployeeDetailsModel>();
                    model = await hrOperations.GetManagerListAsync((Int32)HierarchyLevel.Level5);
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

        public async Task<ActionResult> GetManagerList(int refDesignation)
        {
            Logger.Info("Entering in HRController APP GetManagerList method");
            try
            {
                var model = new List<EmployeeDetailsModel>();
                var refLevel = (Int32)HierarchyLevel.Level5;
                if (refDesignation == (Int32)EmployeeRole.CEO)
                {
                    refLevel = (Int32)HierarchyLevel.Level0;
                }
                else if (refDesignation == (Int32)EmployeeRole.COO || refDesignation == (Int32)EmployeeRole.CTO || refDesignation == (Int32)EmployeeRole.SeniorHR || refDesignation == (Int32)EmployeeRole.HR)
                {
                    refLevel = (Int32)HierarchyLevel.Level1;
                }
                else if (refDesignation == (Int32)EmployeeRole.TeamLead || refDesignation == (Int32)EmployeeRole.TechLead || refDesignation == (Int32)EmployeeRole.TestLead || refDesignation == (Int32)EmployeeRole.TechnicalArchitect || refDesignation == (Int32)EmployeeRole.Manager || refDesignation == (Int32)EmployeeRole.ProjectManager || refDesignation == (Int32)EmployeeRole.DevLead)
                {
                    refLevel = (Int32)HierarchyLevel.Level2;
                }
                else if (refDesignation == (Int32)EmployeeRole.SeniorTestEngineer || refDesignation == (Int32)EmployeeRole.SeniorUIDesigner || refDesignation == (Int32)EmployeeRole.SSE)
                {
                    refLevel = (Int32)HierarchyLevel.Level3;
                }
                else if (refDesignation == (Int32)EmployeeRole.TestEngineer || refDesignation == (Int32)EmployeeRole.UIDesigner || refDesignation == (Int32)EmployeeRole.SoftwareEngineer || refDesignation == (Int32)EmployeeRole.QA || refDesignation == (Int32)EmployeeRole.Finance || refDesignation == (Int32)EmployeeRole.AssociateTechArchitect || refDesignation == (Int32)EmployeeRole.Sales)
                {
                    refLevel = (Int32)HierarchyLevel.Level4;
                }
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

        public async Task<JsonResult> GetProjectsList(int managerId=0)
        {
            Logger.Info("Entering in HRController APP GetProjectsList method");
            try
            {
                var model = new List<ProjectsList>();
                model = await hrOperations.GetProjectsListAsync(managerId);
                Logger.Info("Successfully exiting from HRController APP GetProjectsList method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetProjectsList method.", ex);
                return null;
            }

        }

        public async Task<bool> CheckEmployeeNumber(string empNumber)
        {
            Logger.Info("Entering in HRController APP CheckEmployeeNumber method");
            try
            {
                var result = await hrOperations.CheckEmployeeNumberAsync(empNumber);
                Logger.Info("Successfully exiting from HRController APP CheckEmployeeNumber method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP CheckEmployeeNumber method.", ex);
                return false;
            }

        }

        public async Task<bool> CheckEmployeeMail(string employeeMailid)
        {
            Logger.Info("Entering in HRController APP CheckEmployeeMail method");
            try
            {
                var result = await hrOperations.CheckEmployeeMailAsync(employeeMailid);
                Logger.Info("Successfully exiting from HRController APP CheckEmployeeMail method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP CheckEmployeeMail method.", ex);
                return false;
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

        public async Task<ActionResult> AddCompanyAnnouncements(string title, string carouselContent, string imagePath)
        {
            Logger.Info("Entering in HRController APP AddCompanyAnnouncements method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var result = await hrOperations.AddCompanyAnnouncementsAsync(title, carouselContent, imagePath);
                    Logger.Info("Successfully exiting from HRController APP AddCompanyAnnouncements method");
                    return Json(new { result = result });
                }
                else
                {
                    Logger.Info("Successfully exiting from HRController APP AddCompanyAnnouncements method");
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP AddCompanyAnnouncements method.", ex);
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
        public async Task<JsonResult> AddNewMasterDataValues(int masterDataType, string masterDataValue)
        {
            Logger.Info("Entering in HRController APP AddNewMasterDataValues method");
            try
            {
                var result = await hrOperations.AddNewMasterDataValuesAsync(masterDataType, masterDataValue);
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
                var userdata = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                LMS_WebAPP_Domain.EmployeeDetailsModel empModel = new LMS_WebAPP_Domain.EmployeeDetailsModel();
                empModel.FirstName = model.FirstName;
                empModel.LastName = model.LastName;
                empModel.DateOfBirth = model.DateOfBirth;
                empModel.Email = model.Email;
                empModel.RefRoleId = model.RoleId;
                empModel.DateOfJoining = model.DateOfJoining;
                empModel.City = model.City;
                empModel.Country = model.Country;
                empModel.ImagePath = model.Imagepath;
                empModel.EmployeeNumber = model.EmployeeNumber;
               // empModel.RefHierarchyLevel = model.RefHierarchyLevel;
                empModel.ManagerId = model.ManagerId;
                empModel.Telephone = model.Telephone;
                empModel.Gender = model.Gender;
                empModel.BloodGroup = model.BloodGroup;
                empModel.PassportNumber = model.PassportNumber;
                empModel.DateOfConfirmation = model.DateOfConfirmation;
                empModel.EmployeeConractType = model.EmployeeConractType;
                empModel.InfrrdEmailId = model.InfrrdEmailId;
                empModel.EmployeeType = model.EmployeeType;
                empModel.RefProfileType = model.RefProfileType;
                List<EmployeeEducationDetails> eEduList = new List<EmployeeEducationDetails>();
                foreach (var item in model.EmployeeEducation)
                {
                    if (!string.IsNullOrEmpty(item.Institution))
                    {
                        EmployeeEducationDetails eEduDetails = new EmployeeEducationDetails();
                        eEduDetails.Degree = item.Graduation;
                        eEduDetails.Institution = item.Institution;
                        eEduDetails.TimePeriod = item.FromDate + "~" + item.ToDate;
                        eEduDetails.Specialization = item.Specialization;
                        eEduList.Add(eEduDetails);
                    }
                }
                empModel.EmployeeEducationDetails = eEduList;

                List<EmployeeWorkLocationDetail> eWorkLocationList = new List<EmployeeWorkLocationDetail>();
                foreach (var item in model.EmployeeWorkLocationDetail)
                {
                        EmployeeWorkLocationDetail eWorkLocationDetails = new EmployeeWorkLocationDetail();
                    eWorkLocationDetails.Country = item.Country;
                    eWorkLocationDetails.State = item.State;
                    eWorkLocationDetails.City = item.City;
                    eWorkLocationDetails.Facility = item.Facility;
                    eWorkLocationDetails.RefCreatedBy = userdata.RefEmployeeId;
                    eWorkLocationList.Add(eWorkLocationDetails);
                }
                empModel.EmployeeWorkLocationDetail = eWorkLocationList;

                List<EmployeePermanentAddressDetail> ePermanentAddressList = new List<EmployeePermanentAddressDetail>();
                foreach (var item in model.EmployeePermanentAddressDetail)
                {
                    EmployeePermanentAddressDetail ePermanentAddressDetails = new EmployeePermanentAddressDetail();
                    ePermanentAddressDetails.Country = item.Country;
                    ePermanentAddressDetails.State = item.State;
                    ePermanentAddressDetails.City = item.City;
                    ePermanentAddressDetails.Address = item.Address;
                    ePermanentAddressDetails.RefCreatedBy = userdata.RefEmployeeId;
                    ePermanentAddressList.Add(ePermanentAddressDetails);
                }
                empModel.EmployeePermanentAddressDetail = ePermanentAddressList;

                List<EmployeeCurrentAddressDetail> eCurrentAddressList = new List<EmployeeCurrentAddressDetail>();
                foreach (var item in model.EmployeeCurrentAddressDetail)
                {
                    EmployeeCurrentAddressDetail eCurrentAddressDetails = new EmployeeCurrentAddressDetail();
                    eCurrentAddressDetails.Country = item.Country;
                    eCurrentAddressDetails.State = item.State;
                    eCurrentAddressDetails.City = item.City;
                    eCurrentAddressDetails.Address = item.Address;
                    eCurrentAddressDetails.RefCreatedBy = userdata.RefEmployeeId;
                    eCurrentAddressList.Add(eCurrentAddressDetails);
                }
                empModel.EmployeeCurrentAddressDetail = eCurrentAddressList;

                List<EmployeeEmergencyContactDetail> eEmergencyContactList = new List<EmployeeEmergencyContactDetail>();
                foreach (var item in model.EmployeeEmergencyContactDetail)
                {
                    EmployeeEmergencyContactDetail eEmergencyContactDetails = new EmployeeEmergencyContactDetail();
                    eEmergencyContactDetails.Name = item.Name;
                    eEmergencyContactDetails.Relationship = item.Relationship;
                    eEmergencyContactDetails.Telephone = item.Telephone;
                    eEmergencyContactDetails.Country = item.Country;
                    eEmergencyContactDetails.State = item.State;
                    eEmergencyContactDetails.City = item.City;
                    eEmergencyContactDetails.AddressLine1 = item.AddressLine1;
                    eEmergencyContactDetails.AddressLine2 = item.AddressLine2;
                    eEmergencyContactDetails.RefCreatedBy = userdata.RefEmployeeId;
                    eEmergencyContactList.Add(eEmergencyContactDetails);
                }
                empModel.EmployeeEmergencyContactDetail = eEmergencyContactList;

                List<EmployeeExperienceDetails> eExpList = new List<EmployeeExperienceDetails>();
                foreach (var item in model.EmployeeExperience)
                {
                    if (!string.IsNullOrEmpty(item.CompanyName))
                    {
                        EmployeeExperienceDetails eExpDetails = new EmployeeExperienceDetails();
                        eExpDetails.Company = item.CompanyName;
                        eExpDetails.Role = item.Role;
                        eExpDetails.TimePeriod = item.FromDate + "~" + item.ToDate;
                        eExpList.Add(eExpDetails);
                    }
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
                List<ProjectsList> projects = new List<ProjectsList>();
                foreach (var item in model.Projects)
                {
                    if (item.Id!=0)
                    {
                        ProjectsList project = new ProjectsList();
                        project.Id = item.Id;
                        projects.Add(project);
                    }
                }
                
                empModel.Projects = projects;
                var data = await hrOperations.SubmitEmployeeDetailsAsync(empModel);
                return Json(new { result = data });

                //var data = await hrOperations.SubmitEmployeeDetailsAsync(empModel);
                //    Logger.Info("Successfully exiting from HRController APP SubmitEmployeeDetails method");
                //    return View();
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP SubmitEmployeeDetails method.", ex);
                return View("Error");
            }
        }

        public async Task<ActionResult> GenerateReports(string employeeId, string leaveType, int exportAs, string fromDate, string toDate)
        {
            Logger.Info("Entering in HRController APP GenerateReports method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var model = new List<ConsolidatedEmployeeLeaveDetailsModel>();
                    var data = Array.ConvertAll(employeeId.TrimEnd(':').Split(':'), int.Parse);
                    var leaveData = Array.ConvertAll(leaveType.TrimEnd(':').Split(':'), int.Parse);
                    var detailsList = new List<DetailedLeaveReport>();
                    model = await hrOperations.GenerateReportsAsync(employeeId, fromDate, toDate);
                    detailsList = model[0].DetailedLeaveReports;

                    string filterValue = String.Empty;
                    var filtersList = new List<ExcelDownloadFilterList>();
                    if (leaveData.Count() >= 1 && leaveData[0] != 0)
                    {
                        foreach (var item in leaveData)
                        {
                            filterValue = filterValue + ((ReportType)item).Description() + ",";
                        }
                        filterValue = filterValue.TrimEnd(',');
                    }
                    else
                    {
                        foreach (var value in Enum.GetValues(typeof(ReportType)).Cast<ReportType>().Select(v => v.Description()).ToList())
                        {
                            filterValue = filterValue + value + ", ";
                        }
                        filterValue = filterValue.Substring(0, filterValue.LastIndexOf(", "));
                    }


                    if (!String.IsNullOrEmpty(filterValue))
                    {
                        filtersList.Add(new ExcelDownloadFilterList()
                        {
                            FilterType = "Leave Type",
                            FilterValue = filterValue
                        });
                    }
                    string include = "RefEmployeeId,EmployeeName,";
                    if (leaveData[0] == 0)
                    {
                        include += "AppliedLeavesCount,WorkFromHomeCount,LossofPayCount,CompOffCount,AdvancedLeavesCount";
                    }
                    else if (leaveData[0] != 0)
                    {
                        foreach (var item in leaveData)
                        {
                            switch (item)
                            {

                                case 1:
                                    include += "AppliedLeavesCount,";
                                    break;
                                case 2:
                                    include += "WorkFromHomeCount,";
                                    break;
                                case 3:
                                    include += "LossofPayCount,";
                                    break;
                                case 4:
                                    include += "CompOffCount,";
                                    break;
                                case 5:
                                    include += "AdvancedLeavesCount,";
                                    break;
                            }
                        }
                    }
                    include = include.TrimEnd(',');
                    var file = CommonMethods.CreateDownloadExcel(model, detailsList, include, "", "Report", "Leave Report", filtersList);

                    if (exportAs == 1)
                    {
                        return File(file.GetBuffer(), "application/vnd.ms-excel", "LeaveReport_" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + ".xls");
                    }
                    else
                    {
                        CommonMethods.SendMailWithMultipleAttachments(ConfigurationManager.AppSettings["HRMailId"], true, "Leave Report", "", file, "LeaveReportFile_" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year);
                        return Json(new { result = true });
                    }
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch(Exception ex)
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

        public async Task<JsonResult> AddNewProjectInfo(string projectName, string description, List<string> technologies, List<string> technologyDescriptions, DateTime startDate, int refManager)
        {
            Logger.Info("Entering in HRController APP AddNewProjectInfo method");
            try
            {
                LMS_WebAPP_Domain.ProjectDetails projectModel = new LMS_WebAPP_Domain.ProjectDetails();

                //projectModel.ProjectName = model.ProjectName;
                //projectModel.Description = model.Description;
                //projectModel.startDate = model.StartDate;
                //projectModel.RefManager = model.RefManager;

                //List<TechnologyDetails> technologies = new List<TechnologyDetails>();
                //foreach (var item in model.Technologies)
                //{

                //        TechnologyDetails technologyDetail = new TechnologyDetails();
                //      //  technologyDetail.Id = item.Id;
                //       // technologyDetail.Technology = item.Technology;
                //        technologies.Add(technologyDetail);
                    
                //}
                //projectModel.Technologies = technologies;

                //List<TechnologyDescriptions> technologyDescriptionList = new List<TechnologyDescriptions>();
                //foreach (var item in model.TechnologyDetails)
                //{

                //    TechnologyDescriptions technologyDescriptions = new TechnologyDescriptions();
                //    //technologyDescriptions.Id = item.Id;
                //    //technologyDescriptions.RefTechnology = item.RefTechnology;
                //    //technologyDescriptions.TechnologyDetails = item.TechnologyDetails;
                //    technologyDescriptionList.Add(technologyDescriptions);

                //}
                //projectModel.TechnologyDetails = technologyDescriptionList;
                
                var data = await hrOperations.AddNewProjectInfoAsync(projectName, description, technologies,technologyDescriptions, startDate, refManager);
                Logger.Info("Successfully exiting from HRController APP AddNewProjectInfo method");
                return Json(new { result = data });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP AddNewProjectInfo method.", ex);
                return null;
            }
        }


        public async Task<ActionResult> GenerateIndividualReport(int employeeId)
        {
            Logger.Info("Entering in HRController APP GenerateIndividualReport method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var detailsList = new List<DetailedLeaveReport>();
                    string include = "RefEmployeeId,EmployeeName,AppliedLeavesCount,WorkFromHomeCount,LossofPayCount,CompOffCount,AdvancedLeavesCount";

                    var filtersList = new List<ExcelDownloadFilterList>();
                    var model = await hrOperations.GenerateIndividualReportAsync(employeeId);
                    var file = CommonMethods.CreateDownloadExcel(model, detailsList, include, "", "Report", "Individual Leave Report", null);

                    return File(file.GetBuffer(), "application/vnd.ms-excel", "LeaveReport_" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + ".xls");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch(Exception ex)
            {
                Logger.Error("Error at HRController APP GenerateIndividualReport method.", ex);
                return View("Error");
            }
        }

        public async Task<JsonResult> GetSkillsList()
        {
            Logger.Info("Entering in HRController APP GetSkillsList method");
            try
            {
                var model = new List<EmployeeSkillDetails>();
                model = await hrOperations.GetSkillsListAsync();
                Logger.Info("Successfully exiting from HRController APP GetSkillsList method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetSkillsList method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> GetCountries()
        {
            Logger.Info("Entering in HRController APP GetCountries method");
            try
            {
                var model = new List<CountryDetails>();
                model = await hrOperations.GetCountriesAsync();
                Logger.Info("Successfully exiting from HRController APP GetCountries method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetCountries method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> GetRelationships()
        {
            Logger.Info("Entering in HRController APP GetRelationships method");
            try
            {
                var model = new List<RelationshipDetails>();
                model = await hrOperations.GetRelationshipsAsync();
                Logger.Info("Successfully exiting from HRController APP GetRelationships method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetRelationships method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> GetWorkFacilities()
        {
            Logger.Info("Entering in HRController APP GetWorkFacilities method");
            try
            {
                var model = new List<FacilityDetails>();
                model = await hrOperations.GetFacilitiesAsync();
                Logger.Info("Successfully exiting from HRController APP GetWorkFacilities method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetWorkFacilities method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> GetStates(int CountryId)
        {
            Logger.Info("Entering in HRController APP GetStates method");
            try
            {
                var model = new List<StateDetails>();
                model = await hrOperations.GetStatesAsync(CountryId);
                Logger.Info("Successfully exiting from HRController APP GetStates method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetStates method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> GetWorkFacilityDetails(int FacilityId)
        {
            Logger.Info("Entering in HRController APP GetWorkFacilityDetails method");
            try
            {
                var model = new FacilityDetails();
                model = await hrOperations.GetWorkFacilityDetailsAsync(FacilityId);
                Logger.Info("Successfully exiting from HRController APP GetWorkFacilityDetails method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetWorkFacilityDetails method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> GetCities(int StateId)
        {
            Logger.Info("Entering in HRController APP GetCities method");
            try
            {
                var model = new List<CityDetails>();
                model = await hrOperations.GetCitiesAsync(StateId);
                Logger.Info("Successfully exiting from HRController APP GetCities method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetCities method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> GetFacilities(int CityId)
        {
            Logger.Info("Entering in HRController APP GetFacilities method");
            try
            {
                var model = new List<FacilityDetails>();
                model = await hrOperations.GetFacilitiesAsync(CityId);
                Logger.Info("Successfully exiting from HRController APP GetFacilities method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetFacilities method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> GetTechnologiesList()
        {
            Logger.Info("Entering in HRController APP GetTechnologiesList method");
            try
            {
                var model = new List<TechnologyDetails>();
                model = await hrOperations.GetTechnologiesListAsync();
                Logger.Info("Successfully exiting from HRController APP GetTechnologiesList method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetTechnologiesList method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> GetTechnologyDetailsList(List<TechnologyDetails> technologies)
        {
            Logger.Info("Entering in HRController APP GetTechnologyDetailsList method");
            try
            {
                var model = new List<TechnologyDescriptions>();
                model = await hrOperations.GetTechnologyDetailsListAsync(technologies);
                Logger.Info("Successfully exiting from HRController APP GetTechnologyDetailsList method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetTechnologyDetailsList method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> CheckForExistingMasterDataValues(int masterDataType, string masterDataValue)
        {
            Logger.Info("Entering in HRController APP CheckForExistingMasterDataValues method");
            try
            {
                var result = await hrOperations.CheckForExistingMasterDataValuesAsync(masterDataType, masterDataValue);
                Logger.Info("Successfully exiting from HRController APP CheckForExistingMasterDataValues method");
                return Json(new { result = result });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP CheckForExistingMasterDataValues method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> CheckForExistingProjectMasterDataValues(string projectName, string technology,int refManager)
        {
            Logger.Info("Entering in HRController APP CheckForExistingProjectMasterDataValues method");
            try
            {
                var result = await hrOperations.CheckForExistingProjectMasterDataValuesAsync(projectName, technology,refManager);
                Logger.Info("Successfully exiting from HRController APP CheckForExistingProjectMasterDataValues method");
                return Json(new { result = result });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP CheckForExistingProjectMasterDataValues method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> GetRolesList()
        {
            Logger.Info("Entering in HRController APP GetRolesList method");
            try
            {
                var model = new List<MasterDataModel>();
                model = await hrOperations.GetRolesListAsync();
                Logger.Info("Successfully exiting from HRController APP GetRolesList method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetRolesList method.", ex);
                return null;
            }

        }

        public async Task<JsonResult> GetProjectwiseReport(int projectId,int fromMonth,int toMonth,int year)
        {
            Logger.Info("Entering in HRController APP GetProjectwiseReport method");
            try
            {
               var model = await hrOperations.GetProjectwiseReportAsync(projectId,0,0,year);
                Logger.Info("Successfully exiting from GetProjectwiseReport APP GetRolesList method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetProjectwiseReport method.", ex);
                return null;
            }

        }
        public async Task<JsonResult> GetProjectwiseEmployeeDetails(int projectId, int fromMonth, int toMonth, int year)
        {
            Logger.Info("Entering in HRController APP GetProjectwiseEmployeeDetails method");
            try
            {
                var model = await hrOperations.GetProjectwiseEmployeeDetailsAsync(projectId, 0, 0, year);
                Logger.Info("Successfully exiting from GetProjectwiseEmployeeDetails APP GetRolesList method");
                return Json(new { data = model });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GetProjectwiseEmployeeDetails method.", ex);
                return null;
            }

        }

        public async Task<ActionResult> GenerateProjectwiseReport(int projectId,int fromMonth,int toMonth,int year)
        {
            Logger.Info("Entering in HRController APP GenerateIndividualReport method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var detailsList = new List<DetailedLeaveReport>();
                    string include = "ProjectName,EmployeeName,StartDate,EndDate";

                    var filtersList = new List<ExcelDownloadFilterList>();
                    var model = await hrOperations.GetProjectwiseEmployeeDetailsAsync(projectId,fromMonth,toMonth,year);
                    var file = CommonMethods.CreateDownloadExcel(model, detailsList, include, "", "Report", "Individual Leave Report", null);

                    return File(file.GetBuffer(), "application/vnd.ms-excel", "ProjectwiseReport" + DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year + ".xls");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController APP GenerateIndividualReport method.", ex);
                return View("Error");
            }
        }

    }
}