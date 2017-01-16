using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
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
            try
            {
                Logger.Info("Entering in AddLeaveController API GetLeaveType method");
                EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
                var res = ELTM.GetEmployeeLeaveTransaction(id);
                Logger.Info("Successfully exiting from AddLeaveController API GetLeaveType method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AddLeaveController API GetLeaveType method.", ex);
                return null;
            }

        }
        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public LeaveTransactionResponse CheckLeaveAvailability(int employeeId,DateTime fromDate, DateTime toDate,int leaveType)
        {
            try
            {
                Logger.Info("Entering in AddLeaveController API CheckLeaveAvailability method");
                AddLeaveManagement addLeaveMgt = new AddLeaveManagement();
                var result = addLeaveMgt.CheckLeaveAvailability(employeeId,fromDate,toDate,leaveType);
                Logger.Info("Successfully exiting from AddLeaveController API CheckLeaveAvailability method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AddLeaveController API CheckLeaveAvailability method.", ex);
                return null;
            }
        }
    }
}