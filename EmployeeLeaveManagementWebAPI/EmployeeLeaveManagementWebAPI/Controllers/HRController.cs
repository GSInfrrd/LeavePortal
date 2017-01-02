using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;

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


        [System.Web.Http.HttpGet]
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
        public MemoryStream GenerateReports(int exportAs, int employeeId=0, int leaveType=0)
        {
            try
            {
                var empData = hrOperations.GetReportData(employeeId,leaveType);
                string filterValue = String.Empty;
                var filtersList = new List<ExcelDownloadFilterList>();
                if (leaveType == 0)
                {
                    filterValue = ((ReportType)leaveType).Description();
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
                        FilterType = "Status",
                        FilterValue = filterValue
                    });
                }

                //if (!String.IsNullOrEmpty(employeeId))
                //{
                //    filtersList.Add(new ExcelDownloadFilterList()
                //    {
                //        FilterType = "Employee ID",
                //        FilterValue = employeeId 
                //    });
                //}
                string include = "RefEmployeeId,EmployeeName,EarnedLeavesCount,AppliedLeavesCount,WorkFromHomeCount,LossofPayCount";
                var file = CommonMethods.CreateDownloadExcel(empData, include, "", "Report", "Leave Report", filtersList);
               // return File(file.GetBuffer(), "application/vnd.ms-excel", "LeaveReport.xls");

                return file;
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
        }

    }
}
