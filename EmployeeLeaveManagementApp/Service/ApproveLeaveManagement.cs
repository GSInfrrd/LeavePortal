using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPP_Domain;
using System.Net.Http.Headers;

namespace LMS_WebAPP_ServiceHelpers
{
    public class ApproveLeaveManagement
    {
        static HttpClient client = new HttpClient();


        private string URL = "http://localhost:64476/api/ApproveLeave";
        private string urlParameters;

        public async Task<IList<ApproveLeave>> GetAprroveLeaveAsync(int empid)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            urlParameters = "?id=" + empid;
            URL += urlParameters;
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<IList<ApproveLeave>>().Result.ToList();
                return dataObjects;

            }
            return null;
        }
      
        public async Task<bool> AprroveEmployeeLeaveAsync(int id , string comments , int st)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            urlParameters = "?id=" + id + "&comments=" + comments + "&st=" + st;
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
