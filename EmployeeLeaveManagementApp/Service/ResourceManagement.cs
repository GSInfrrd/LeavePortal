using LMS_WebAPP_Domain;
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
        private string URLGetResourcesToRespond = "http://localhost:64476/api/ResourceRequest/ResourceRequestsToRespond";
        private string URLPost = "http://localhost:64476/api/ResourceRequest/SubmitResourceRequest";
        private string URLPostRequestResponse = "http://localhost:64476/api/ResourceRequest/SubmitResourceRequestsResponse";
        private string urlParameters;

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
                        return dataObjects;
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
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
                    return resourceDetailModel;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ResourceRequestDetailModel>> GetResourceRequests(int hrId)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URLGetResourcesToRespond);
                urlParameters = "?id=" + hrId;
                URLGetResourcesToRespond += urlParameters;

                client.BaseAddress = new Uri(URLGetResourcesToRespond);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(URLGetResourcesToRespond); // Blocking call!                

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var lstResourceDetails = response.Content.ReadAsAsync<List<ResourceRequestDetailModel>>().Result;
                    return lstResourceDetails;
                }
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
                    return resourceResponseModel;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
