using Domain;
using LMS_WebAPI_DAL;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ResourceRequestManagemenet
    {
        private IResourceRequestRepository _resourceRequest = new ResourceRequestRepository();
        public ResourceDetails GetResourceRequestFormDetails(int managerId)
        {
            try
            {
                var resourceDetails = new ResourceDetails();
                resourceDetails = _resourceRequest.GetResourceRequestFormDetails(managerId);
                return resourceDetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResourceRequestDetailModel SubmitResourceRequest(ResourceRequestDetail model)
        {
            try
            {
                var requestModel = _resourceRequest.SubmitResourceRequest(model);

                return requestModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
