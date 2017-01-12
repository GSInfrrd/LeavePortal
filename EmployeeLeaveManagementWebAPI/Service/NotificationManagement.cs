using LMS_WebAPI_DAL;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_ServiceHelpers
{
    public class NotificationManagement
    {
        private INotificationRepository EmployeeNotifications = new NotificationRepository();


        public List<NotificationModel> GetNotifications(int id)
        {
            Logger.Info("Entering into NotificationManagement Service helper GetNotifications method ");
            try
            {
                var NotificationDetails = EmployeeNotifications.GetNotifications(id);
                Logger.Info("Exiting from into NotificationManagement Service helper GetNotifications method ");
                return NotificationDetails;
            }
            catch
            {
                Logger.Info("Exception occured at NotificationManagement Service helper GetNotifications method ");
                throw;
            }
        }

        public void NotificationSeen(int id, int NotificationType)
        {
            Logger.Info("Entering into NotificationManagement Service helper NotificationSeen method ");
            try
            {
                EmployeeNotifications.NotificationSeen(id, NotificationType);
                Logger.Info("Exiting from into NotificationManagement Service helper NotificationSeen method ");
            }
            catch
            {
                Logger.Info("Exception occured at NotificationManagement Service helper NotificationSeen method ");
                throw;
            }
        }
    }
}
