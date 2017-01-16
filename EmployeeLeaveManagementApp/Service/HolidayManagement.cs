using LMS_WebAPP_Domain;
using LMS_WebAPP_Utils;
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
            Logger.Info("Entering into HolidayManagement APP Service helper AddNewHolidayDetailsAsync method ");
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
                    Logger.Info("Exiting from into HolidayManagement APP Service helper AddNewHolidayDetailsAsync method ");
                    return dataObjects;
                }
                Logger.Info("Exiting from into HolidayManagement APP Service helper AddNewHolidayDetailsAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at HolidayManagement APP Service helper AddNewHolidayDetailsAsync method ");
                throw;
            }
        }

        public async Task<IList<HolidayModel>> GetHolidayListAsync()
        {
            Logger.Info("Entering into HolidayManagement APP Service helper GetHolidayListAsync method ");
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
                    Logger.Info("Exiting from into HolidayManagement APP Service helper GetHolidayListAsync method ");
                    return dataObjects;

                }
                Logger.Info("Exiting from into HolidayManagement APP Service helper GetHolidayListAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at HolidayManagement APP Service helper GetHolidayListAsync method ");
                throw;
            }
        }

        public async Task<List<HolidayModel>> UpdateNewHolidayDetailsAsync(HolidayModel model)
        {
            Logger.Info("Entering into HolidayManagement APP Service helper UpdateNewHolidayDetailsAsync method ");
            try
            {
                string URL = "http://localhost:64476/api/Holiday/UpdateHoliday";
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
                    var dataObjects = response.Content.ReadAsAsync<List<HolidayModel>>().Result;
                    Logger.Info("Exiting from into HolidayManagement APP Service helper UpdateNewHolidayDetailsAsync method ");
                    return dataObjects;
                }
                Logger.Info("Exiting from into HolidayManagement APP Service helper UpdateNewHolidayDetailsAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at HolidayManagement APP Service helper UpdateNewHolidayDetailsAsync method ");
                throw;
            }
        }

        public async Task<List<HolidayModel>> DeleteHolidayDetailsAsync(int Id)
        {
            Logger.Info("Entering into HolidayManagement APP Service helper DeleteHolidayDetailsAsync method ");
            try
            {
                string URL = "http://localhost:64476/api/Holiday/DeleteHoliday";
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
                    var dataObjects = response.Content.ReadAsAsync<List<HolidayModel>>().Result;
                    Logger.Info("Exiting from into HolidayManagement APP Service helper DeleteHolidayDetailsAsync method ");
                    return dataObjects;
                }
                Logger.Info("Exiting from into HolidayManagement APP Service helper DeleteHolidayDetailsAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at HolidayManagement APP Service helper DeleteHolidayDetailsAsync method ");
                throw;
            }
        }

        public async Task<List<CalendarEvents>> GetCalendarEventsAsync(int employeeId)
        {
            Logger.Info("Entering into HolidayManagement APP Service helper GetCalendarEventsAsync method ");
            try
            {
                string URL = "http://localhost:64476/api/Holiday/GetCalendarEvents";
                URL += "?employeeId=" + employeeId;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                // Add an Accept header for JSON format.
                //client.DefaultRequestHeaders.Accept.Add(
                //new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(URL); // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<List<CalendarEvents>>().Result.ToList();
                    Logger.Info("Exiting from into HolidayManagement APP Service helper GetCalendarEventsAsync method ");
                    return dataObjects;

                }
                Logger.Info("Exiting from into HolidayManagement APP Service helper GetCalendarEventsAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at HolidayManagement APP Service helper GetCalendarEventsAsync method ");
                throw;
            }
        }
    }
}
