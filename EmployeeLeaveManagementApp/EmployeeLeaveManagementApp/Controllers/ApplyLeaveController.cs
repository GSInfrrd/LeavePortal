using System.Net.Http;
using System.Web.Mvc;
using LMS_WebAPP_ServiceHelpers;
using System.Threading.Tasks;
using System;
using LMS_WebAPP_Utils;
using LMS_WebAPP_Domain;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class ApplyLeaveController : Controller
    {
        static HttpClient client = new HttpClient();


        // GET: LeaveTransection
        public async Task<ActionResult> ApplyLeave()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
                var res = await ELTM.GetProductAsync(data.RefEmployeeId);
                //var values = Enum.GetValues(typeof(LeaveType));
                return View(res);
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
        public async Task<ActionResult> SubmitLeaveRequest(int leaveType, string fromDate, string toDate, string comments, int workingDays, bool isFullDay = true)
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

    }
}