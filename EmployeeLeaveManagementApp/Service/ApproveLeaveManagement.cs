using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPP_Domain;
using System.Net.Http.Headers;
using LMS_WebAPP_Utils;
using System.Configuration;

namespace LMS_WebAPP_ServiceHelpers
{
    public class ApproveLeaveManagement
    {
        static HttpClient client = new HttpClient();

        private string URL = ConfigurationManager.AppSettings["WebApiURL"] + "/ApproveLeave";
        private string urlParameters;

        public async Task<IList<ApproveLeave>> GetAprroveLeaveAsync(int empid)
        {
            Logger.Info("Entering into ApproveLeaveManagement APP Service helper GetAprroveLeaveAsync method ");
            try
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
                    Logger.Info("Exiting from into ApproveLeaveManagement APP Service helper GetAprroveLeaveAsync method ");
                    return dataObjects;

                }
                Logger.Info("Exiting from into ApproveLeaveManagement APP Service helper GetAprroveLeaveAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveManagement APP Service helper GetAprroveLeaveAsync method ");
                throw;
            }
        }
      
        public async Task<bool> TakeActionOnEmployeeLeaveAsync(int Leaveid, string Leavecomments, string Leavestatus, int Approverid)
        {
            Logger.Info("Entering into ApproveLeaveManagement APP Service helper AprroveEmployeeLeaveAsync method ");
            try
            {
                HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            urlParameters = "?Leaveid=" + Leaveid + "&Leavecomments=" + Leavecomments + "&Leavestatus=" + Leavestatus + "&Approverid=" + Approverid;
            URL += urlParameters;
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                    Logger.Info("Exiting from into ApproveLeaveManagement APP Service helper AprroveEmployeeLeaveAsync method ");
                    return true;
            }
                Logger.Info("Exiting from into ApproveLeaveManagement APP Service helper AprroveEmployeeLeaveAsync method ");
                return false;
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveManagement APP Service helper AprroveEmployeeLeaveAsync method ");
                throw;
            }
        }

        public async Task<IList<EmployeeDetailsModel>> GetAllManagersAsync(int id, int st)
        {
            Logger.Info("Entering into ApproveLeaveManagement APP Service helper GetAllManagersAsync method ");
            try
            {
                HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            urlParameters = "?id=" + id + "&st=" + st;
            URL += urlParameters;
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsAsync<IList<EmployeeDetailsModel>>().Result.ToList();
                    Logger.Info("Exiting from into ApproveLeaveManagement APP Service helper GetAllManagersAsync method ");
                    return dataObjects;

            }
                Logger.Info("Exiting from into ApproveLeaveManagement APP Service helper GetAllManagersAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveManagement APP Service helper GetAllManagersAsync method ");
                throw;
            }
        }
    }
}
