﻿using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        public List<NotificationModel> GetNotifications(int id)
        {
            try
            {
                Logger.Info("Entering in NotificationRepository API GetNotifications method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var EmployeeNotifications = ctx.Notifications.Where(m => m.RefEmployeeId == id).ToList();
                    var retResult = ToModel(EmployeeNotifications);
                    Logger.Info("Successfully exiting from NotificationRepository API GetNotifications method");
                    return retResult;
                }
            }
            catch
            {
                Logger.Info("Exception occured at NotificationRepository GetNotifications method ");
                throw;
            }
        }

        public void NotificationSeen(int id, int NotificationType)
        {
            Logger.Info("Entering in NotificationRepository API NotificationSeen method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var EmployeeNotifications = ctx.Notifications.FirstOrDefault(x => x.Id == id);
                    ctx.Notifications.Remove(EmployeeNotifications);
                    ctx.SaveChanges();
                }
                Logger.Info("Successfully exiting from NotificationRepository API NotificationSeen method");
            }
            catch
            {
                Logger.Info("Exception occured at NotificationRepository NotificationSeen method ");
                throw;
            }
        }

        private List<NotificationModel> ToModel(List<Notification> employeeNotification)
        {
            Logger.Info("Entering in NotificationRepository API ToModel method");
            List<NotificationModel> Empres = new List<NotificationModel>();
            try
            {

                foreach (var m in employeeNotification)
                {
                    var newTrans = new NotificationModel();
                    newTrans.Id = m.Id;
                    newTrans.RefEmployeeId = m.RefEmployeeId;
                    newTrans.Text = m.Text;
                    newTrans.CreatedDate = m.CreatedDate;
                    newTrans.RefNotificationType = m.RefNotificationType;
                    Empres.Add(newTrans);
                }
                Logger.Info("Successfully exiting from NotificationRepository API ToModel method");
            }
            catch 
            {
                Logger.Info("Exception occured at NotificationRepository ToModel method ");
                throw;
            }
            return Empres;
        }
    
}
}
