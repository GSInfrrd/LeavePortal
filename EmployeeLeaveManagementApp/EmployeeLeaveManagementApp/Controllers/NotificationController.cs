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
            Logger.Info("Entering in NotificationController APP GetNotifications method");
            try
            {
                NotificationManagement ELTM = new NotificationManagement();
                var data = (LMS_WebAPP_Domain.UserAccount)Session[Constants.SESSION_OBJ_USER];
                // var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
                int id = data.RefEmployeeId;
                var res = await ELTM.GetNotificationsAsync(id);
                // Session["LastUpdate"] = DateTime.Now;
                Logger.Info("Successfully exiting from NotificationController APP GetNotifications method");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at NotificationController APP GetNotifications method.", ex);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> NotificationSeen(int Id, int NotificationType)
        {
            Logger.Info("Entering in NotificationController APP NotificationSeen method");
            try
            {
                NotificationManagement ELTM = new NotificationManagement();
                var res = await ELTM.NotificationSeenAsync(Id, NotificationType);
                Logger.Info("Successfully exiting from NotificationController APP NotificationSeen method");
                return Json(new { result = res });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at NotificationController APP NotificationSeen method.", ex);
                return View("Error");
            }
        }


       
        public ActionResult NotificationRedirect(int NotificationType)
        {
            Logger.Info("Entering in NotificationController APP NotificationRedirect method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
            {
                if ((NotificationType == @Convert.ToInt16(NotificationTypes.ApproveLeave))|| (NotificationType == @Convert.ToInt16(NotificationTypes.RejectLeave))|| (NotificationType == @Convert.ToInt16(NotificationTypes.ReassignLeave)))
                {
                        Logger.Info("Successfully exiting from NotificationController APP NotificationRedirect method");
                        return RedirectToAction("ApplyLeave", "ApplyLeave");
                }
                if (NotificationType == @Convert.ToInt16(NotificationTypes.SubmitLeaveRequest))
                {
                        Logger.Info("Successfully exiting from NotificationController APP NotificationRedirect method");
                        return RedirectToAction("ApproveLeave", "ApproveLeave");
                }
                if (NotificationType == @Convert.ToInt16(NotificationTypes.SubmitResourceRequest))
                {
                        Logger.Info("Successfully exiting from NotificationController APP NotificationRedirect method");
                        return RedirectToAction("RequestForResourcesHR", "ResourceRequest");
                }
                if (NotificationType == @Convert.ToInt16(NotificationTypes.SubmitResourceRequestResponse))
                {
                        Logger.Info("Successfully exiting from NotificationController APP NotificationRedirect method");
                        return RedirectToAction("RequestForResources", "ResourceRequest");
                }
                if(NotificationType == @Convert.ToInt16(NotificationTypes.RewardLeave))
                {
                        Logger.Info("Successfully exiting from NotificationController APP NotificationRedirect method");
                        return RedirectToAction("Dashboard", "Account");
                }



            }
            else
            {
                    Logger.Info("Successfully exiting from NotificationController APP NotificationRedirect method");
                    return RedirectToAction("Login", "Account");
            }
                Logger.Info("Successfully exiting from NotificationController APP NotificationRedirect method");
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at NotificationController APP NotificationRedirect method.", ex);
                return View("Error");
            }

        }

        
    }
}