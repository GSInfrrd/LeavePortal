using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Domain;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class EmployeeLeaveTransController : ApiController
    {

        // GET api/values
        public List<EmployeeLeaveTransactionModel> Get(int id)
        {
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
            var res=ELTM.GetEmployeeLeaveTransaction(id);

            return res;
            
        }

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        public List<EmployeeLeaveTransactionModel> Get(int id, int leaveType, string fromDate, string toDate, string comments, int workingDays)
        {
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
            var detailsInserted = ELTM.InsertEmployeeLeaveDetails(id,leaveType,fromDate,toDate,comments,workingDays);
            var res = new List<EmployeeLeaveTransactionModel>();
            if(detailsInserted)
            {
                res = ELTM.GetEmployeeLeaveTransaction(id);
            }
            return res;
        }

        public List<EmployeeLeaveTransactionModel> Get(int id, bool status)
        {
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
            var detailsInserted = ELTM.SubmitLeaveForApproval(id);
            var res = new List<EmployeeLeaveTransactionModel>();
            if (detailsInserted)
            {
                res = ELTM.GetEmployeeLeaveTransaction(id);
            }
            return res;
        }

        public List<EmployeeLeaveTransactionModel> Get(int leaveId,int employeeId)
        {
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
            var detailsInserted = ELTM.DeleteLeaveRequest(leaveId);
            var res = new List<EmployeeLeaveTransactionModel>();
            if (detailsInserted)
            {
                res = ELTM.GetEmployeeLeaveTransaction(employeeId);
            }
            return res;
        }

    }
}
