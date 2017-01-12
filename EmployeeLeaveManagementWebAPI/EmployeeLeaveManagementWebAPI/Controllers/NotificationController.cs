using LMS_WebAPI_DAL;
using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
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
            try
            {
                Logger.Info("Entering in NotificationController API GetNotificationSeen method");
                NotificationManagement NM = new NotificationManagement();
                NM.NotificationSeen(Id, NotificationType);
                Logger.Info("Successfully exiting from NotificationController API GetNotificationSeen method");
            }
            catch (Exception ex)
            {
                Logger.Error("Error at NotificationController API GetNotificationSeen method.", ex);
            }
        }

        [EnableCors(origins: "http://localhost:64949", headers: "*", methods: "*")]
        public List<NotificationModel> GetNotifications(int id)
        {
            try
            {
                Logger.Info("Entering in NotificationController API GetNotifications method");
                var notificationRegisterTime = HttpContext.Current.Session["LastUpdated"] != null ? Convert.ToDateTime(HttpContext.Current.Session["LastUpdated"]) : DateTime.Now;
                NotificationManagement NM = new NotificationManagement();
                var res = NM.GetNotifications(id);
                Logger.Info("Successfully exiting from NotificationController API GetNotifications method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at NotificationController API GetNotifications method.", ex);
                return null;
            }
        }
    }
}