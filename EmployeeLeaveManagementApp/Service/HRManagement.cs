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
using System.Configuration;
using LMS_WebAPI_Domain;

namespace LMS_WebAPP_ServiceHelpers
{
    public class HRManagement
    {
        static HttpClient client = new HttpClient();

        private static string WebapiURL = ConfigurationManager.AppSettings["WebApiURL"] + "/HR";

        private string urlParameters;

        public async Task<bool> SubmitEmployeeDetailsAsync(EmployeeDetailsModel model)
        {
            Logger.Info("Entering into HRManagement APP Service helper SubmitEmployeeDetailsAsync method ");
            try
            {
                HttpClient client = new HttpClient();
                string URL = WebapiURL + "/SubmitEmployeeDetails";
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
                Logger.Error("Exception occured at HRManagement APP Service helper SubmitEmployeeDetailsAsync method ");
                throw;
            }
        }

        public async Task<List<EmployeeDetailsModel>> GetEmployeeListAsync()
        {
            Logger.Info("Entering into HRManagement APP Service helper GetEmployeeListAsync method ");
            try
            {
                string URL = WebapiURL;
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
                Logger.Error("Exception occured at HRManagement APP Service helper GetEmployeeListAsync method ");
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
                    string URL = WebapiURL + "/GetManagerList";
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
                Logger.Error("Exception occured at HRManagement APP Service helper GetManagerListAsync method ");
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
                    string URL = WebapiURL + "/GenerateReports";
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
                Logger.Error("Exception occured at HRManagement APP Service helper GenerateReportsAsync method ");
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
                    string URL = WebapiURL + "/GetChartDetails";
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
                Logger.Error("Exception occured at HRManagement APP Service helper GetChartDetailsAsync method ");
                throw;
            }
        }

        public async Task<bool> AddNewMasterDataValuesAsync(int masterDataType, string masterDataValue)
        {
            Logger.Info("Entering into HRManagement APP Service helper AddNewMasterDataValuesAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/AddNewMasterDataValues";
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
                Logger.Error("Exception occured at HRManagement APP Service helper AddNewMasterDataValuesAsync method ");
                throw;
            }
        }

        public async Task<bool> AddNewProjectInfoAsync(string projectName, string description, List<string> technologies, List<string> technologyDescriptions, DateTime startDate, int refManager)
        {
            Logger.Info("Entering into HRManagement APP Service helper AddNewProjectInfoAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string technology = "";
                    string technologyDetails = "";
                    foreach (var TN in technologies)
                    {
                        technology += TN + ",";
                    }
                    foreach (var TND in technologyDescriptions)
                    {
                        technologyDetails += TND + ",";
                    }
                    technology = technology.TrimEnd(',');
                    technologyDetails = technologyDetails.TrimEnd(',');
                    technologyDetails = technologyDetails.Replace("#", "Sharp");
                    string URL = WebapiURL + "/AddNewProjectInfo";
                    urlParameters = "?projectName=" + projectName + "&description=" + description + "&technology=" + technology + "&technologyDetails=" + technologyDetails + "&startDate=" + startDate + "&refManager=" + refManager;
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
                Logger.Error("Exception occured at HRManagement APP Service helper AddNewProjectInfoAsync method ");
                throw;
            }
        }

        public async Task<bool> AddCompanyAnnouncementsAsync(string title, string carouselContent, string imagePath)
        {
            Logger.Info("Entering into HRManagement APP Service helper AddCompanyAnnouncementsAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/AddCompanyAnnouncements";
                    urlParameters = "?title=" + title + "&carouselContent=" + carouselContent + "&imagePath=" + imagePath;
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
                        Logger.Info("Exiting from into HRManagement APP Service helper AddCompanyAnnouncementsAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper AddCompanyAnnouncementsAsync method ");
                        return false;
                    }
                }
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement APP Service helper AddCompanyAnnouncementsAsync method ");
                throw;
            }
        }


        public async Task<List<ProjectsList>> GetProjectsListAsync(int managerId = 0)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetProjectsListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetProjectsList";
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
                Logger.Error("Exception occured at HRManagement APP Service helper GetProjectsListAsync method ");
                throw;
            }
        }

        public async Task<bool> CheckEmployeeNumberAsync(string employeeNumber)
        {
            Logger.Info("Entering into HRManagement APP Service helper CheckEmployeeNumberAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/CheckEmployeeNumber";
                    urlParameters = "?employeeNumber=" + employeeNumber;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        var dataObjects = response.Content.ReadAsAsync<bool>().Result;
                        Logger.Info("Exiting from into HRManagement APP Service helper CheckEmployeeNumberAsync method ");
                        return dataObjects;
                    }
                    return false;
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetProjectsListAsync method ");
                throw;
            }
        }

        public async Task<bool> CheckEmployeeMailAsync(string employeeMailid)
        {
            Logger.Info("Entering into HRManagement APP Service helper CheckEmployeeMailAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/CheckEmployeeMail";
                    urlParameters = "?employeeMailid=" + employeeMailid;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        var dataObjects = response.Content.ReadAsAsync<bool>().Result;
                        Logger.Info("Exiting from into HRManagement APP Service helper CheckEmployeeMailAsync method ");
                        return dataObjects;
                    }
                    return false;
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper CheckEmployeeMailAsync method ");
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
                    string URL = WebapiURL + "/GenerateIndividualReport";
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
                Logger.Error("Exception occured at HRManagement APP Service helper GenerateIndividualReportAsync method ");
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
                    string URL = WebapiURL + "/GetSkillsList";
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
                Logger.Error("Exception occured at HRManagement APP Service helper GetSkillsListAsync method ");
                throw;
            }
        }

        public async Task<List<CountryDetails>> GetCountriesAsync()
        {
            Logger.Info("Entering into HRManagement APP Service helper GetCountriesAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetCountries";
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<CountryDetails>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetCountriesAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetCountriesAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetCountriesAsync method ");
                throw;
            }
        }

        public async Task<List<RelationshipDetails>> GetRelationshipsAsync()
        {
            Logger.Info("Entering into HRManagement APP Service helper GetRelationshipsAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetRelationships";
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<RelationshipDetails>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetRelationshipsAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetRelationshipsAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetRelationshipsAsync method ");
                throw;
            }
        }

        public async Task<List<FacilityDetails>> GetFacilitiesAsync()
        {
            Logger.Info("Entering into HRManagement APP Service helper GetFacilitiesAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetFacilities";
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<FacilityDetails>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetFacilitiesAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetFacilitiesAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetFacilitiesAsync method ");
                throw;
            }
        }

        public async Task<List<StateDetails>> GetStatesAsync(int CountryId)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetStatesAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetStates";
                    urlParameters = "?CountryId=" + CountryId;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<StateDetails>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetStatesAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetStatesAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetStatesAsync method ");
                throw;
            }
        }

        public async Task<FacilityDetails> GetWorkFacilityDetailsAsync(int FacilityId)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetWorkFacilityDetailsAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetWorkFacilityDetails";
                    urlParameters = "?FacilityId=" + FacilityId;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<FacilityDetails>().Result;
                        Logger.Info("Exiting from into HRManagement APP Service helper GetWorkFacilityDetailsAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetWorkFacilityDetailsAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetWorkFacilityDetailsAsync method ");
                throw;
            }
        }

        public async Task<List<CityDetails>> GetCitiesAsync(int StateId)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetCitiesAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetCities";
                    urlParameters = "?StateId=" + StateId;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<CityDetails>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetCitiesAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetCitiesAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetCitiesAsync method ");
                throw;
            }
        }

        public async Task<List<FacilityDetails>> GetFacilitiesAsync(int CityId)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetFacilitiesAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetFacilities";
                    urlParameters = "?CityId=" + CityId;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<FacilityDetails>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetFacilitiesAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetFacilitiesAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetFacilitiesAsync method ");
                throw;
            }
        }

        public async Task<List<TechnologyDetails>> GetTechnologiesListAsync()
        {
            Logger.Info("Entering into HRManagement APP Service helper GetTechnologiesListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetTechnologiesList";
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<TechnologyDetails>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetTechnologiesListAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetTechnologiesListAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetTechnologiesListAsync method ");
                throw;
            }
        }

        public async Task<List<TechnologyDescriptions>> GetTechnologyDetailsListAsync(List<TechnologyDetails> technologies)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetTechnologyDetailsListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetTechnologyDetailsList";
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.PostAsJsonAsync(URL, technologies);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<TechnologyDescriptions>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetTechnologyDetailsListAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetTechnologyDetailsListAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetTechnologyDetailsListAsync method ");
                throw;
            }
        }

        public async Task<bool> CheckForExistingMasterDataValuesAsync(int masterDataType, string masterDataValue)
        {
            Logger.Info("Entering into HRManagement APP Service helper CheckForExistingMasterDataValuesAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/CheckForExistingMasterDataValues";
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
                        Logger.Info("Exiting from into HRManagement APP Service helper CheckForExistingMasterDataValuesAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper CheckForExistingMasterDataValuesAsync method ");
                        return false;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper CheckForExistingMasterDataValuesAsync method ");
                throw;
            }
        }

        public async Task<bool> CheckForExistingProjectMasterDataValuesAsync(string projectName, string technology, int refManager)
        {
            Logger.Info("Entering into HRManagement APP Service helper CheckForExistingProjectMasterDataValuesAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/CheckForExistingProjectMasterDataValues";
                    urlParameters = "?projectName=" + projectName + "&technology=" + technology + "&refManager=" + refManager;
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
                        Logger.Info("Exiting from into HRManagement APP Service helper CheckForExistingProjectMasterDataValuesAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper CheckForExistingProjectMasterDataValuesAsync method ");
                        return false;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper CheckForExistingProjectMasterDataValuesAsync method ");
                throw;
            }
        }

        public async Task<List<MasterDataModel>> GetRolesListAsync()
        {
            Logger.Info("Entering into HRManagement APP Service helper GetRolesListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetRolesList";
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<MasterDataModel>>().Result.ToList();
                        Logger.Info("Exiting from into HRManagement APP Service helper GetRolesListAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetRolesListAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetRolesListAsync method ");
                throw;
            }
        }

        public async Task<LeaveReportModel> GetProjectwiseReportAsync(int projectId, int fromMonth, int toMonth, int year)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetProjectwiseReportAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetProjectwiseReport";
                    urlParameters = "?projectId=" + projectId + "&fromMonth=" + fromMonth + "&toMonth=" + toMonth + "&year=" + year;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<LeaveReportModel>().Result;
                        Logger.Info("Exiting from into HRManagement APP Service helper GetProjectwiseReportAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetProjectwiseReportAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetProjectwiseReportAsync method ");
                throw;
            }
        }

        public async Task<List<ProjectsList>> GetProjectwiseEmployeeDetailsAsync(int projectId, int fromMonth, int toMonth, int year)
        {
            Logger.Info("Entering into HRManagement APP Service helper GetProjectwiseReportAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetProjectwiseEmployeeDetails";
                    urlParameters = "?projectId=" + projectId + "&fromMonth=" + fromMonth + "&toMonth=" + toMonth + "&year=" + year;
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
                        Logger.Info("Exiting from into HRManagement APP Service helper GetProjectwiseReportAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into HRManagement APP Service helper GetProjectwiseReportAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement APP Service helper GetProjectwiseReportAsync method ");
                throw;
            }
        }
    }
}

