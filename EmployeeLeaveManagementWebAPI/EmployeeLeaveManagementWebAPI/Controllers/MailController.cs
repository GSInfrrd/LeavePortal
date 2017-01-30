using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class MailController : ApiController
    {
        public void SendMail()
        {
            try
            {
                Logger.Info("Entering in MailController API SendMail method");
                MailUtility MU = new MailUtility();
                //MU.sendmail();
                Logger.Info("Successfully exiting from MailController API SendMail method");
            }
            catch (Exception ex)
            {
                Logger.Error("Error at MailController API SendMail method.", ex);
            }
        }
    }
}