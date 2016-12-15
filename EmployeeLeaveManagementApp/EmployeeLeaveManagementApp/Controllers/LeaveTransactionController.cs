using System.Net.Http;
using System.Web.Mvc;
using LMS_WebAPP_ServiceHelpers;
using System.Threading.Tasks;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class LeaveTransactionController : Controller
    {
        static HttpClient client = new HttpClient();
        

        // GET: LeaveTransection
        public async Task<ActionResult> LeaveTransaction()
        {
            EmployeeLeaveTransactionManagement ELTM = new EmployeeLeaveTransactionManagement();
            var res = await ELTM.GetProductAsync();
            
            return View(res);
        }

        
    }
}