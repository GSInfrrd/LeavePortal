using Domain;
using LMS_WebAPI_DAL;
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
        ResourceRequestManagemenet resourceRequestmanagement = new ResourceRequestManagemenet();
        public ResourceDetails Get(int id)
        {
            try
            {
                var resourceDetails = new ResourceDetails();
                resourceDetails = resourceRequestmanagement.GetResourceRequestFormDetails(id);

                return resourceDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ResourceRequestDetailModel SubmitResourceRequest(ResourceRequestDetailModel model)
        {
            try
            {
                long ticks = DateTime.Now.Ticks;
                byte[] bytes = BitConverter.GetBytes(ticks);
                string randomId = Convert.ToBase64String(bytes)
                                        .Replace('+', '0')
                                        .Replace('/', '0')
                                        .TrimEnd('=');
                var requestEntity = new ResourceRequestDetail();
                requestEntity.RequestFromId = model.RequestFromId;
                requestEntity.RequestToId = model.RequestToId;
                requestEntity.ResourceRequestTitle = model.ResourceRequestTitle;
                requestEntity.NumberRequestedResources = model.NumberRequestedResources;
                requestEntity.Skills = model.Skills;
                requestEntity.Ticket = model.RequestFromId + randomId;
                requestEntity.CreatedDate = DateTime.Now;
                requestEntity.UpdatedDate = DateTime.Now;
                requestEntity.Status = Convert.ToInt16((Enum)ResourceRequestStatus.InProgress);

                var result = resourceRequestmanagement.SubmitResourceRequest(requestEntity);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
