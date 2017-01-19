using System.Net.Http;
using System.Web.Mvc;
using LMS_WebAPP_ServiceHelpers;
using System.Threading.Tasks;
using LMS_WebAPP_Domain;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class LeaveTransactionController : Controller
    {
        static HttpClient client = new HttpClient();

        EmployeeLeaveTransactionManagement leaveManagement = new EmployeeLeaveTransactionManagement();
        // GET: LeaveTransection
        public async Task<ActionResult> LeaveTransaction()
        {
            Logger.Info("Entering in LeaveTransactionController APP LeaveTransaction method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var res = await leaveManagement.GetEmployeeLeaveTransactionAsync(data.RefEmployeeId);
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

        public async Task<JsonResult> GetEmployeeLeaveTransactionList(int leaveType = 0, int month = 0,int transactionType=0)
        {
            Logger.Info("Entering in LeaveTransactionController APP LeaveTransaction method");
            try
            {
                var res = new List<LeaveTransaction>();
                    if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                  res = await leaveManagement.GetEmployeeLeaveTransactionAsync(data.RefEmployeeId,leaveType,month,transactionType);
                    //var values = Enum.GetValues(typeof(LeaveType));
                    Logger.Info("Successfully exiting from LeaveTransactionController APP LeaveTransaction method");
                }
                return Json(new { result = res });

            }
            catch (Exception ex)
            {
                Logger.Error("Error at LeaveTransactionController APP LeaveTransaction method.", ex);
                return null;
            }
        }

        public ActionResult RewardLeaveTransaction()
        {
            Logger.Info("Entering in LeaveTransactionController APP RewardLeaveTransaction method");
            try
            {
                var rewardLeaveDetails = new RewardLeaveModel();
                if (null != Session[Constants.SESSION_OBJ_USER])
                {

                    rewardLeaveDetails = leaveManagement.GetRewardLeaveFormDetails();
                    Logger.Info("Successfully exiting from LeaveTransactionController APP RewardLeaveTransaction method");
                }
                return View(rewardLeaveDetails);

            }
            catch (Exception ex)
            {
                Logger.Error("Error at LeaveTransactionController APP RewardLeaveTransaction method.", ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult RewardLeave(RewardLeaveModel model)
        {
            Logger.Info("Entering in LeaveTransactionController APP RewardLeave method");
            try
            {
                bool rewarded = false;
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    model.ManagerId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;
                    model.ManagerName = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).UserName;

                    rewarded = leaveManagement.SubmitLeaveReward(model);
                    Logger.Info("Successfully exiting from LeaveTransactionController APP RewardLeave method");
                }
                return Json(new { rewarded });

            }
            catch (Exception ex)
            {
                Logger.Error("Error at LeaveTransactionController APP RewardLeave method.", ex);
                return null;
            }
        }
    }
}