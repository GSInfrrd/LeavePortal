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

        private string URLGet = "http://localhost:64476/api/ResourceRequest";
        private string URLPost = "http://localhost:64476/api/ResourceRequest/SubmitResourceRequest";
        private string urlParameters;

        public async Task<ResourceDetails> GetResourceRequestFormDetails(int managerId)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URLGet);
                urlParameters = "?id=" + managerId;
                URLGet += urlParameters;

                client.BaseAddress = new Uri(URLGet);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(URLGet); // Blocking call!
                var resourceDetails = new ResourceDetails();

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    resourceDetails = response.Content.ReadAsAsync<ResourceDetails>().Result;
                    return resourceDetails;
                }
                return null;
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
    }
}
