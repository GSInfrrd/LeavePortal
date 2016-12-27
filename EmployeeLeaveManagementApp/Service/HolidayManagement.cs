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
    public class HolidayManagement
    {
        static HttpClient client = new HttpClient();
        private string urlParameters;

        public async Task<List<HolidayModel>> AddNewHolidayDetailsAsync(HolidayModel model)
        {
            try
            {
                string URL = "http://localhost:64476/api/Holiday/AddNewHoliday";
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
                    var dataObjects = response.Content.ReadAsAsync<List<HolidayModel>>().Result;
                    return dataObjects;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<HolidayModel>> GetHolidayListAsync()
        {
            try
            {
                string URL = "http://localhost:64476/api/Holiday/GetHolidayList";
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
                    var dataObjects = response.Content.ReadAsAsync<List<HolidayModel>>().Result.ToList();
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
