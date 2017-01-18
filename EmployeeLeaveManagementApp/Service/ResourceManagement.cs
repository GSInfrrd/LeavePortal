using LMS_WebAPP_Domain;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ResourceManagement
    {
        static HttpClient client = new HttpClient();

        private string URLGetResourcesFormDetails = "http://localhost:64476/api/ResourceRequest/GetResourceDetails";
        private string URLGetResources = "http://localhost:64476/api/ResourceRequest/ResourceRequests";
        private string URLPost = "http://localhost:64476/api/ResourceRequest/SubmitResourceRequest";
        private string URLPostRequestResponse = "http://localhost:64476/api/ResourceRequest/SubmitResourceRequestsResponse";
        private string URLDeleteRequest = "http://localhost:64476/api/ResourceRequest/DeleteResourceRequest";
        private string urlParameters;
        private string urlParameters1;
        private string urlParameters2;

        public async Task<ResourceDetails> GetResourceRequestFormDetails(int managerId)
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
                    HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!

                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body. Blocking!
                        var dataObjects = await response.Content.ReadAsAsync<ResourceDetails>();

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

        public async Task<ResourceRequestDetailModel> SubmitResourceRequest(ResourceRequestDetailModel model)
        {
            try
            {
                var resourceDetailModel = new ResourceRequestDetailModel();
                HttpClient client = new HttpClient();
                urlParameters = "?model=" + model;
                client.BaseAddress = new Uri(URLPost);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URLPost, model); // Blocking call!

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    resourceDetailModel = response.Content.ReadAsAsync<ResourceRequestDetailModel>().Result;

                    Logger.Info("Exiting from into ResourceManagement APP Service helper SubmitResourceRequest method ");
                    return resourceDetailModel;
                }
                Logger.Info("Exiting from into ResourceManagement APP Service helper SubmitResourceRequest method ");
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ResourceRequestDetailModel>> GetResourceRequests(int userId, bool viewAll)
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
                HttpResponseMessage response = await client.GetAsync(URLGetResources); // Blocking call!                

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var lstResourceDetails = response.Content.ReadAsAsync<List<ResourceRequestDetailModel>>().Result;

                    Logger.Info("Exiting from into ResourceManagement APP Service helper GetResourceRequests method ");
                    return lstResourceDetails;
                }
                Logger.Info("Exiting from into ResourceManagement APP Service helper GetResourceRequests method ");
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<ResourceRequestDetailModel> RespondToResourceRequests(ResourceRequestDetailModel model)
        {
            try
            {
                var resourceResponseModel = new ResourceRequestDetailModel();
                HttpClient client = new HttpClient();
                urlParameters = "?model=" + model;
                client.BaseAddress = new Uri(URLPostRequestResponse);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URLPostRequestResponse, model); // Blocking call!

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    resourceResponseModel = response.Content.ReadAsAsync<ResourceRequestDetailModel>().Result;

                    Logger.Info("Exiting from into ResourceManagement APP Service helper RespondToResourceRequests method ");
                    return resourceResponseModel;
                }
                Logger.Info("Exiting from into ResourceManagement APP Service helper RespondToResourceRequests method ");
                return null;
            }
            catch
            {

                throw;
            }
        }

        public async Task<bool> DeleteResourceRequest(string ticket)
        {
            bool deleted = false;
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URLDeleteRequest);
                urlParameters = "?id=" + ticket;
                URLDeleteRequest += urlParameters;
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(URLDeleteRequest);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    deleted = response.Content.ReadAsAsync<bool>().Result;
                    Logger.Info("Exiting from into ResourceManagement APP Service helper DeleteResourceRequest method ");
                    return deleted;
                }
                Logger.Info("Exiting from into ResourceManagement APP Service helper DeleteResourceRequest method ");
                return deleted;
            }
            catch
            {

                return false;
            }
        }

        public async Task<List<TeamMembers>> GetProjectMembersListAsync(int projectId)
        {
            Logger.Info("Entering into ResourceManagement APP Service helper GetProjectMembersListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    const string URL = "http://localhost:64476/api/ResourceRequest/GetProjectMembersList";
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
                    const string URL = "http://localhost:64476/api/ResourceRequest/RemoveProjectResource";
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

        public async Task<List<TeamMembers>> GetResourceListAsync()
        {
            Logger.Info("Entering into ResourceManagement APP Service helper GetResourceListAsync method ");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    const string URL = "http://localhost:64476/api/ResourceRequest/GetResourceList";
                    //urlParameters = "?projectId=" + projectId;
                    client.BaseAddress = new Uri(URL);
                    // Add an Accept header for JSON format.
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                    // List data response.
                    HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
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
                    const string URL = "http://localhost:64476/api/ResourceRequest/AddNewProjectResource";
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
