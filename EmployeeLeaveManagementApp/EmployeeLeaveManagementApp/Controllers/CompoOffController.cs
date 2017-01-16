using LMS_WebAPP_Domain;
using LMS_WebAPP_ServiceHelpers;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class CompoOffController : Controller
    {
        EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
        // GET: CompoOff
        public async Task<ActionResult> Index()
        {
            Logger.Info("Entering in CompoOffController APP Index method");
            try
            {
                //get all CompoOff leaves 
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
            
                    var leaveType = Convert.ToInt16(LeaveType.CompoOff);
                    var res = await ELTM.GetEmployeeLeaveTransactionAsync(data.RefEmployeeId, leaveType);
                    Logger.Info("Successfully exiting from CompoOffController APP Index method");
                    return View(res);
                }
                else
                {
                    Logger.Info("Successfully exiting from CompoOffController APP Index method");
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at CompoOffController APP Index method.", ex);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<JsonResult> ApplyCompoOff(string fromDate, string comments)
        {
            Logger.Info("Entering in CompoOffController APP ApplyCompoOff method");
            try
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
                int empId = data.RefEmployeeId;
                var CompoOff = Convert.ToInt16(LeaveType.CompoOff);
                var res = await ELTM.SubmitLeaveRequestAsync(empId, CompoOff, fromDate, fromDate, comments, 1);
                Logger.Info("Successfully exiting from CompoOffController APP ApplyCompoOff method");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at CompoOffController APP ApplyCompoOff method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> GetCompoOffList()
        {
            Logger.Info("Entering in CompoOffController APP GetCompoOffList method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var leaveType = Convert.ToInt16(LeaveType.CompoOff);
                    var res = await ELTM.GetEmployeeLeaveTransactionAsync(data.RefEmployeeId, leaveType);
                    var resultJson = new { result = res };
                    Logger.Info("Successfully exiting from CompoOffController APP GetCompoOffList method");
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                Logger.Info("Successfully exiting from CompoOffController APP GetCompoOffList method");
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at CompoOffController APP GetCompoOffList method.", ex);
                return null;
            }
        }
    }
}