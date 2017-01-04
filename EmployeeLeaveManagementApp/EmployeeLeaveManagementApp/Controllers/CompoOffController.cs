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
            try
            {
                //get all CompoOff leaves 
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
            
                    var leaveType = Convert.ToInt16(LeaveType.CompoOff);
                    var res = await ELTM.GetProductAsync(data.RefEmployeeId, leaveType);
                    return View(res);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<JsonResult> ApplyCompoOff(string fromDate, string comments)
        {
            var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
            int empId = data.RefEmployeeId;
            var CompoOff = Convert.ToInt16(LeaveType.CompoOff);
            var res = await ELTM.SubmitLeaveRequestAsync(empId, CompoOff, fromDate, fromDate, comments, 1);
            return Json(new { result = res });
        }

        public async Task<JsonResult> GetCompoOffList()
        {
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var leaveType = Convert.ToInt16(LeaveType.CompoOff);
                    var res = await ELTM.GetProductAsync(data.RefEmployeeId, leaveType);
                    var resultJson = new { result = res };
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}