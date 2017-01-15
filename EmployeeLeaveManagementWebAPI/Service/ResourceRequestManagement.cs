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

        public List<ResourceRequestDetailModel> GetResourceRequests(int hrId)
        {
            try
            {
                bool isManager = false;
                var requestModel = _resourceRequest.GetResourceRequestDetails(hrId, isManager);

                return requestModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResourceRequestDetailModel SubmitResourceRequestResponse(ResourceRequestDetail model)
        {
            try
            {
                var responseModel = _resourceRequest.SubmitResourceRequestResponse(model);

                return responseModel;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
