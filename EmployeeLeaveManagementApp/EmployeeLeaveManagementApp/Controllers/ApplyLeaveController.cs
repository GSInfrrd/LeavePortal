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
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                IList<LeaveTransaction> les = new List<LeaveTransaction>();
                les = await ELTM.GetProductAsync(data.RefEmployeeId);
                // var datas = (LMS_WebAPP_Domain.UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER];
                var toTalcasualLeave = data.TotalCasualLeave;
                var refLeaveTypeCasual = @Convert.ToInt16((LMS_WebAPP_Utils.LeaveType.CasualLeave));
                var LeaveStatusIn = @Convert.ToInt16((LMS_WebAPP_Utils.LeaveStatus.Approved));


                var totalCasualApproved = (from n in les where n.RefLeaveType == refLeaveTypeCasual && n.RefStatus == LeaveStatusIn select n).ToList().Count();
                var totalLeft = toTalcasualLeave - totalCasualApproved;

                //var values = Enum.GetValues(typeof(LeaveType));
                return View(les);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult AddLeave()
        {
            return View();

        }
        [HttpPost]
        public async Task<JsonResult> SubmitLeaveRequest(int leaveType, string fromDate, string toDate, string comments, double workingDays, bool isFullDay = true)
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
            //return RedirectToAction("ApplyLeave");
            return Json(new { result = res });
        }


        [HttpPost]
        public async Task<JsonResult> SubmitLeaveRequestForCasualORAdvance(int CasualleaveCount, int AdvanceLeaveCount, int LOP, string fromDate, string toDate, string comments)
        {
            var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
            int id = data.RefEmployeeId;
            string fromDay = fromDate;
            string toDay = toDate;
            IList<LeaveTransaction> res = new List<LeaveTransaction>();
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
            //Add advance leave
            if (CasualleaveCount != 0)
            {
                fromDay = fromDate;
                toDay = Convert.ToDateTime(fromDay).AddDays(CasualleaveCount).ToString();
                res = await ELTM.SubmitLeaveRequestAsync(id, Convert.ToInt16(LeaveType.CasualLeave), fromDay, toDay, comments, Convert.ToDouble(CasualleaveCount));
            }
            if (AdvanceLeaveCount != 0)
            {
                fromDay = toDay;
                toDay = Convert.ToDateTime(fromDay).AddDays(AdvanceLeaveCount).ToString();
                res = await ELTM.SubmitLeaveRequestAsync(id, Convert.ToInt16(LeaveType.AdvanceLeave), fromDay, toDay, comments, Convert.ToDouble(AdvanceLeaveCount));
            }
            if (LOP != 0)
            {
                fromDay = toDay;
                toDay = Convert.ToDateTime(fromDay).AddDays(LOP).ToString();
                res = await ELTM.SubmitLeaveRequestAsync(id, Convert.ToInt16(LeaveType.LOP), fromDate, toDate, comments, Convert.ToDouble(LOP));
            }
            //return RedirectToAction("ApplyLeave");
            return Json(new { result = res });
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
            int empId = ((UserAccount)Session[Constants.SESSION_OBJ_USER]).RefEmployeeId;
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();

            var res = await ELTM.DeleteLeaveRequestAsync(leaveId, empId);
            //return RedirectToAction("ApplyLeave");
            return Json(new { result = res });
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