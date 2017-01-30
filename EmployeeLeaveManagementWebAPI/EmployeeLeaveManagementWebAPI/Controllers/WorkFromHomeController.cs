using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Web.Hosting;
using System.Web.Http;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    [RoutePrefix("api/workfromhome")]
    public class WorkFromHomeController : ApiController
    {
        WorkFromHomeManagement WorkFromHomeManager = new WorkFromHomeManagement();

        [System.Web.Http.HttpPost]
        public long AddNewWorkFromHome(WorkFromHomeModel model)
        {
            try
            {
                Logger.Info("Entering in WorkFromHomeController API AddNewWorkFromHome method");
                var result = WorkFromHomeManager.AddNewWorkFromHome(model);
                // Send Mail 
                Thread MailThread = new Thread(() => SendMailForWorkFromHome(result, model.RefEmployeeId, model));
                MailThread.Start();
                Logger.Info("Successfully exiting from WorkFromHomeController API AddNewWorkFromHome method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController API AddNewWorkFromHome method.", ex);
                throw ex;
            }
        }

        private void SendMailForWorkFromHome(long result, int id, WorkFromHomeModel model)
        {
            if (result!=0)
            {
                ActionsForMail actionName = ActionsForMail.WorkFromHome;
                MailManagement MM = new MailManagement();
                var MailDetails = MM.GetMailTemplateForLeaveApplied(actionName, id);
                string TemplatePath = MailDetails.TemplatePath;

                string body;
                //Read template file from the App_Data folder
                using (var sr = new StreamReader(HostingEnvironment.MapPath(TemplatePath)))
                {
                    body = sr.ReadToEnd();
                }
                var logoPath = HostingEnvironment.MapPath("~/Content/Images/infrrd-logo-main.png");
                WorkFormHomeReasons Reason = (WorkFormHomeReasons)model.RefReason;
                string WorkFromHomeReason = (model.OtherReason != "") ? model.OtherReason : Reason.Description();
                string messageBody = string.Format(body, MailDetails.ManagerName, MailDetails.EmployeeName,WorkFromHomeReason);
                string CcMailId = MailDetails.CcMailId + "," + ConfigurationManager.AppSettings["HRMailId"];
                MailUtility.sendmail(MailDetails.ToMailId, CcMailId, actionName.Description(), messageBody, logoPath);
            }

        }

        [System.Web.Http.HttpGet]
        public IList<WorkFromHomeModel> GetWorkFromHomeList(int EmpId)
        {
            try
            {
                Logger.Info("Entering in WorkFromHomeController API GetWorkFromHomeList method");
                var result = WorkFromHomeManager.GetWorkFromHomeList(EmpId);
                Logger.Info("Successfully exiting from WorkFromHomeController API GetWorkFromHomeList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController API GetWorkFromHomeList method.", ex);
                throw ex;
            }
        }

        [System.Web.Http.HttpDelete]
        public long DeleteWorkFromHome(long Id)
        {
            try
            {
                Logger.Info("Entering in WorkFromHomeController API DeleteWorkFromHome method");
                Logger.Info("Successfully exiting from WorkFromHomeController API DeleteWorkFromHome method");
                return WorkFromHomeManager.DeleteWorkFromHome(Id);
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController API DeleteWorkFromHome method.", ex);
                throw ex;
            }
        }

        [System.Web.Http.HttpPut]
        public bool UpdateWorkFromHome(WorkFromHomeModel model)
        {
            try
            {
                Logger.Info("Entering in WorkFromHomeController API UpdateWorkFromHome method");
                Logger.Info("Successfully exiting from WorkFromHomeController API UpdateWorkFromHome method");
                return WorkFromHomeManager.UpdateWorkFromHome(model);
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController API UpdateWorkFromHome method.", ex);
                throw ex;
            }
        }

        [Route("GetWorkFromHomeReasonsList")]
        public List<WorkFromHomeReasonModel> GetWorkFromHomeReasonsList()
        {
            try
            {
                Logger.Info("Entering in WorkFromHomeController API GetWorkFromHomeReasonList method");
                var result = WorkFromHomeManager.GetWorkFromHomeReasonsList();
                Logger.Info("Successfully exiting from WorkFromHomeController API GetWorkFromHomeReasonList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController API GetWorkFromHomeReasonList method.", ex);
                throw ex;
            }
        }

    }
}