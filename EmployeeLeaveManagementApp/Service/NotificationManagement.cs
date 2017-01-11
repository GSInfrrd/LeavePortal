using LMS_WebAPP_Domain;
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
                return dataObjects;

            }
            return null;
        }

        public async Task<bool> NotificationSeenAsync(int Id, int NotificationType)
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
                return true;

            }
            return false;
        }
    }
}
