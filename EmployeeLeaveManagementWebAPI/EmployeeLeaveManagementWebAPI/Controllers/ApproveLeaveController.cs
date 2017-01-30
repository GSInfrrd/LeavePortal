﻿using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
//using LMS_WebAPI_DAL;

namespace EmployeeLeaveManagementWebAPI.Controllers
{

    public class ApproveLeaveController : ApiController
    {
        // GET: ApproveLeave

        public List<EmployeeDetailsModel> Get(int id, int st)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveController API Get method");
                ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                var res = ALM.GetAllManagers(id, st);
                Logger.Info("Successfully exiting from ApproveLeaveController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController API Get method.", ex);
                return null;
            }

        }
        public List<ApproveLeaveModel> Get(int id)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveController API Get method");
                ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                var res = ALM.GetApproveLeave(id);
                Logger.Info("Successfully exiting from ApproveLeaveController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController API Get method.", ex);
                return null;
            }

        }
        
        public List<ApproveLeaveModel> GetTakeActionOnEmployeeLeave(int Leaveid, string Leavecomments, string Leavestatus, int Approverid)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveController API Get method");
                ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                var EmployeeLeaveApproved = ALM.TakeActionOnEmployeeLeave(Leaveid, Leavecomments, Leavestatus, Approverid);

                // Send Mail 
                Thread MailThread = new Thread(() => SendMailForTakeActionOnEmployeeLeave(EmployeeLeaveApproved, Leaveid, Leavestatus));
                MailThread.Start();

                var res = new List<ApproveLeaveModel>();
                if (EmployeeLeaveApproved)
                {
                    res = ALM.GetApproveLeave(Leaveid);
                }
                Logger.Info("Successfully exiting from ApproveLeaveController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController API Get method.", ex);
                return null;
            }

        }

        private void SendMailForTakeActionOnEmployeeLeave(bool EmployeeLeaveApproved, int Leaveid, string Leavestatus)
        {
            if (EmployeeLeaveApproved)
            {
                ActionsForMail actionName = ActionsForMail.ApproveLeave;
                if (Leavestatus == "Approved")
                {
                    actionName = ActionsForMail.ApproveLeave;
                }
                else if (Leavestatus == "Rejected")
                {
                    actionName = ActionsForMail.RejectLeave;
                }
                else if (Leavestatus == "Reassigned")
                {
                    actionName = ActionsForMail.ReassignLeave;
                }
                MailManagement MM = new MailManagement();
                var MailDetails = MM.GetMailTemplateForTakeActionOnLeave(actionName, Leaveid);
                string TemplatePath = MailDetails.TemplatePath;

                string body;
                //Read template file from the App_Data folder
                using (var sr = new StreamReader(HostingEnvironment.MapPath(TemplatePath)))
                {
                    body = sr.ReadToEnd();
                }

                var logoPath = HostingEnvironment.MapPath("~/Content/Images/infrrd-logo-main.png");

                string EmployeeName = MailDetails.EmployeeName.Substring(0, MailDetails.EmployeeName.IndexOf(" "));
                string messageBody;
                if ((Leavestatus == "Approved") || (Leavestatus == "Rejected"))
                {
                    messageBody = string.Format(body, EmployeeName, MailDetails.ManagerName, MailDetails.LeaveFromDate, MailDetails.LeaveToDate, MailDetails.NumberOfWorkingDays, MailDetails.ManagerComments);
                }
                else
                {
                    messageBody = string.Format(body, EmployeeName, MailDetails.ManagerName, MailDetails.LeaveFromDate, MailDetails.LeaveToDate, MailDetails.NumberOfWorkingDays, MailDetails.ManagerComments, MailDetails.NewManagerName);

                    //Send Mail to New Manager for the applied leaves by employee
                    ActionsForMail ApplyLeaveactionName = ActionsForMail.ApplyLeave;
                    var ApplyLeaveMailDetails = MM.GetMailTemplateForLeaveApplied(ApplyLeaveactionName, MailDetails.EmployeeId);
                    string ApplyLeaveTemplatePath = ApplyLeaveMailDetails.TemplatePath;

                    string ApplyLeavebody;
                    //Read template file from the App_Data folder
                    using (var sr = new StreamReader(HostingEnvironment.MapPath(ApplyLeaveTemplatePath)))
                    {
                        ApplyLeavebody = sr.ReadToEnd();
                    }
                    string NewManagerName = MailDetails.NewManagerName.Substring(0, MailDetails.NewManagerName.IndexOf(" "));
                    string ApplyLeavemessageBody = string.Format(ApplyLeavebody, NewManagerName, MailDetails.EmployeeName, MailDetails.LeaveFromDate, MailDetails.LeaveToDate, MailDetails.NumberOfWorkingDays, MailDetails.EmployeeComments);

                    MailUtility.sendmail(MailDetails.NewManagerMailId, MailDetails.CcMailId, ApplyLeaveactionName.Description(), ApplyLeavemessageBody, logoPath);
                }

                MailUtility.sendmail(MailDetails.ToMailId, MailDetails.CcMailId, actionName.Description(), messageBody, logoPath);

            }

        }
    }
}