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

        private string URL = "http://localhost:64476/api/ResourceRequest";
        private string urlParameters;

        public async Task<ResourceDetails> GetResourceRequestFormDetails()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync(URL); // Blocking call!
            var resourceDetails = new ResourceDetails();

            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                resourceDetails = response.Content.ReadAsAsync<ResourceDetails>().Result;
                return resourceDetails;

            }
            return null;
        }
    }
}
