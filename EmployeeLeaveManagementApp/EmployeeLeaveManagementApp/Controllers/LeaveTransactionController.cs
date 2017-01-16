﻿using System.Net.Http;
using System.Web.Mvc;
using LMS_WebAPP_ServiceHelpers;
using System.Threading.Tasks;
using LMS_WebAPP_Domain;
using LMS_WebAPP_Utils;
using System;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class LeaveTransactionController : Controller
    {
        static HttpClient client = new HttpClient();
        

        // GET: LeaveTransection
        public async Task<ActionResult> LeaveTransaction()
        {
            Logger.Info("Entering in LeaveTransactionController APP LeaveTransaction method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
            {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
                    var res = await ELTM.GetEmployeeLeaveTransactionAsync(data.RefEmployeeId);
                    //var values = Enum.GetValues(typeof(LeaveType));
                    Logger.Info("Successfully exiting from LeaveTransactionController APP LeaveTransaction method");
                    return View(res);
            }
            else
            {
                    Logger.Info("Successfully exiting from LeaveTransactionController APP LeaveTransaction method");
                    return RedirectToAction("Login", "Account");
            }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at LeaveTransactionController APP LeaveTransaction method.", ex);
                return View("Error");
            }
        }

        
    }
}