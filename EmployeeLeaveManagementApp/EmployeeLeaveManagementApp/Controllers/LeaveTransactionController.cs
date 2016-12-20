using System.Net.Http;
using System.Web.Mvc;
using LMS_WebAPP_ServiceHelpers;
using System.Threading.Tasks;
using LMS_WebAPP_Domain;
using LMS_WebAPP_Utils;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class LeaveTransactionController : Controller
    {
        static HttpClient client = new HttpClient();
        

        // GET: LeaveTransection
        public async Task<ActionResult> LeaveTransaction()
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

        
    }
}