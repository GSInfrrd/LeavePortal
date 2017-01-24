﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    [RoutePrefix("api/employeeleavetrans")]
    public class EmployeeLeaveTransController : ApiController
    {
        EmployeeLeaveTransactionManagement leaveManagement = new EmployeeLeaveTransactionManagement();

        // GET api/values
        public List<EmployeeLeaveTransactionModel> Get(int id, int? leaveType = 0,int? month=0,int? transactionType=0)
        {
            try
            {
                Logger.Info("Entering in EmployeeLeaveTransController API Get method");
                int leaveTypeConverted = Convert.ToInt16(leaveType);
                int monthConverted = Convert.ToInt16(month);
                int transactionTypeConverted = Convert.ToInt16(transactionType);
                var res = leaveManagement.GetEmployeeLeaveTransaction(id, leaveTypeConverted,monthConverted,transactionTypeConverted);
                Logger.Info("Successfully exiting from EmployeeLeaveTransController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at EmployeeLeaveTransController API Get method.", ex);
                return null;
            }
        }

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}
        [Route("applyleave")]
        public List<EmployeeLeaveTransactionModel> Get(int id, int leaveType, string fromDate, string toDate, string comments, double workingDays)
        {
            try
            {
                Logger.Info("Entering in EmployeeLeaveTransController API Get method");
                var detailsInserted = leaveManagement.InsertEmployeeLeaveDetails(id, leaveType, fromDate, toDate, comments, workingDays);
                var res = new List<EmployeeLeaveTransactionModel>();
                Logger.Info("Successfully exiting from EmployeeLeaveTransController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at EmployeeLeaveTransController API Get method.", ex);
                return null;
            }
        }

        public List<EmployeeLeaveTransactionModel> Get(int id, bool status)
        {
            try
            {
                Logger.Info("Entering in EmployeeLeaveTransController API Get method");
                var detailsInserted = leaveManagement.SubmitLeaveForApproval(id);
                var res = new List<EmployeeLeaveTransactionModel>();
               if (detailsInserted)
               {
                   res = leaveManagement.GetEmployeeLeaveTransaction(id);
               }
                Logger.Info("Successfully exiting from EmployeeLeaveTransController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at EmployeeLeaveTransController API Get method.", ex);
                return null;
            }
        }

        public List<EmployeeLeaveTransactionModel> Get(int leaveId, int employeeId)
        {
            try
            {
                Logger.Info("Entering in EmployeeLeaveTransController API Get method");
                var detailsInserted = leaveManagement.DeleteLeaveRequest(leaveId);
                var res = new List<EmployeeLeaveTransactionModel>();
                if (detailsInserted)
               {
                  res = leaveManagement.GetEmployeeLeaveTransaction(employeeId);
               }
                Logger.Info("Successfully exiting from EmployeeLeaveTransController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at EmployeeLeaveTransController API Get method.", ex);
                return null;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetRewardLeaveFormDetails")]
        public RewardLeaveModel GetRewardLeaveFormDetails()
        {
            try
            {
                Logger.Info("Entering in GetRewardLeaveFormDetails API Get method");

                var rewardLeaveDetails = leaveManagement.GetRewardLeaveDetails();
                
                Logger.Info("Successfully exiting from GetRewardLeaveFormDetails API Get method");
                return rewardLeaveDetails;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at GetRewardLeaveFormDetails API Get method.", ex);
                return null;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SubmitLeaveReward")]
        public bool SubmitLeaveReward(RewardLeaveModel model)
        {
            try
            {
                Logger.Info("Entering in GetRewardLeaveFormDetails API Get method");

                bool leaveRewarded = false;
                leaveRewarded = leaveManagement.SubmitLeaveRewardManagement(model);

                Logger.Info("Successfully exiting from GetRewardLeaveFormDetails API Get method");
                return leaveRewarded;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at GetRewardLeaveFormDetails API Get method.", ex);
                return false;
            }
        }
    }
}
