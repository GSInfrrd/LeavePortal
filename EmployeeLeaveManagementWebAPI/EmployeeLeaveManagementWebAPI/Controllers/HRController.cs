using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                var result = hrOperations.SubmitEmployeeDetails(model);
                Logger.Info("Successfully exiting from HRController API Post method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API Post method.", ex);
                return false;
            }
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
        public bool AddNewProjectInfo(string projectName, string description, string technology, DateTime startDate, int refManager)
        {
            try
            {
                Logger.Info("Entering in HRController API AddNewProjectInfo method");
                var empData = hrOperations.AddNewProjectInfo(projectName, description, technology, startDate, refManager);
                Logger.Info("Successfully exiting from HRController API AddNewProjectInfo method");
                return empData;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API AddNewProjectInfo method.", ex);
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

    }
}
