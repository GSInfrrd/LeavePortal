using System;
using System.Net.Http;
using System.Threading.Tasks;
using LMS_WebAPP_Domain;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;

namespace LMS_WebAPP_ServiceHelpers
{
    public class EmployeeLeaveTransactionManagement
    {
        static HttpClient client = new HttpClient();


        private string URL = "http://localhost:64476/api/EmployeeLeaveTrans";
        private string urlParameters;

        public async Task<IList<LeaveTransaction>> GetProductAsync(int empid)
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
                var dataObjects =  response.Content.ReadAsAsync<IList<LeaveTransaction>>().Result.ToList();
                return dataObjects;

            }
            return null;
        }

        public async Task<IList<LeaveTransaction>> SubmitLeaveRequestAsync(int id, int leaveType,string fromDate,string toDate,string comments,int workingDays)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            var urlParameters = "?Id=" + id + "&leaveType=" + leaveType + "&fromDate=" + fromDate + "&toDate="+ toDate+"&comments="+comments+"&workingDays="+workingDays;
            URL += urlParameters;
            //URL = URL + "/SubmitLeaveRequest";
            

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<IList<LeaveTransaction>>().Result.ToList();
                return dataObjects;

            }
            return null;
        }

        public async Task<IList<LeaveTransaction>> SubmitLeaveForApprovalAsync(int id)
        {
            HttpClient client = new HttpClient();
            var urlParameters = "?id=" + id + "&status=" + true;
            //urlParameters = "?empId=" + empId + "&year=" + year;
            //URL = URL + "/SubmitLeaveRequest";
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<IList<LeaveTransaction>>().Result.ToList();
                return dataObjects;

            }
            return null;
        }


        public async Task<IList<LeaveTransaction>> DeleteLeaveRequestAsync(int leaveId,int empId)
        {
            HttpClient client = new HttpClient();
            var urlParameters = "?leaveId=" + leaveId+"&employeeId="+ empId;
            //URL = URL + "/SubmitLeaveRequest";
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<IList<LeaveTransaction>>().Result.ToList();
                return dataObjects;

            }
            return null;
        }

    }
}
