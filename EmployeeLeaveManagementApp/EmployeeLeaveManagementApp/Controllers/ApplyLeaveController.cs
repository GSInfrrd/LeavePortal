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
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    IList<LeaveTransaction> les = new List<LeaveTransaction>();
                    les = await ELTM.GetProductAsync(data.RefEmployeeId);
                    return View(les);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult AddLeave()
        {
            return View();

        }

        [HttpPost]
        public async Task<JsonResult> SubmitLeaveRequest(int leaveType, string fromDate, string toDate, string comments, double workingDays, bool isFullDay = true)
        {
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
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }


        [HttpPost]
        public async Task<JsonResult> SubmitLeaveRequestForCasualORAdvance(int CasualleaveCount, int AdvanceLeaveCount, int LOP, string fromDate, string toDate, string comments)
        {
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
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        [HttpPost]
        public async Task<ActionResult> SubmitLeaveForApproval(int id)
        {
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();

            var res = await ELTM.SubmitLeaveForApprovalAsync(id);
            //return RedirectToAction("ApplyLeave");
            return Json(new { result = res });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteLeaveRequest(int leaveId)
        {
            try
            {
                int empId = ((UserAccount)Session[Constants.SESSION_OBJ_USER]).RefEmployeeId;
                var res = await ELTM.DeleteLeaveRequestAsync(leaveId, empId);
                //return RedirectToAction("ApplyLeave");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return RedirectToAction("Error", "Home");
            }
        }

        public async Task<JsonResult> GetEmployeeLeaveList()
        {
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var res = await ELTM.GetProductAsync(data.RefEmployeeId);

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