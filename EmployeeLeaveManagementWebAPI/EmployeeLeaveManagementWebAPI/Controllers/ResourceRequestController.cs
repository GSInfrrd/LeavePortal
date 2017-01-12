using Domain;
using LMS_WebAPI_Utils;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class ResourceRequestController : ApiController
    {
        ResourceRequestManagement resourceRequestmanagement = new ResourceRequestManagement();
        public ResourceDetails Get()
        {
            try
            {
                Logger.Info("Entering in ResourceRequestController API Get method");
                var resourceDetails = new ResourceDetails();
                resourceDetails = resourceRequestmanagement.GetResourceRequestFormDetails();
                Logger.Info("Successfully exiting from ResourceRequestController API Get method");
                return resourceDetails;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController API Get method.", ex);
                throw;
            }
        }
    }
}
