using Domain;
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
                var resourceDetails = new ResourceDetails();
                resourceDetails = resourceRequestmanagement.GetResourceRequestFormDetails();

                return resourceDetails;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
