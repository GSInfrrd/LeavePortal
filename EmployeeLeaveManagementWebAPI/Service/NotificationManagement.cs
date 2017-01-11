using LMS_WebAPI_DAL;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
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
            var NotificationDetails = EmployeeNotifications.GetNotifications(id);
            return NotificationDetails;
        }

        public void NotificationSeen(int id, int NotificationType)
        {
            EmployeeNotifications.NotificationSeen(id, NotificationType);
        }
    }
}
