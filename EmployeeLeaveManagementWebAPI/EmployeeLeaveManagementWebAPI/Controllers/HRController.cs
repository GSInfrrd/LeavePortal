﻿using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Hosting;
using System.Web.Http;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    [RoutePrefix("api/HR")]
    public class HRController : ApiController
    {
        HRManagement hrOperations = new HRManagement();

        [HttpPost]
        [Route("SubmitEmployeeDetails")]
        public bool Post(EmployeeDetailsModel model)
        {
            try
            {
                Logger.Info("Entering in HRController API Post method");
                string OTP = GenerateOTP();
                int EmployeeId;
                int HrId = Convert.ToInt32(model.EmployeeEmergencyContactDetail[0].RefCreatedBy);
                var result = hrOperations.SubmitEmployeeDetails(model,OTP,out EmployeeId);

                // Send Mail 
                Thread MailThread = new Thread(() => SendMailForAddNewEmployee(OTP, EmployeeId,HrId));
                MailThread.Start();

                Logger.Info("Successfully exiting from HRController API Post method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API Post method.", ex);
                return false;
            }
        }

        private void SendMailForAddNewEmployee(string OTP, int EmployeeId, int HrId)
        {
            ActionsForMail actionName = ActionsForMail.AddNewEmployee;
            MailManagement MM = new MailManagement();
            var MailDetails = MM.GetMailTemplateForAddNewEmployee(actionName, EmployeeId, HrId);
            string TemplatePath = MailDetails.TemplatePath;

            string body;
            //Read template file from the App_Data folder
            using (var sr = new StreamReader(HostingEnvironment.MapPath(TemplatePath)))
            {
                body = sr.ReadToEnd();
            }

            var logoPath = HostingEnvironment.MapPath("~/Content/Images/infrrd-logo-main.png");
            string appurl = ConfigurationManager.AppSettings["AppURL"];

            string EmployeeName = MailDetails.EmployeeName.Substring(0, MailDetails.EmployeeName.IndexOf(" "));
            string messageBody = string.Format(body, EmployeeName, MailDetails.ToMailId, OTP, appurl);

            MailUtility.sendmail(MailDetails.ToMailId, MailDetails.CcMailId, actionName.Description(), messageBody, logoPath);
        }

        public string GenerateOTP()
        {
            char[] charArr = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            string strrandom = string.Empty;
            Random objran = new Random();
            int noofcharacters = 10;
            for (int i = 0; i < noofcharacters; i++)
            {
                //It will not allow Repetation of Characters
                int pos = objran.Next(1, charArr.Length);
                if (!strrandom.Contains(charArr.GetValue(pos).ToString()))
                    strrandom += charArr.GetValue(pos);
                else
                    i--;
            }
            return strrandom;
        }

        [HttpGet]
        [AcceptVerbs("GET")]
        [Route("")]
        public List<EmployeeDetailsModel> GetEmployeeList()
        {
            try
            {
                Logger.Info("Entering in HRController API GetEmployeeList method");
                var result = hrOperations.GetEmployeeList();
                Logger.Info("Successfully exiting from HRController API GetEmployeeList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetEmployeeList method.", ex);
                return null;
            }
        }


        [HttpGet]
        public List<EmployeeDetailsModel> GetManagerList(int refLevel, bool status)
        {
            try
            {
                Logger.Info("Entering in HRController API GetManagerList method");
                var empData = hrOperations.GetManagerList(refLevel);
                Logger.Info("Successfully exiting from HRController API GetManagerList method");
                return empData;
                
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetManagerList method.", ex);
                return null;
            }
        }

        [System.Web.Http.HttpGet]
        public List<ConsolidatedEmployeeLeaveDetailsModel> GenerateReports(string fromDate, string toDate, string employeeId)
        {
            try
            {
                var detailsList = new List<DetailedLeaveReport>();
                var data = Array.ConvertAll(employeeId.TrimEnd(':').Split(':'), int.Parse);
                var empData = hrOperations.GetReportData(fromDate, toDate, data.ToList(), out detailsList);
                empData[0].DetailedLeaveReports = detailsList;
                return empData;
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
        }

        [System.Web.Http.HttpGet]
        public ConsolidatedEmployeeLeaveDetailsModel GetChartDetails(int employeeId)
        {
            try
            {
                Logger.Info("Entering in HRController API GetChartDetails method");
                var empData = hrOperations.GetChartDetails(employeeId);
                Logger.Info("Successfully exiting from HRController API GetChartDetails method");
                return empData;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetChartDetails method.", ex);
                return null;
            }
        }


        [System.Web.Http.HttpGet]
        public bool AddNewMasterDataValues(int masterDataType, string masterDataValue)
        {
            try
            {
                Logger.Info("Entering in HRController API AddNewMasterDataValues method");
                var empData = hrOperations.AddNewMasterDataValues(masterDataType, masterDataValue);
                Logger.Info("Successfully exiting from HRController API AddNewMasterDataValues method");
                return empData;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API AddNewMasterDataValues method.", ex);
                throw;
            }
        }

        [System.Web.Http.HttpGet]
        public bool AddNewProjectInfo(string projectName, string description, string technology, string technologyDetails, DateTime startDate, int refManager)
        {
            try
            {
                Logger.Info("Entering in HRController API AddNewProjectInfo method");
                var empData = hrOperations.AddNewProjectInfo(projectName, description, technology, technologyDetails, startDate, refManager);
                Logger.Info("Successfully exiting from HRController API AddNewProjectInfo method");
                return empData;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API AddNewProjectInfo method.", ex);
                throw;
            }
        }

        [System.Web.Http.HttpGet]
        public bool AddCompanyAnnouncements(string title, string carouselContent, string imagePath)
        {
            try
            {
                Logger.Info("Entering in HRController API AddCompanyAnnouncements method");
                var AnnouncementData = hrOperations.AddCompanyAnnouncements(title, carouselContent, imagePath);
                Logger.Info("Successfully exiting from HRController API AddCompanyAnnouncements method");
                return AnnouncementData;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API AddCompanyAnnouncements method.", ex);
                throw;
            }
        }


        [HttpGet]
        [Route("GetProjectsList")]
        public List<ProjectsList> GetProjectsList(int managerId=0)
        {
            try
            {
                Logger.Info("Entering in HRController API GetProjectsList method");
                var result = hrOperations.GetProjectsList(managerId);
                Logger.Info("Successfully exiting from HRController API GetProjectsList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetProjectsList method.", ex);
                return null;
            }
        }

        [System.Web.Http.HttpGet]
        [Route("GenerateIndividualReport")]
        public List<ConsolidatedEmployeeLeaveDetailsModel> GenerateIndividualReport(int employeeId)
        {
            try
            {
                var detailsList = new List<DetailedLeaveReport>();
                var empData = new List<ConsolidatedEmployeeLeaveDetailsModel>();
                var data = hrOperations.GetChartDetails(employeeId);

                empData.Add(data);
                return empData;
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
        }

        [HttpGet]
        [Route("GetSkillsList")]
        public List<EmployeeSkillDetails> GetSkillsList()
        {
            try
            {
                Logger.Info("Entering in HRController API GetSkillsList method");
                var result = hrOperations.GetSkillsList();
                Logger.Info("Successfully exiting from HRController API GetSkillsList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetSkillsList method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetCountries")]
        public List<CountryDetails> GetCountries()
        {
            try
            {
                Logger.Info("Entering in HRController API GetCountries method");
                var result = hrOperations.GetCountries();
                Logger.Info("Successfully exiting from HRController API GetCountries method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetCountries method.", ex);
                return null;
            }
        }


        [HttpGet]
        [Route("GetRelationships")]
        public List<RelationshipDetails> GetRelationships()
        {
            try
            {
                Logger.Info("Entering in HRController API GetRelationships method");
                var result = hrOperations.GetRelationships();
                Logger.Info("Successfully exiting from HRController API GetRelationships method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetRelationships method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetBloodGroups")]
        public List<BloodGroupDetails> GetBloodGroups()
        {
            try
            {
                Logger.Info("Entering in HRController API GetBloodGroups method");
                var result = hrOperations.GetBloodGroups();
                Logger.Info("Successfully exiting from HRController API GetBloodGroups method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetBloodGroups method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetFacilities")]
        public List<FacilityDetails> GetFacilities()
        {
            try
            {
                Logger.Info("Entering in HRController API GetFacilities method");
                var result = hrOperations.GetFacilities();
                Logger.Info("Successfully exiting from HRController API GetFacilities method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetFacilities method.", ex);
                return null;
            }
        }


        [HttpGet]
        [Route("GetStates")]
        public List<StateDetails> GetStates(int CountryId)
        {
            try
            {
                Logger.Info("Entering in HRController API GetStates method");
                var result = hrOperations.GetStates(CountryId);
                Logger.Info("Successfully exiting from HRController API GetStates method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetStates method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetWorkFacilityDetails")]
        public FacilityDetails GetWorkFacilityDetails(int FacilityId)
        {
            try
            {
                Logger.Info("Entering in HRController API GetWorkFacilityDetails method");
                var result = hrOperations.GetWorkFacilityDetails(FacilityId);
                Logger.Info("Successfully exiting from HRController API GetWorkFacilityDetails method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetWorkFacilityDetails method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetCities")]
        public List<CityDetails> GetCities(int StateId)
        {
            try
            {
                Logger.Info("Entering in HRController API GetCities method");
                var result = hrOperations.GetCities(StateId);
                Logger.Info("Successfully exiting from HRController API GetCities method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetCities method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetFacilities")]
        public List<FacilityDetails> GetFacilities(int CityId)
        {
            try
            {
                Logger.Info("Entering in HRController API GetFacilities method");
                var result = hrOperations.GetFacilities(CityId);
                Logger.Info("Successfully exiting from HRController API GetFacilities method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetFacilities method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetTechnologiesList")]
        public List<TechnologyDetails> GetTechnologiesList()
        {
            try
            {
                Logger.Info("Entering in HRController API GetTechnologiesList method");
                var result = hrOperations.GetTechnologiesList();
                Logger.Info("Successfully exiting from HRController API GetTechnologiesList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetTechnologiesList method.", ex);
                return null;
            }
        }

        [HttpPost]
        [Route("GetTechnologyDetailsList")]
        public List<TechnologyDescriptions> GetTechnologyDetailsList(List<TechnologyDetails> technologies)
        {
            try
            {
                Logger.Info("Entering in HRController API GetTechnologyDetailsList method");
                var result = hrOperations.GetTechnologyDetailsList(technologies);
                Logger.Info("Successfully exiting from HRController API GetTechnologyDetailsList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetTechnologyDetailsList method.", ex);
                return null;
            }
        }

        [System.Web.Http.HttpGet]
        [Route("CheckForExistingMasterDataValues")]
        public bool CheckForExistingMasterDataValues(int masterDataType, string masterDataValue)
        {
            try
            {
                Logger.Info("Entering in HRController API CheckForExistingMasterDataValues method");
                var empData = hrOperations.CheckForExistingMasterDataValues(masterDataType, masterDataValue);
                Logger.Info("Successfully exiting from HRController API CheckForExistingMasterDataValues method");
                return empData;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API CheckForExistingMasterDataValues method.", ex);
                throw;
            }
        }

        [System.Web.Http.HttpGet]
        [Route("CheckForExistingProjectMasterDataValues")]
        public bool CheckForExistingProjectMasterDataValues(string projectName, string technology, int refManager)
        {
            try
            {
                Logger.Info("Entering in HRController API CheckForExistingProjectMasterDataValues method");
                var empData = hrOperations.CheckForExistingProjectMasterDataValues(projectName,technology,refManager);
                Logger.Info("Successfully exiting from HRController API CheckForExistingProjectMasterDataValues method");
                return empData;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API CheckForExistingProjectMasterDataValues method.", ex);
                throw;
            }
        }

        [HttpGet]
        [Route("GetRolesList")]
        public List<MasterDataModel> GetRolesList()
        {
            try
            {
                Logger.Info("Entering in HRController API GetRolesList method");
                var result = hrOperations.GetRolesList();
                Logger.Info("Successfully exiting from HRController API GetRolesList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetRolesList method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("GetProjectwiseReport")]
        public LeaveReportModel GetProjectwiseReport(int projectId, int fromMonth, int toMonth, int year)
        {
            try
            {
                Logger.Info("Entering in HRController API GetProjectwiseReport method");
                var result = hrOperations.GetProjectwiseReport(projectId,fromMonth,toMonth,year);
                Logger.Info("Successfully exiting from HRController API GetProjectwiseReport method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetProjectwiseReport method.", ex);
                return null;
            }
        }
        [HttpGet]
        [Route("GetProjectwiseEmployeeDetails")]
        public List<ProjectsList> GetProjectwiseEmployeeDetails(int projectId, int fromMonth, int toMonth, int year)
        {
            try
            {
                Logger.Info("Entering in HRController API GetProjectwiseEmployeeDetails method");
                var result = hrOperations.GetProjectwiseEmployeeDetails(projectId, fromMonth, toMonth, year);
                Logger.Info("Successfully exiting from HRController API GetProjectwiseReport method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetProjectwiseEmployeeDetails method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("CheckEmployeeNumber")]
        public bool CheckEmployeeNumber(string employeeNumber)
        {
            try
            {
                Logger.Info("Entering in HRController API CheckEmployeeNumber method");
                var result = hrOperations.CheckEmployeeNumber(employeeNumber);
                Logger.Info("Successfully exiting from HRController API CheckEmployeeNumber method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API CheckEmployeeNumber method.", ex);
                return false;
            }
        }

        [HttpGet]
        [Route("CheckEmployeeMail")]
        public bool CheckEmployeeMail(string employeeMailid)
        {
            try
            {
                Logger.Info("Entering in HRController API CheckEmployeeMail method");
                var result = hrOperations.CheckEmployeeMail(employeeMailid);
                Logger.Info("Successfully exiting from HRController API CheckEmployeeMail method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API CheckEmployeeMail method.", ex);
                return false;
            }
        }

    }
}
