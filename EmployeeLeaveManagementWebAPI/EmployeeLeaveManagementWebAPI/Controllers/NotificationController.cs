using LMS_WebAPI_DAL;
using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class NotificationController : ApiController
    {
        // GET: Notification

        [EnableCors(origins: "http://localhost:64949", headers: "*", methods: "*")]
        public void GetNotificationSeen(int Id, int NotificationType)
        {
            NotificationManagement NM = new NotificationManagement();
            NM.NotificationSeen(Id, NotificationType);
        }

        [EnableCors(origins: "http://localhost:64949", headers: "*", methods: "*")]
        public List<NotificationModel> GetNotifications(int id)
        {
            var notificationRegisterTime = HttpContext.Current.Session["LastUpdated"] != null ? Convert.ToDateTime(HttpContext.Current.Session["LastUpdated"]) : DateTime.Now;
            NotificationManagement NM = new NotificationManagement();
            var res = NM.GetNotifications(id);
            return res;
        }
    }
}