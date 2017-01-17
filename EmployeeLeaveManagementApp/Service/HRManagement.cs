using System;
using System.Net.Http;
using System.Threading.Tasks;
using LMS_WebAPP_Domain;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using LMS_WebAPP_Utils;

namespace LMS_WebAPP_ServiceHelpers
{
    public class HRManagement
    {
        static HttpClient client = new HttpClient();


        private string URL = "http://localhost:64476/api/HR";
        private string urlParameters;

        public async Task<bool> SubmitEmployeeDetailsAsync(EmployeeDetailsModel model)
        {
            Logger.Info("Entering into HRManagement APP Service helper SubmitEmployeeDetailsAsync method ");
            try
            {
                HttpClient client = new HttpClient();
                URL = URL + "/SubmitEmployeeDetails";
                // urlParameters = "?model=" + model;      
                client.BaseAddress = new Uri(URL);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.PostAsJsonAsync(URL, model); // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<bool>().Result;
                    Logger.Info("Exiting from into HRManagement APP Service helper SubmitEmployeeDetailsAsync method ");
                    return dataObjects;

            }
                Logger.Info("Exiting from into HRManagement APP Service helper SubmitEmployeeDetailsAsync method ");
                return false;
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper SubmitEmployeeDetailsAsync method ");
                throw;
            }
        }

        public async Task<List<EmployeeDetailsModel>> GetEmployeeListAsync()
        {
            Logger.Info("Entering into HRManagement APP Service helper GetEmployeeListAsync method ");
            try
            {
                // const string URL = "http://localhost:64476/api/HR/GetEmployeeList";
                HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(URL); // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<List<EmployeeDetailsModel>>().Result.ToList();
                    Logger.Info("Exiting from into HRManagement APP Service helper GetEmployeeListAsync method ");
                    return dataObjects;

            }
                Logger.Info("Exiting from into HRManagement APP Service helper GetEmployeeListAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper GetEmployeeListAsync method ");
                throw;
            }
        }

        public async Task<List<EmployeeDetailsModel>> GetManagerListAsync(int refLevel)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetManagerListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                const string URL = "http://localhost:64476/api/HR/GetManagerList";
                urlParameters = "?refLevel=" + refLevel + "&status=" + true;
                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<EmployeeDetailsModel>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetManagerListAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetManagerListAsync method ");
                        return null;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper GetManagerListAsync method ");
                throw;
            }
        }


        public async Task<List<ConsolidatedEmployeeLeaveDetailsModel>> GenerateReportsAsync(string employeeId, string fromDate, string toDate)
        {
            Logger.Info("Entering into HRManagement APP Service helper GenerateReportsAsync method ");

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    const string URL = "http://localhost:64476/api/HR/GenerateReports";
                    urlParameters = "?fromDate=" + fromDate + "&toDate=" + toDate + "&employeeId=" + employeeId;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<ConsolidatedEmployeeLeaveDetailsModel>>().Result;
                        Logger.Info("Exiting from into HRManagement APP Service helper GenerateReportsAsync method ");
                     return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GenerateReportsAsync method ");
                       return null;
                    }
                }
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper GenerateReportsAsync method ");
                throw;
            }
        }
        public async Task<ConsolidatedEmployeeLeaveDetailsModel> GetChartDetailsAsync(int employeeId)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetChartDetailsAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                const string URL = "http://localhost:64476/api/HR/GetChartDetails";
                urlParameters = "?employeeId=" + employeeId;
                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<ConsolidatedEmployeeLeaveDetailsModel>().Result;
                        Logger.Info("Exiting from into HRManagement APP Service helper GetChartDetailsAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetChartDetailsAsync method ");
                        return null;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper GetChartDetailsAsync method ");
                throw;
            }
        }

        public async Task<bool> AddNewMasterDataValuesAsync(int masterDataType,string masterDataValue)
        {
            Logger.Info("Entering into HRManagement APP Service helper AddNewMasterDataValuesAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                const string URL = "http://localhost:64476/api/HR/AddNewMasterDataValues";
                urlParameters = "?masterDataType=" + masterDataType + "&masterDataValue=" + masterDataValue;
                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<bool>().Result;
                        Logger.Info("Exiting from into HRManagement APP Service helper AddNewMasterDataValuesAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper AddNewMasterDataValuesAsync method ");
                        return false;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper AddNewMasterDataValuesAsync method ");
                throw;
            }
        }

        public async Task<bool> AddNewProjectInfoAsync(string projectName, string description, string technology, DateTime startDate, int refManager)
        {
            Logger.Info("Entering into HRManagement APP Service helper AddNewProjectInfoAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                const string URL = "http://localhost:64476/api/HR/AddNewMasterDataValues";
                urlParameters = "?projectName=" + projectName + "&description=" + description+ "&technology="+ technology+ "&startDate="+ startDate+ "&refManager="+ refManager;
                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<bool>().Result;
                        Logger.Info("Exiting from into HRManagement APP Service helper AddNewProjectInfoAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper AddNewProjectInfoAsync method ");
                        return false;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper AddNewProjectInfoAsync method ");
                throw;
            }
        }

        public async Task<List<ProjectsList>> GetProjectsListAsync(int managerId=0)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetProjectsListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
            {
                const string URL = "http://localhost:64476/api/HR/GetProjectsList";
                    urlParameters = "?managerId=" + managerId;
                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<ProjectsList>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetProjectsListAsync method ");
                        return dataObjects;
                }
                else
                {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetProjectsListAsync method ");
                        return null;
                }
            }
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper GetProjectsListAsync method ");
                throw;
            }
        }

        public async Task<List<ConsolidatedEmployeeLeaveDetailsModel>> GenerateIndividualReportAsync(int employeeId)
        {
            Logger.Info("Entering into HRManagement APP Service helper GenerateIndividualReportAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    const string URL = "http://localhost:64476/api/HR/GenerateIndividualReport";
                    urlParameters = "?employeeId=" + employeeId;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<ConsolidatedEmployeeLeaveDetailsModel>>().Result;
                        Logger.Info("Exiting from into HRManagement APP Service helper GenerateIndividualReportAsync method ");

                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GenerateIndividualReportAsync method ");

                        return null;
                    }
                }
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper GenerateIndividualReportAsync method ");
                throw;
            }
        }

        public async Task<List<EmployeeSkillDetails>> GetSkillsListAsync()
        {
            Logger.Info("Entering into HRManagement APP Service helper GetSkillsListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    const string URL = "http://localhost:64476/api/HR/GetSkillsList";
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<EmployeeSkillDetails>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetSkillsListAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetSkillsListAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper GetSkillsListAsync method ");
                throw;
            }
        }
    }
}
