using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
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
                using (var ctx = new LeaveManagementSystemEntities1())
                {


                    var EmployeeNotifications = ctx.Notifications.Where(m => m.RefEmployeeId == id).ToList();
                    var retResult = ToModel(EmployeeNotifications);

                    if (retResult != null)
                    {
                        return retResult;
                    }
                    else
                        return null;
                }
            }
            catch
            {
                throw;
            }
        }

        public void NotificationSeen(int id, int NotificationType)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var EmployeeNotifications = ctx.Notifications.FirstOrDefault(x => x.Id == id);
                    ctx.Notifications.Remove(EmployeeNotifications);
                    ctx.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        private List<NotificationModel> ToModel(List<Notification> employeeNotification)
        {
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
            }
            catch (Exception)
            {

                throw;
            }
            return Empres;



        }
    
}
}
