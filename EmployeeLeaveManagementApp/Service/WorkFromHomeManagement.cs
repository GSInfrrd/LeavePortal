using LMS_WebAPP_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_ServiceHelpers
{
  public class WorkFromHomeManagement
    {
        static HttpClient client = new HttpClient();
        private string urlParameters;
        public async Task<long> AddNewWorkFromHomeDetailsAsync(WorkFromHomeModel model)
        {
            try
            {
                string URL = "http://localhost:64476/api/WorkFromHome/AddNewWorkFromHome";
                HttpClient client = new HttpClient();
                urlParameters = "?model=" + model;

                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PostAsJsonAsync(URL, model);
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<long>().Result;
                    return dataObjects;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<WorkFromHomeModel>> GetWorkFromHomeListAsync(int refEmpId)
        {
            try
            {
                string URL = "http://localhost:64476/api/WorkFromHome/GetWorkFromHomeList";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                urlParameters = "?EmpId=" + refEmpId;
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters); // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<List<WorkFromHomeModel>>().Result.ToList();
                    return dataObjects;

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<WorkFromHomeModel>> UpdateNewWorkFromHomeDetailsAsync(WorkFromHomeModel model)
        {
            try
            {
                string URL = "http://localhost:64476/api/WorkFromHome/UpdateWorkFromHome";
                HttpClient client = new HttpClient();
                urlParameters = "?Editmodel=" + model;

                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.PutAsJsonAsync(URL, model);
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<List<WorkFromHomeModel>>().Result;
                    return dataObjects;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<WorkFromHomeModel>> DeleteWorkFromHomeDetailsAsync(int Id)
        {
            try
            {
                string URL = "http://localhost:64476/api/WorkFromHome/DeleteWorkFromHome";
                HttpClient client = new HttpClient();
                urlParameters = "?Id=" + Id;

                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.DeleteAsync(urlParameters);
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<List<WorkFromHomeModel>>().Result;
                    return dataObjects;
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
