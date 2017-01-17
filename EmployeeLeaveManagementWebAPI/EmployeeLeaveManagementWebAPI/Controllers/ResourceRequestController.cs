using Domain;
using LMS_WebAPI_DAL;
using LMS_WebAPI_Domain;
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
    [RoutePrefix("api/ResourceRequest")]
    public class ResourceRequestController : ApiController
    {
        ResourceRequestManagemenet resourceRequestmanagement = new ResourceRequestManagemenet();


        [AllowAnonymous]
        [HttpGet]
        [Route("GetResourceDetails")]
        public ResourceDetails GetResourceDetails(int id)
        {
            try
            {
                var resourceDetails = new ResourceDetails();
                resourceDetails = resourceRequestmanagement.GetResourceRequestFormDetails(id);

                return resourceDetails;
            }
            catch
            {
                throw;
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
                requestEntity.Ticket = "inf-" + model.RequestFromId + randomId;
                requestEntity.CreatedDate = DateTime.Now;
                requestEntity.UpdatedDate = DateTime.Now;
                requestEntity.Status = Convert.ToInt16((Enum)ResourceRequestStatus.Requested);

                var result = resourceRequestmanagement.SubmitResourceRequest(requestEntity);
                return result;
            }
            catch 
            {

                throw;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ResourceRequests/{id}/{viewAll}")]
        public List<ResourceRequestDetailModel> ResourceRequests(int id, bool viewAll)
        {
            try
            {
                var lstResourceDetails = new List<ResourceRequestDetailModel>();
                lstResourceDetails = resourceRequestmanagement.GetResourceRequests(id, viewAll);

                return lstResourceDetails;
            }
            catch 
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SubmitResourceRequestsResponse")]
        public ResourceRequestDetailModel SubmitResourceRequestsResponse(ResourceRequestDetailModel model)
        {
            try
            {
                var resourceRequestResponse = new ResourceRequestDetail() {
                    Ticket = model.Ticket,
                    Status = model.Status,
                    CreatedDate = model.CreatedDate,
                    UpdatedDate = DateTime.Now
                };

                var result = resourceRequestmanagement.SubmitResourceRequestResponse(resourceRequestResponse);
                return result;
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetProjectMembersList")]
        public List<EmployeeDetailsModel> GetProjectMembersList(int projectId)
        {
            try
            {
                Logger.Info("Entering in HRController API GetProjectMembersList method");
                var result = resourceRequestmanagement.GetProjectMembersList(projectId);
                Logger.Info("Successfully exiting from HRController API GetProjectMembersList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HRController API GetProjectMembersList method.", ex);
                return null;
            }
        }
    }
}
