using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class AddLeaveController : Controller
    {
        //GET: AddLeave
        [AllowAnonymous]
        [HttpGet]
        public List<EmployeeLeaveTransactionModel> GetLeaveType(int id)
        {
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
            var res = ELTM.GetEmployeeLeaveTransaction(id);

            return res;

        }
    }
}