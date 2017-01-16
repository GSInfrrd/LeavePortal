using LMS_WebAPP_Domain;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_ServiceHelpers
{
    public class NotificationManagement
    {
        static HttpClient client = new HttpClient();


        private string URL = "http://localhost:64476/api/Notification";
        private string urlParameters;

        public async Task<IList<Notification>> GetNotificationsAsync(int id)
        {
            Logger.Info("Entering into NotificationManagement APP Service helper GetNotificationsAsync method ");
            try
            {
                HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            urlParameters = "?id=" + id;
            URL += urlParameters;
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<IList<Notification>>().Result.ToList();
                    Logger.Info("Exiting from into NotificationManagement APP Service helper GetNotificationsAsync method ");
                    return dataObjects;

            }
                Logger.Info("Exiting from into NotificationManagement APP Service helper GetNotificationsAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at NotificationManagement APP Service helper GetNotificationsAsync method ");
                throw;
            }
        }

        public async Task<bool> NotificationSeenAsync(int Id, int NotificationType)
        {
            Logger.Info("Entering into NotificationManagement APP Service helper NotificationSeenAsync method ");
            try
            {
                HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            urlParameters = "?Id=" + Id + "&NotificationType=" + NotificationType;
            URL += urlParameters;
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                    Logger.Info("Exiting from into NotificationManagement APP Service helper NotificationSeenAsync method ");
                    return true;
            }
                Logger.Info("Exiting from into NotificationManagement APP Service helper NotificationSeenAsync method ");
                return false;
            }
            catch
            {
                Logger.Info("Exception occured at NotificationManagement APP Service helper NotificationSeenAsync method ");
                throw;
            }
        }
    }
}
