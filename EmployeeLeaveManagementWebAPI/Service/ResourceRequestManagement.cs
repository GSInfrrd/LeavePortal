using Domain;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ResourceRequestManagement
    {
        private IResourceRequestRepository _resourceRequest = new ResourceRequestRepository();
        public ResourceDetails GetResourceRequestFormDetails()
        {
            Logger.Info("Entering into ResourceRequestManagement Service helper GetResourceRequestFormDetails method ");
            try
            {
                var resourceDetails = new ResourceDetails();
                resourceDetails = _resourceRequest.GetResourceRequestFormDetails();
                Logger.Info("Exiting from into ResourceRequestManagement Service helper GetResourceRequestFormDetails method ");
                return resourceDetails;
            }
            catch 
            {
                Logger.Info("Exception occured at ResourceRequestManagement Service helper GetResourceRequestFormDetails method ");
                throw;
            }
        }
    }
}
