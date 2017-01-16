using System.Net.Http;
using System.Web.Mvc;
using LMS_WebAPP_ServiceHelpers;
using System.Threading.Tasks;
using System;
using LMS_WebAPP_Utils;
using LMS_WebAPP_Domain;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class ApplyLeaveController : Controller
    {
        static HttpClient client = new HttpClient();
        EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();

        // GET: LeaveTransection
        public async Task<ActionResult> ApplyLeave()
        {
            Logger.Info("Entering in ApplyLeaveController APP ApplyLeave method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    IList<LeaveTransaction> leaveTransactionList = new List<LeaveTransaction>();
                    leaveTransactionList = await ELTM.GetEmployeeLeaveTransactionAsync(data.RefEmployeeId);
                    Logger.Info("Successfully exiting from ApplyLeaveController APP ApplyLeave method");
                    return View(leaveTransactionList);
                }
                else
                {
                    Logger.Info("Successfully exiting from ApplyLeaveController APP ApplyLeave method");
                    return RedirectToAction("Login", "Account");
                }

            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApplyLeaveController APP ApplyLeave method.", ex);
                return View("Error");
            }
        }

        public ActionResult AddLeave()
        {
            return View();

        }

        [HttpPost]
        public async Task<JsonResult> SubmitLeaveRequest(int leaveType, string fromDate, string toDate, string comments, double workingDays, bool isFullDay = true)
        {
            Logger.Info("Entering in ApplyLeaveController APP SubmitLeaveRequest method");
            try
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                int id = data.RefEmployeeId;
                double WorkingDays = Convert.ToDouble(workingDays);
                EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
                switch ((LeaveType)leaveType)
                {
                    case LeaveType.SickLeave:
                    case LeaveType.CasualLeave:
                        if (!isFullDay)
                        {
                            WorkingDays = 0.5;
                        }
                        break;
                    default:
                        break;
                }
                var res = await ELTM.SubmitLeaveRequestAsync(id, leaveType, fromDate, toDate, comments, WorkingDays);
                Logger.Info("Successfully exiting from ApplyLeaveController APP SubmitLeaveRequest method");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApplyLeaveController APP SubmitLeaveRequest method.", ex);
                return null;
            }
        }


        [HttpPost]
        public async Task<JsonResult> SubmitLeaveRequestForCasualORAdvance(int CasualleaveCount, int AdvanceLeaveCount, int LOP, string fromDate, string toDate, string comments)
        {
            Logger.Info("Entering in ApplyLeaveController APP SubmitLeaveRequestForCasualORAdvance method");
            try
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                int id = data.RefEmployeeId;
                string fromDay = fromDate;
                string toDay = toDate;
                IList<LeaveTransaction> res = new List<LeaveTransaction>();
                //Add advance leave
                if (CasualleaveCount != 0)
                {
                    fromDay = fromDate;
                    toDay = CommonMethods.AddBusinessDays(Convert.ToDateTime(fromDay), CasualleaveCount).ToString();
                    res = await ELTM.SubmitLeaveRequestAsync(id, Convert.ToInt16(LeaveType.CasualLeave), fromDay, toDay, comments, Convert.ToDouble(CasualleaveCount));
                }
                if (AdvanceLeaveCount != 0)
                {
                    var temtoday = toDay;
                    toDay = CommonMethods.AddBusinessDays(Convert.ToDateTime(toDay), AdvanceLeaveCount).ToString();
                    fromDay = Convert.ToDateTime(temtoday).AddDays(1).ToString();
                    res = await ELTM.SubmitLeaveRequestAsync(id, Convert.ToInt16(LeaveType.AdvanceLeave), fromDay, toDay, comments, Convert.ToDouble(AdvanceLeaveCount));
                }
                if (LOP != 0)
                {
                    var temtoday = toDay;
                    toDay = CommonMethods.AddBusinessDays(Convert.ToDateTime(toDay), LOP).ToString();
                    fromDay = Convert.ToDateTime(temtoday).AddDays(1).ToString();//toDay;
                    res = await ELTM.SubmitLeaveRequestAsync(id, Convert.ToInt16(LeaveType.LOP), fromDay, toDay, comments, Convert.ToDouble(LOP));
                }
                Logger.Info("Successfully exiting from ApplyLeaveController APP SubmitLeaveRequestForCasualORAdvance method");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApplyLeaveController APP SubmitLeaveRequestForCasualORAdvance method.", ex);
                return null;
            }
        }
        [HttpPost]
        public async Task<ActionResult> SubmitLeaveForApproval(int id)
        {
            Logger.Info("Entering in ApplyLeaveController APP SubmitLeaveForApproval method");
            try
            {
                EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();

            var res = await ELTM.SubmitLeaveForApprovalAsync(id);
                //return RedirectToAction("ApplyLeave");
                Logger.Info("Successfully exiting from ApplyLeaveController APP SubmitLeaveForApproval method");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApplyLeaveController APP SubmitLeaveForApproval method.", ex);
                return null;
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteLeaveRequest(int leaveId)
        {
            Logger.Info("Entering in ApplyLeaveController APP DeleteLeaveRequest method");
            try
            {
                int empId = ((UserAccount)Session[Constants.SESSION_OBJ_USER]).RefEmployeeId;
                var res = await ELTM.DeleteLeaveRequestAsync(leaveId, empId);
                //return RedirectToAction("ApplyLeave");
                Logger.Info("Successfully exiting from ApplyLeaveController APP DeleteLeaveRequest method");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApplyLeaveController APP DeleteLeaveRequest method.", ex);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<JsonResult> GetEmployeeLeaveList()
        {
            Logger.Info("Entering in ApplyLeaveController APP GetEmployeeLeaveList method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var res = await ELTM.GetEmployeeLeaveTransactionAsync(data.RefEmployeeId);

                    var resultJson = new { result = res };
                    Logger.Info("Successfully exiting from ApplyLeaveController APP GetEmployeeLeaveList method");
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApplyLeaveController APP GetEmployeeLeaveList method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> CheckLeaveAvailability(DateTime fromDate, DateTime toDate,int leaveType)
        {
            Logger.Info("Entering in ApplyLeaveController APP CheckLeaveAvailability method");
            try
            {
                var result = new LeaveTransactionResponse();
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    result = await ELTM.CheckLeaveAvailabilityAsync(data.RefEmployeeId, fromDate, toDate,leaveType);
                    Logger.Info("Successfully exiting from ApplyLeaveController APP CheckLeaveAvailability method");

                }
                return Json(new { result = result });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApplyLeaveController APP CheckLeaveAvailability method.", ex);
                return null;
            }
        }


    }
}