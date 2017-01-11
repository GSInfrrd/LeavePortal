using EmployeeLeaveManagementApp.Models;
using LMS_WebAPP_Domain;
using LMS_WebAPP_ServiceHelpers;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class NotificationController : Controller
    {
        // GET: Notification
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetNotifications()
        {
            NotificationManagement ELTM = new NotificationManagement();
            var data = (LMS_WebAPP_Domain.UserAccount)Session[Constants.SESSION_OBJ_USER];
            // var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            int id = data.RefEmployeeId;
            var res = await ELTM.GetNotificationsAsync(id);
           // Session["LastUpdate"] = DateTime.Now;
            return Json(new { result = res });
        }

        [HttpPost]
        public async Task<ActionResult> NotificationSeen(int Id, int NotificationType)
        {
            NotificationManagement ELTM = new NotificationManagement();
            var res = await ELTM.NotificationSeenAsync(Id, NotificationType);
            return Json(new { result = res });
        }


       
        public ActionResult NotificationRedirect(int NotificationType)
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                if (NotificationType == @Convert.ToInt16(NotificationTypes.LeaveNotification))
                {
                    return RedirectToAction("ApplyLeave", "ApplyLeave");
                }

            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            return null;

        }

        
    }
}