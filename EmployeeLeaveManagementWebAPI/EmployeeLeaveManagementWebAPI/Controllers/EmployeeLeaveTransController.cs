using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class EmployeeLeaveTransController : ApiController
    {
        // GET api/values
        public List<EmployeeLeaveTransactionModel> Get(int id, int? leaveType = 0,int? month=0,int? transactionType=0)
        {
            try
            {
                Logger.Info("Entering in EmployeeLeaveTransController API Get method");
                EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
                int leaveTypeConverted = Convert.ToInt16(leaveType);
                int monthConverted = Convert.ToInt16(month);
                int transactionTypeConverted = Convert.ToInt16(transactionType);
                var res = ELTM.GetEmployeeLeaveTransaction(id, leaveTypeConverted,monthConverted,transactionTypeConverted);
                Logger.Info("Successfully exiting from EmployeeLeaveTransController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at EmployeeLeaveTransController API Get method.", ex);
                return null;
            }
        }

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        public List<EmployeeLeaveTransactionModel> Get(int id, int leaveType, string fromDate, string toDate, string comments, double workingDays)
        {
            try
            {
                Logger.Info("Entering in EmployeeLeaveTransController API Get method");
                EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
                var detailsInserted = ELTM.InsertEmployeeLeaveDetails(id, leaveType, fromDate, toDate, comments, workingDays);
                var res = new List<EmployeeLeaveTransactionModel>();
                Logger.Info("Successfully exiting from EmployeeLeaveTransController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at EmployeeLeaveTransController API Get method.", ex);
                return null;
            }
        }

        public List<EmployeeLeaveTransactionModel> Get(int id, bool status)
        {
            try
            {
                Logger.Info("Entering in EmployeeLeaveTransController API Get method");
                EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
                var detailsInserted = ELTM.SubmitLeaveForApproval(id);
                var res = new List<EmployeeLeaveTransactionModel>();
               if (detailsInserted)
               {
                   res = ELTM.GetEmployeeLeaveTransaction(id);
               }
                Logger.Info("Successfully exiting from EmployeeLeaveTransController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at EmployeeLeaveTransController API Get method.", ex);
                return null;
            }
        }

        public List<EmployeeLeaveTransactionModel> Get(int leaveId, int employeeId)
        {
            try
            {
                Logger.Info("Entering in EmployeeLeaveTransController API Get method");
                EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
                var detailsInserted = ELTM.DeleteLeaveRequest(leaveId);
                var res = new List<EmployeeLeaveTransactionModel>();
                if (detailsInserted)
               {
                  res = ELTM.GetEmployeeLeaveTransaction(employeeId);
               }
                Logger.Info("Successfully exiting from EmployeeLeaveTransController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at EmployeeLeaveTransController API Get method.", ex);
                return null;
            }
        }
    }
}
