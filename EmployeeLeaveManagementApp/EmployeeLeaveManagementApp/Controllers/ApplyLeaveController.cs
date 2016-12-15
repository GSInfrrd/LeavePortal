using System.Net.Http;
using System.Web.Mvc;
using LMS_WebAPP_ServiceHelpers;
using System.Threading.Tasks;
using System;
using LMS_WebAPP_Utils;

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
                EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
                var res = await ELTM.GetProductAsync();
                //var values = Enum.GetValues(typeof(LeaveType));
                return View(res);
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
        }

        public ActionResult AddLeave()
        {
            return View();

        }
        [HttpPost]
        public async  Task<ActionResult> SubmitLeaveRequest(int leaveType,string fromDate,string toDate,string comments,int workingDays)
        {
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
          
            var res =await  ELTM.SubmitLeaveRequestAsync(leaveType, fromDate,toDate,comments,workingDays);
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
        public async Task<ActionResult> DeleteLeaveRequest(int id)
        {
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();

            var res = await ELTM.DeleteLeaveRequestAsync(id);
            //return RedirectToAction("ApplyLeave");
            return Json(new { result = res });
        }

    }
}