using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using LMS_WebAPP_Domain;
using LMS_WebAPI_Domain;

namespace LMS_WebAPP_ServiceHelpers
{
    public class UserManagement
    {


        ////static async Task<UserAccount> GetProductAsync(string path)
        ////{
        ////    UserAccount user = null;
        ////    HttpResponseMessage response = await client.GetAsync(path);
        ////    if (response.IsSuccessStatusCode)
        ////    {
        ////        user = await response.Content.ReadAsAsync<UserAccount>();
        ////    }
        ////    return user;
        ////}

        ////static async Task RunAsync()
        ////{
        ////    New code:
        ////    client.BaseAddress = new Uri("http://localhost:55268/");
        ////    client.DefaultRequestHeaders.Accept.Clear();
        ////    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        ////    Console.ReadLine();
        ////}

        private string urlParameters = "";

        public async Task<UserAccount> GetUserAsync(string userName, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                const string URL = "http://localhost:64476/api/Account/Login";
                urlParameters = "?userName=" + userName + "&password=" + password;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = await response.Content.ReadAsAsync<UserAccount>();
                    return dataObjects;
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return null;
                }
            }
        }

        public async Task<EmployeeDetailsModel> GetUserDetailsAsync(int EmpId)
        {
            using (HttpClient client = new HttpClient())
            {
                const string URL = "http://localhost:64476/api/Account/GetUserDetails";
                urlParameters = "?empId=" + EmpId;
                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = await response.Content.ReadAsAsync<EmployeeDetailsModel>();
                    return dataObjects;
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return null;
                }
            }
        }

        
                 public async Task<LeaveReportModel> GetLeaveReportDetails(int empId,int year)
        {
            using (HttpClient client = new HttpClient())
            {
                const string URL = "http://localhost:64476/api/Account/GetUserDetails";
                urlParameters = "?empId=" + empId+"&year="+year;
                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = await response.Content.ReadAsAsync<LeaveReportModel>();
                    return dataObjects;
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return null;
                }
            }
        }


                      public async Task<EmployeeDetailsModel> GetUserProfileDetails(int empId)
        {
            using (HttpClient client = new HttpClient())
            {
                const string URL = "http://localhost:64476/api/Profile/GetUserProfileDetails";
                urlParameters = "?empId=" + empId;
                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = await response.Content.ReadAsAsync<EmployeeDetailsModel>();
                    return dataObjects;
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return null;
                }
            }
        }
    }
}

