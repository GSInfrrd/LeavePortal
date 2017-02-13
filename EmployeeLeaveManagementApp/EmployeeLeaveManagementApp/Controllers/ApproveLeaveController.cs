using LMS_WebAPP_Domain;
using LMS_WebAPP_ServiceHelpers;
using LMS_WebAPP_Utils;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class ApproveLeaveController : Controller
    {
        static HttpClient client = new HttpClient();


        // GET: ApproveLeave
        public async Task<ActionResult> ApproveLeave()
        {
            Logger.Info("Entering in ApproveLeaveController APP ApproveLeave method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
            {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                    var res = await ALM.GetAprroveLeaveAsync(data.RefEmployeeId);
                    //var values = Enum.GetValues(typeof(LeaveType));
                    Logger.Info("Successfully exiting from ApproveLeaveController APP ApproveLeave method");
                    return View(res);
            }
            else
            {
                    Logger.Info("Successfully exiting from ApproveLeaveController APP ApproveLeave method");
                    return RedirectToAction("Login", "Account");
            }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController APP ApproveLeave method.", ex);
                return View("Error");
            }
        }

        public async Task<ActionResult> ViewApprovedLeave()
        {
            Logger.Info("Entering in ApproveLeaveController APP ViewApprovedLeave method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                    var res = await ALM.GetViewAprrovedLeaveAsync(data.RefEmployeeId);
                    //var values = Enum.GetValues(typeof(LeaveType));
                    Logger.Info("Successfully exiting from ApproveLeaveController APP ViewApprovedLeave method");
                    return View(res);
                }
                else
                {
                    Logger.Info("Successfully exiting from ApproveLeaveController APP ViewApprovedLeave method");
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController APP ViewApprovedLeave method.", ex);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> TakeActionOnEmployeeLeave(int Leaveid, string Leavecomments, string Leavestatus, int Approverid)
        {
            Logger.Info("Entering in ApproveLeaveController APP TakeActionOnEmployeeLeave method");
            try
            {
                ApproveLeaveManagement ALM = new ApproveLeaveManagement();

                var res = await ALM.TakeActionOnEmployeeLeaveAsync(Leaveid, Leavecomments, Leavestatus, Approverid);
                //return RedirectToAction("ApplyLeave");
                Logger.Info("Successfully exiting from ApproveLeaveController APP TakeActionOnEmployeeLeave method");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController APP TakeActionOnEmployeeLeave method.", ex);
                return View("Error");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> CancelEmployeeLeave(int Leaveid)
        {
            Logger.Info("Entering in ApproveLeaveController APP CancelEmployeeLeave method");
            try
            {
                ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                var res = await ALM.CancelEmployeeLeaveAsync(Leaveid);
                Logger.Info("Successfully exiting from ApproveLeaveController APP CancelEmployeeLeave method");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController APP CancelEmployeeLeave method.", ex);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetAllManagers()
        {
            Logger.Info("Entering in ApproveLeaveController APP GetAllManagers method");
            try
            {
                ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                int id = data.RefEmployeeId;
                int st = 1;
                var res = await ALM.GetAllManagersAsync(id, st);
                Logger.Info("Successfully exiting from ApproveLeaveController APP GetAllManagers method");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController APP GetAllManagers method.", ex);
                return View("Error");
            }
        }

    }
}