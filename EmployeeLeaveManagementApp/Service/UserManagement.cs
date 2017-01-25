using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using LMS_WebAPP_Domain;
using LMS_WebAPI_Domain;
using Newtonsoft.Json;
using LMS_WebAPP_Utils;
using System.Configuration;

namespace LMS_WebAPP_ServiceHelpers
{
    public class UserManagement
    {


        ////static async Task<UserAccount> GetEmployeeLeaveTransactionAsync(string path)
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
        private static string WebapiURL = ConfigurationManager.AppSettings["WebApiURL"] + "/Account";
        private static string ProfileURL = ConfigurationManager.AppSettings["WebApiURL"] + "/Profile";
        private string urlParameters = "";

        public async Task<List<EmployeeDetailsModel>> GetTeamMembers(int empId)
        {
            using (HttpClient client = new HttpClient())
            {
                 string URL = WebapiURL + "/GetTeamMembers";
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
                    var dataObjects = await response.Content.ReadAsAsync<List<EmployeeDetailsModel>>();
                    return dataObjects;
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    return null;
                }
            }
        }

        public async Task<UserAccount> GetUserAsync(string userName, string password)
        {
            Logger.Info("Entering into UserManagement APP Service helper GetUserAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                string URL = WebapiURL + "/Login";
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
                        Logger.Info("Exiting from into UserManagement APP Service helper GetUserAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into UserManagement APP Service helper GetUserAsync method ");
                        return null;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement APP Service helper GetUserAsync method ");
                throw;
            }
        }

        public async Task<EmployeeDetailsModel> GetUserDetailsAsync(int EmpId)
        {
            Logger.Info("Entering into UserManagement APP Service helper GetUserDetailsAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                string URL = WebapiURL + "/GetUserDetails";
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
                        Logger.Info("Exiting from into UserManagement APP Service helper GetUserDetailsAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into UserManagement APP Service helper GetUserDetailsAsync method ");
                        return null;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement APP Service helper GetUserDetailsAsync method ");
                throw;
            }
        }


        public async Task<LeaveReportModel> GetLeaveReportDetails(int empId, int year,int leaveType=0)
        {
            Logger.Info("Entering into UserManagement APP Service helper GetLeaveReportDetails method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                string URL = WebapiURL + "/GetUserDetails";
                urlParameters = "?empId=" + empId + "&year=" + year+"+&leaveType="+leaveType;
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
                        Logger.Info("Exiting from into UserManagement APP Service helper GetLeaveReportDetails method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into UserManagement APP Service helper GetLeaveReportDetails method ");
                        return null;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement APP Service helper GetLeaveReportDetails method ");
                throw;
            }
        }


        public async Task<EmployeeDetailsModel> GetUserProfileDetails(int empId)
        {
            Logger.Info("Entering into UserManagement APP Service helper GetUserProfileDetails method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                string URL = WebapiURL + "/GetUserProfileDetails";
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
                        Logger.Info("Exiting from into UserManagement APP Service helper GetUserProfileDetails method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into UserManagement APP Service helper GetUserProfileDetails method ");
                        return null;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement APP Service helper GetUserProfileDetails method ");
                throw;
            }
        }

        public async Task<bool> EditEmployeeDetailsAsync(EmployeeDetailsModel model)
        {
            Logger.Info("Entering into UserManagement APP Service helper EditEmployeeDetailsAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                string URL = WebapiURL + "/EditEmployeeDetails";
                urlParameters = "?model=" + model;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URL, model);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                        // Parse the response body. Blocking!
                        var dataObjects = await response.Content.ReadAsAsync<bool>();
                        Logger.Info("Exiting from into UserManagement APP Service helper EditEmployeeDetailsAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into UserManagement APP Service helper EditEmployeeDetailsAsync method ");
                        return false;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement APP Service helper EditEmployeeDetailsAsync method ");
                throw;
            }
        }

        public async Task<bool> EditEmployeeEducationDetailsAsync(List<EmployeeEducationDetails> educationDetails, int employeeId)
        {
            Logger.Info("Entering into UserManagement APP Service helper EditEmployeeEducationDetailsAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(educationDetails);
                StringContent sc = new StringContent(json, Encoding.UTF8, "application/json");

                string URL = ProfileURL +"/EditEmployeeEducationDetails?employeeId=";
                //urlParameters = "?employeeId="+employeeId;
                URL = URL + employeeId;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsync(URL, sc);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                        // Parse the response body. Blocking!
                        var dataObjects = await response.Content.ReadAsAsync<bool>();
                        Logger.Info("Exiting from into UserManagement APP Service helper EditEmployeeEducationDetailsAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into UserManagement APP Service helper EditEmployeeEducationDetailsAsync method ");
                        return false;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement APP Service helper EditEmployeeEducationDetailsAsync method ");
                throw;
            }
        }

        public async Task<bool> EditEmployeeExperienceDetailsAsync(List<EmployeeExperienceDetails> experienceDetails, int employeeId)
        {
            Logger.Info("Entering into UserManagement APP Service helper EditEmployeeExperienceDetailsAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
               
                string URL = ProfileURL + "/EditEmployeeExperienceDetails";
                // urlParameters = "?employeeId="+employeeId;
                URL = URL + "?employeeId=" + employeeId;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URL, experienceDetails);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                        // Parse the response body. Blocking!
                        var dataObjects = await response.Content.ReadAsAsync<bool>();
                        Logger.Info("Exiting from into UserManagement APP Service helper EditEmployeeExperienceDetailsAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into UserManagement APP Service helper EditEmployeeExperienceDetailsAsync method ");
                        return false;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement APP Service helper EditEmployeeExperienceDetailsAsync method ");
                throw;
            }
        }

        public async Task<bool> EditEmployeeSkillsAsync(List<EmployeeSkillDetails> skills, int employeeId)
        {
            Logger.Info("Entering into UserManagement APP Service helper EditEmployeeSkillsAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                string URL = ProfileURL + "/EditEmployeeSkills";
                // urlParameters = "?employeeId="+employeeId;
                URL = URL + "?employeeId=" + employeeId;
                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URL, skills);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                        // Parse the response body. Blocking!
                        var dataObjects = await response.Content.ReadAsAsync<bool>();
                        Logger.Info("Exiting from into UserManagement APP Service helper EditEmployeeSkillsAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into UserManagement APP Service helper EditEmployeeSkillsAsync method ");
                        return false;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement APP Service helper EditEmployeeSkillsAsync method ");
                throw;
            }
        }


    }
}

