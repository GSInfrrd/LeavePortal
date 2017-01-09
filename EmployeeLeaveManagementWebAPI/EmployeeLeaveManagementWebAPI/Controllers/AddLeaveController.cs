using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class AddLeaveController : ApiController
    {
        //GET: AddLeave
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public List<EmployeeLeaveTransactionModel> GetLeaveType(int id)
        {
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
            var res = ELTM.GetEmployeeLeaveTransaction(id);

            return res;

        }
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public bool CheckLeaveAvailability(int employeeId,bool status, DateTime fromDate, DateTime toDate)
        {
            AddLeaveManagement addLeaveMgt = new AddLeaveManagement();
            var result = addLeaveMgt.CheckLeaveAvailability(employeeId,fromDate,toDate);
            return true;  
        }
    }
}