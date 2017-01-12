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
        public MemoryStream GenerateReports(string fromDate, string toDate, string employeeId, string leaveType)
        {
            try
            {
                Logger.Info("Entering in HRController API GenerateReports method");
                var detailsList = new List<DetailedLeaveReport>();
                var data = Array.ConvertAll(employeeId.TrimEnd(':').Split(':'), int.Parse);
                var leaveData = Array.ConvertAll(leaveType.TrimEnd(':').Split(':'), int.Parse);
                var empData = hrOperations.GetReportData(fromDate, toDate, data.ToList(), out detailsList);
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

                var file = CommonMethods.CreateDownloadExcel(empData, detailsList, include, "", "Report", "Leave Report", filtersList);
                // return File(file.GetBuffer(), "application/vnd.ms-excel", "LeaveReport.xls");
                Logger.Info("Successfully exiting from HRController API GenerateReports method");
                return file;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GenerateReports method.", ex);
                return null;
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
        public List<ProjectsList> GetProjectsList()
        {
            try
            {
                Logger.Info("Entering in HRController API GetProjectsList method");
                var result = hrOperations.GetProjectsList();
                Logger.Info("Successfully exiting from HRController API GetProjectsList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetProjectsList method.", ex);
                return null;
            }
        }
    }
}
