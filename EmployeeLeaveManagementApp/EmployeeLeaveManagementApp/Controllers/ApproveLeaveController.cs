using LMS_WebAPP_Domain;
using LMS_WebAPP_ServiceHelpers;
using LMS_WebAPP_Utils;
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
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                ApproveLeaveManagement ELTM = new ApproveLeaveManagement();
                var res = await ELTM.GetAprroveLeaveAsync(data.RefEmployeeId);
                //var values = Enum.GetValues(typeof(LeaveType));
                return View(res);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ApproveEmployeeLeave(int id, string comments , int st)
        {
            ApproveLeaveManagement ELTM = new ApproveLeaveManagement();

            var res = await ELTM.AprroveEmployeeLeaveAsync(id, comments, st);
            //return RedirectToAction("ApplyLeave");
            return Json(new { result = res });
        }

        [HttpPost]
        public async Task<ActionResult> GetAllManagers()
        {
            ApproveLeaveManagement ELTM = new ApproveLeaveManagement();

            var res = await ELTM.GetAllManagersAsync();
            //return RedirectToAction("ApplyLeave");
            return Json(new { result = res });
        }

    }
}