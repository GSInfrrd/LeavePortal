using System;
using System.Net.Http;
using System.Threading.Tasks;
using LMS_WebAPP_Domain;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace LMS_WebAPP_ServiceHelpers
{
    public class HRManagement
    {
        static HttpClient client = new HttpClient();


        private string URL = "http://localhost:64476/api/HR";
        private string urlParameters;

        public async Task<bool> SubmitEmployeeDetailsAsync(EmployeeDetailsModel model)
        {
            HttpClient client = new HttpClient();       
            urlParameters = "?model=" + model;
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
                return dataObjects;

            }
            return false;
        }

        public async Task<List<EmployeeDetailsModel>> GetEmployeeListAsync()
        {
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
                return dataObjects;

            }
            return null;
        }

        public async Task<List<EmployeeDetailsModel>> GetManagerListAsync(int refLevel)
        {
            using (HttpClient client = new HttpClient())
            {
                const string URL = "http://localhost:64476/api/HR/GetManagerList";
                urlParameters = "?refLevel=" + refLevel;
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
