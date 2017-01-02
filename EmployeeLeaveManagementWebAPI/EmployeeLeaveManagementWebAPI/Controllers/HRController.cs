using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using System;
using System.Collections.Generic;
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
        public List<EmployeeDetailsModel> GetManagerList(int refLevel)
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

        [HttpGet]
        public List<EmployeeDetailsModel> GenerateReports(int employeeId, int leaveType, int exportAs)
        {
            try
            {
                var empData = hrOperations.GetReportData(employeeId,leaveType,exportAs);
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

    }
}
