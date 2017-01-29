using LMS_WebAPP_Domain;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_ServiceHelpers
{
    public class ResourceManagement
    {
        static HttpClient client = new HttpClient();

        private static string WebapiURL = ConfigurationManager.AppSettings["WebApiURL"]+ "/ResourceRequest";
        private string URLGetResourcesFormDetails = WebapiURL + "/GetResourceDetails";
        private string URLGetResources = WebapiURL + "/ResourceRequests";
        private string URLPost = WebapiURL + "/SubmitResourceRequest";
        private string URLPostRequestResponse = WebapiURL + "/SubmitResourceRequestsResponse";
        private string URLDeleteRequest = WebapiURL + "/DeleteResourceRequest";
        private string urlParameters;
        private string urlParameters1;
        private string urlParameters2;

        public ResourceDetails GetResourceRequestFormDetails(int managerId)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    urlParameters = "?id=" + managerId;
                    client.BaseAddress = new Uri(URLGetResourcesFormDetails);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call!

                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<ResourceDetails>().Result;

                        Logger.Info("Exiting from into ResourceManagement APP Service helper GetResourceRequestFormDetails method ");
                        return dataObjects;
                    }
                    else
                    {
                        Logger.Info("Exiting from into ResourceManagement APP Service helper GetResourceRequestFormDetails method ");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResourceDetails SubmitResourceRequest(ResourceRequestDetailModel model)
        {
            try
            {
                var resourceRequests = new ResourceDetails();
                HttpClient client = new HttpClient();
                urlParameters = "?model=" + model;
                client.BaseAddress = new Uri(URLPost);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.PostAsJsonAsync(URLPost, model).Result; // Blocking call!

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    resourceRequests = response.Content.ReadAsAsync<ResourceDetails>().Result;

                    Logger.Info("Exiting from into ResourceManagement APP Service helper SubmitResourceRequest method ");
                    return resourceRequests;
                }
                Logger.Info("Exiting from into ResourceManagement APP Service helper SubmitResourceRequest method ");
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResourceDetails GetResourceRequests(int userId, bool viewAll)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URLGetResources);
                urlParameters1 = "/" + userId;
                urlParameters2 = "/" + viewAll;
                URLGetResources += urlParameters1 + urlParameters2;

                client.BaseAddress = new Uri(URLGetResources);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(URLGetResources).Result; // Blocking call!                

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var resourceRequests = response.Content.ReadAsAsync<ResourceDetails>().Result;

                    Logger.Info("Exiting from into ResourceManagement APP Service helper GetResourceRequests method ");
                    return resourceRequests;
                }
                Logger.Info("Exiting from into ResourceManagement APP Service helper GetResourceRequests method ");
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool RespondToResourceRequests(ResourceRequestDetailModel model)
        {
            try
            {
                bool result = false;
                HttpClient client = new HttpClient();
                urlParameters = "?model=" + model;
                client.BaseAddress = new Uri(URLPostRequestResponse);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.PostAsJsonAsync(URLPostRequestResponse, model).Result; // Blocking call!

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    result = response.Content.ReadAsAsync<bool>().Result;

                    Logger.Info("Exiting from into ResourceManagement APP Service helper RespondToResourceRequests method ");
                    return result;
                }
                Logger.Info("Exiting from into ResourceManagement APP Service helper RespondToResourceRequests method ");
                return result;
            }
            catch
            {

                throw;
            }
        }

        public ResourceDetails DeleteResourceRequest(string ticket, int userId)
        {
            var resourceRequest = new ResourceDetails();
            resourceRequest.Result = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URLDeleteRequest);
                urlParameters1 = "?id=" + ticket;
                urlParameters2 = "&userId=" + userId;
                URLDeleteRequest += urlParameters1 + urlParameters2;
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(URLDeleteRequest).Result;  // Blocking call!

                if (response.IsSuccessStatusCode)
                {
                    resourceRequest = response.Content.ReadAsAsync<ResourceDetails>().Result;
                    Logger.Info("Exiting from into ResourceManagement APP Service helper DeleteResourceRequest method ");
                    return resourceRequest;
                }
                Logger.Info("Exiting from into ResourceManagement APP Service helper DeleteResourceRequest method ");
                return resourceRequest;
            }
            catch
            {
                Logger.Info("Exception occured at ResourceManagement APP Service helper GetProjectsListAsync method ");
                throw;
            }
        }

        public async Task<List<TeamMembers>> GetProjectMembersListAsync(int projectId)
        {
            Logger.Info("Entering into ResourceManagement APP Service helper GetProjectMembersListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetProjectMembersList";
                    urlParameters = "?projectId=" + projectId;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<TeamMembers>>().Result.ToList();
                        Logger.Info("Exiting from into ResourceManagement APP Service helper GetProjectMembersListAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into ResourceManagement APP Service helper GetProjectMembersListAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Info("Exception occured at ResourceManagement APP Service helper GetProjectsListAsync method ");
                throw;
            }
        }

        public async Task<List<TeamMembers>> RemoveProjectResourceAsync(int employeeProjectId, int projectId)
        {
            Logger.Info("Entering into ResourceManagement APP Service helper RemoveProjectResourceAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/RemoveProjectResource";
                    urlParameters = "?employeeProjectId=" + employeeProjectId + "&projectId=" + projectId;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<TeamMembers>>().Result.ToList();
                        Logger.Info("Exiting from into ResourceManagement APP Service helper RemoveProjectResourceAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into ResourceManagement APP Service helper RemoveProjectResourceAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Info("Exception occured at ResourceManagement APP Service helper RemoveProjectResourceAsync method ");
                throw;
            }
        }

        public async Task<List<TeamMembers>> GetResourceListAsync(int refProject)
        {
            Logger.Info("Entering into ResourceManagement APP Service helper GetResourceListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/GetResourceList";
                    urlParameters = "?refProject=" + refProject;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<TeamMembers>>().Result.ToList();
                        Logger.Info("Exiting from into ResourceManagement APP Service helper GetResourceListAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into ResourceManagement APP Service helper GetResourceListAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Info("Exception occured at ResourceManagement APP Service helper GetProjectsListAsync method ");
                throw;
            }
        }

        public async Task<List<TeamMembers>> AddNewProjectResourceAsync(int employeeId, int projectId)
        {
            Logger.Info("Entering into ResourceManagement APP Service helper AddNewProjectResourceAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string URL = WebapiURL + "/AddNewProjectResource";
                    urlParameters = "?employeeId=" + employeeId + "&projectId=" + projectId;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = response.Content.ReadAsAsync<List<TeamMembers>>().Result.ToList();
                        Logger.Info("Exiting from into ResourceManagement APP Service helper AddNewProjectResourceAsync method ");
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                        Logger.Info("Exiting from into ResourceManagement APP Service helper AddNewProjectResourceAsync method ");
                        return null;
                    }
                }
            }
            catch
            {
                Logger.Info("Exception occured at ResourceManagement APP Service helper AddNewProjectResourceAsync method ");
                throw;
            }
        }

    }
}
