using System;
using System.Net.Http;
using System.Threading.Tasks;
using LMS_WebAPP_Domain;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Linq;
using LMS_WebAPP_Utils;
using System.Configuration;

namespace LMS_WebAPP_ServiceHelpers
{
    public class EmployeeLeaveTransactionManagement
    {
        static HttpClient client = new HttpClient();

        private static string WebapiUrl = ConfigurationManager.AppSettings["WebApiURL"];
        private string URL = WebapiUrl + "/EmployeeLeaveTrans";
        private string URLGetRewardLeaveDetails = WebapiUrl + "/EmployeeLeaveTrans/GetRewardLeaveFormDetails";
        private string URLSubmitLeaveReward = WebapiUrl + "/EmployeeLeaveTrans/SubmitLeaveReward";
        private string urlParameters ="";

        public async Task<List<LeaveTransaction>> GetEmployeeLeaveTransactionAsync(int empid, int? leaveType = 0,int? month=0,int? transactionType=0)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement APP Service helper GetEmployeeLeaveTransactionAsync method ");
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URL);
                urlParameters = "/Get?id=" + empid + "&leaveType=" + leaveType+"&month="+month+"&transactionType="+transactionType;

                URL += urlParameters;
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<IList<LeaveTransaction>>().Result.ToList();
                    Logger.Info("Exiting from into EmployeeLeaveTransactionManagement APP Service helper GetEmployeeLeaveTransactionAsync method ");
                    return dataObjects;

                }
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement APP Service helper GetEmployeeLeaveTransactionAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement APP Service helper GetEmployeeLeaveTransactionAsync method ");
                throw;
            }
        }

        public async Task<IList<LeaveTransaction>> SubmitLeaveRequestAsync(int id, int leaveType, string fromDate, string toDate, string comments, double workingDays)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement APP Service helper SubmitLeaveRequestAsync method ");
            try
            {
               
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            var urlParameters = "/applyLeave?Id=" + id + "&leaveType=" + leaveType + "&fromDate=" + fromDate + "&toDate=" + toDate + "&comments=" + comments + "&workingDays=" + workingDays;
            URL += urlParameters;
            //URL = URL + "/SubmitLeaveRequest";


            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(URL);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<IList<LeaveTransaction>>().Result.ToList();
                    Logger.Info("Exiting from into EmployeeLeaveTransactionManagement APP Service helper SubmitLeaveRequestAsync method ");
                    return dataObjects;

            }
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement APP Service helper SubmitLeaveRequestAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement APP Service helper SubmitLeaveRequestAsync method ");
                throw;
            }
        }

        public async Task<IList<LeaveTransaction>> SubmitLeaveForApprovalAsync(int id)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement APP Service helper SubmitLeaveForApprovalAsync method ");
            try
            {
                HttpClient client = new HttpClient();
            var urlParameters = "?id=" + id + "&status=" + true;
            //urlParameters = "?empId=" + empId + "&year=" + year;
            //URL = URL + "/SubmitLeaveRequest";
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                var dataObjects = response.Content.ReadAsAsync<IList<LeaveTransaction>>().Result.ToList();
                    Logger.Info("Exiting from into EmployeeLeaveTransactionManagement APP Service helper SubmitLeaveForApprovalAsync method ");
                    return dataObjects;

            }
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement APP Service helper SubmitLeaveForApprovalAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement APP Service helper SubmitLeaveForApprovalAsync method ");
                throw;
            }
        }


        public async Task<IList<LeaveTransaction>> DeleteLeaveRequestAsync(int leaveId, int empId)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement APP Service helper DeleteLeaveRequestAsync method ");
            try
            {
                HttpClient client = new HttpClient();
            var urlParameters = "?leaveId=" + leaveId + "&employeeId=" + empId;
            //URL = URL + "/SubmitLeaveRequest";
            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<IList<LeaveTransaction>>().Result.ToList();
                    Logger.Info("Exiting from into EmployeeLeaveTransactionManagement APP Service helper DeleteLeaveRequestAsync method ");
                    return dataObjects;

            }
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement APP Service helper DeleteLeaveRequestAsync method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement APP Service helper DeleteLeaveRequestAsync method ");
                throw;
            }
        }

        public async Task<LeaveTransactionResponse> CheckLeaveAvailabilityAsync(int employeeId, DateTime fromDate, DateTime toDate,int leaveType)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement APP Service helper CheckLeaveAvailabilityAsync method ");

            try
            {
                HttpClient client = new HttpClient();
                URL = WebapiUrl + "/AddLeave/CheckLeaveAvailability";
                var urlParameters = "?employeeId=" + employeeId + "&fromDate=" + fromDate + "&toDate=" + toDate + "&leaveType=" + leaveType; ;

                client.BaseAddress = new Uri(URL);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = await client.GetAsync(urlParameters);  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    var dataObjects = response.Content.ReadAsAsync<LeaveTransactionResponse>().Result;
                    return dataObjects;

                }
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement APP Service helper CheckLeaveAvailabilityAsync method ");
                throw;
            }
        }
       
        public RewardLeaveModel GetRewardLeaveFormDetails()
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement APP Service helper GetRewardLeaveFormDetails method ");
            try
            {
                RewardLeaveModel leaveModel = new RewardLeaveModel();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(URLGetRewardLeaveDetails);
               
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync(URLGetRewardLeaveDetails).Result;  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    leaveModel = response.Content.ReadAsAsync<RewardLeaveModel>().Result;
                    Logger.Info("Exiting from into EmployeeLeaveTransactionManagement APP Service helper GetRewardLeaveFormDetails method ");
                    return leaveModel;

                }
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement APP Service helper GetRewardLeaveFormDetails method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement APP Service helper GetEmployeeLeaveTransactionAsync method ");
                throw;
            }
        }

        public bool SubmitLeaveReward(RewardLeaveModel model)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement APP Service helper GetRewardLeaveFormDetails method ");
            try
            {
                bool leaveRewarded = false;
                HttpClient client = new HttpClient();
                urlParameters = "?model=" + model;
                client.BaseAddress = new Uri(URLSubmitLeaveReward);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.PostAsJsonAsync(URLSubmitLeaveReward, model).Result; // Blocking call!

                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    leaveRewarded = response.Content.ReadAsAsync<bool>().Result;

                    Logger.Info("Exiting from into ResourceManagement APP Service helper SubmitResourceRequest method ");
                    return leaveRewarded;
                }
                Logger.Info("Exiting from into ResourceManagement APP Service helper SubmitResourceRequest method ");
                return leaveRewarded;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
