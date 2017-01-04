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
    public class HRController : ApiController
    {
        HRManagement hrOperations = new HRManagement();

        public bool Post(EmployeeDetailsModel model)
        {
            var result = hrOperations.SubmitEmployeeDetails(model);
            return result;
        }

        public List<EmployeeDetailsModel> Get()
        {
            var result = hrOperations.GetEmployeeList();
            return result;
        }


        [HttpGet]
        public List<EmployeeDetailsModel> GetManagerList(int refLevel,bool status)
        {
            try
            {
                var empData = hrOperations.GetManagerList(refLevel);
                if (null != empData)
                {
                    return empData;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
        }

        [System.Web.Http.HttpGet]
        public MemoryStream GenerateReports(string fromDate,string toDate,string employeeId, string leaveType)
        {
            try
            {
                var detailsList = new List<DetailedLeaveReport>();
                var data = Array.ConvertAll(employeeId.TrimEnd(':').Split(':'), int.Parse);
                var leaveData = Array.ConvertAll(leaveType.TrimEnd(':').Split(':'), int.Parse);
                var empData = hrOperations.GetReportData(fromDate,toDate, data.ToList(),out detailsList);
                string filterValue = String.Empty;
                var filtersList = new List<ExcelDownloadFilterList>();
                if (leaveData.Count()>=1 && leaveData[0]!= 0)
                {
                    foreach(var item in leaveData)
                    { 
                    filterValue =filterValue+ ((ReportType)item).Description()+",";
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

                var file = CommonMethods.CreateDownloadExcel(empData,detailsList, include, "", "Report", "Leave Report", filtersList);
                // return File(file.GetBuffer(), "application/vnd.ms-excel", "LeaveReport.xls");

                return file;
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
                 var empData = hrOperations.GetChartDetails(employeeId);
                return empData;
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
        }



    }
}
