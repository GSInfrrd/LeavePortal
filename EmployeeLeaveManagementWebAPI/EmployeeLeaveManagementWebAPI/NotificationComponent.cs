using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LMS_WebAPI_DAL;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Data.Entity.Core.Objects;

namespace EmployeeLeaveManagementWebAPI
{
    public class NotificationComponent
    {
        //Here we will add a function for register notification (will add sql dependency)
        public void RegisterNotification(DateTime currentTime)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["sqlConString"].ConnectionString;
            string commandText = null;
            var parameters = new SqlParameter[0];

            using (var db = new LeaveManagementSystemEntities1())
            {
                var query = db.Notifications.Where(x => x.CreatedDate > currentTime) as DbQuery<Notification>;
                commandText = query.ToString();

                var internalQuery = query.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(field => field.Name == "_internalQuery")
                .Select(field => field.GetValue(query))
                .First();

                var objectQuery = internalQuery.GetType()
                    .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(field => field.Name == "_objectQuery")
                    .Select(field => field.GetValue(internalQuery))
                    .Cast<ObjectQuery<Notification>>()
                    .First();

                parameters = objectQuery.Parameters.Select(x => new SqlParameter(x.Name, x.Value)).ToArray();
            }
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.Parameters.AddRange(parameters);
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChange;
                //we must have to execute the command here
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    // nothing need to add here now
                }
            }
        }

        void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            //or you can also check => if (e.Info == SqlNotificationInfo.Insert) , if you want notification only for inserted record
            if (e.Info == SqlNotificationInfo.Insert)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;

                //from here we will send notification message to client
                var notificationHub = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
                notificationHub.Clients.All.notify("added");
                //re-register notification
                RegisterNotification(DateTime.Now);
            }
        }

        
    }
}