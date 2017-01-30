using LMS_WebAPI_DAL;
using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Hosting;
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
        [Route("SubmitResourceRequest")]
        public ResourceDetails SubmitResourceRequest(ResourceRequestDetailModel model)
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
                requestEntity.Ticket = "INF-" + model.RequestFromId + randomId;
                requestEntity.CreatedDate = DateTime.Now;
                requestEntity.UpdatedDate = DateTime.Now;
                requestEntity.Status = Convert.ToInt16((Enum)ResourceRequestStatus.Requested);

                var result = resourceRequestmanagement.SubmitResourceRequest(requestEntity);


                //Send mail
                Thread MailThread = new Thread(() => SendMailForResourceRequest(result, model, randomId));
                MailThread.Start();

                return result;
            }
            catch
            {

                throw;
            }
        }

        private void SendMailForResourceRequest(ResourceDetails result, ResourceRequestDetailModel model, string randomId)
        {

            if (result != null)
            {
                ActionsForMail actionName = ActionsForMail.AddResourceRequest;
                MailManagement MM = new MailManagement();
                var MailDetails = MM.GetMailTemplateForAddResourceRequest(actionName, model.RequestFromId, model.RequestToId);
                string TemplatePath = MailDetails.TemplatePath;

                string body;
                //Read template file from the App_Data folder
                using (var sr = new StreamReader(HostingEnvironment.MapPath(TemplatePath)))
                {
                    body = sr.ReadToEnd();
                }
                var logoPath = HostingEnvironment.MapPath("~/Content/Images/infrrd-logo-main.png");
                string messageBody = string.Format(body, MailDetails.ManagerName, MailDetails.EmployeeName, model.ResourceRequestTitle, model.NumberRequestedResources, model.Skills, ResourceRequestStatus.Requested.Description());
                string subject = "Ticket " + "INF-" + model.RequestFromId + randomId + " raised " + (DateTime.Now).ToShortDateString();
                MailUtility.sendmail(MailDetails.ToMailId, MailDetails.CcMailId, subject, messageBody, logoPath);
            }
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("ResourceRequests/{id}/{viewAll}")]
        public ResourceDetails ResourceRequests(int id, bool viewAll)
        {
            try
            {
                var resourceRequests = resourceRequestmanagement.GetResourceRequests(id, viewAll);

                return resourceRequests;
            }
            catch
            {
                throw;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("SubmitResourceRequestsResponse")]
        public bool SubmitResourceRequestsResponse(ResourceRequestDetailModel model)
        {
            try
            {
                bool result = false;
                var resourceRequestResponse = new ResourceRequestDetail()
                {
                    Ticket = model.Ticket,
                    Status = model.Status,
                    CreatedDate = model.CreatedDate,
                    UpdatedDate = DateTime.Now,
                    RequestFromId = model.RequestFromId,
                    RequestToId = model.RequestToId
                };

                var resourceSubmit = resourceRequestmanagement.SubmitResourceRequestResponse(resourceRequestResponse);
                if (null != resourceSubmit)
                {
                    if (resourceSubmit.Result)
                        result = true;
                }

                //Send mail
                Thread MailThread = new Thread(() => SendMailForResourceRequestResponse(result, model, resourceSubmit));
                MailThread.Start();

                return result;
            }
            catch
            {
                throw;
            }
        }

        private void SendMailForResourceRequestResponse(bool result, ResourceRequestDetailModel model, ResourceRequestDetailModel resourceSubmit)
        {

            if (result)
            {
                ActionsForMail actionName = ActionsForMail.ResourceRequestUpdate;
                MailManagement MM = new MailManagement();
                var MailDetails = MM.GetMailTemplateForResourceRequestUpdate(actionName, model.RequestFromId, model.RequestToId);
                string TemplatePath = MailDetails.TemplatePath;
                ResourceRequestStatus status = (ResourceRequestStatus)(Convert.ToInt32(resourceSubmit.Status));
                string body;
                //Read template file from the App_Data folder
                using (var sr = new StreamReader(HostingEnvironment.MapPath(TemplatePath)))
                {
                    body = sr.ReadToEnd();
                }
                var logoPath = HostingEnvironment.MapPath("~/Content/Images/infrrd-logo-main.png");
                string messageBody = string.Format(body, MailDetails.EmployeeName, MailDetails.ManagerName, resourceSubmit.ResourceRequestTitle, resourceSubmit.NumberRequestedResources, resourceSubmit.Skills, status);
                string subject = "Ticket " + model.Ticket + " updated " + (DateTime.Now).ToShortDateString();
                MailUtility.sendmail(MailDetails.ToMailId, MailDetails.CcMailId, subject, messageBody, logoPath);

            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("DeleteResourceRequest")]
        public ResourceDetails DeleteResourceRequest(string id, int userId)
        {
            try
            {
                var resourceRequests = resourceRequestmanagement.DeleteResourceRequestManagement(id, userId);

                return resourceRequests;
            }
            catch
            {

                throw;
            }
        }

        [Route("GetProjectMembersList")]
        public List<TeamMembers> GetProjectMembersList(int projectId)
        {
            try
            {
                Logger.Info("Entering in ResourceRequestController API GetProjectMembersList method");
                var result = resourceRequestmanagement.GetProjectMembersList(projectId);
                Logger.Info("Successfully exiting from ResourceRequestController API GetProjectMembersList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController API GetProjectMembersList method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("RemoveProjectResource")]
        public List<TeamMembers> RemoveProjectResource(int employeeProjectId, int projectId)
        {
            try
            {
                Logger.Info("Entering in ResourceRequestController API RemoveProjectResource method");
                var removeResource = resourceRequestmanagement.RemoveProjectResource(employeeProjectId);
                var result = new List<TeamMembers>();
                if (removeResource)
                {
                    result = resourceRequestmanagement.GetProjectMembersList(projectId);
                }
                Logger.Info("Successfully exiting from ResourceRequestController API RemoveProjectResource method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController API RemoveProjectResource method.", ex);
                return null;
            }
        }

        [Route("GetResourceList")]
        public List<TeamMembers> GetResourceList(int refProject)
        {
            try
            {
                Logger.Info("Entering in ResourceRequestController API GetResourceList method");
                var result = resourceRequestmanagement.GetResourceList(refProject);
                Logger.Info("Successfully exiting from ResourceRequestController API GetResourceList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController API GetResourceList method.", ex);
                return null;
            }
        }

        [HttpGet]
        [Route("AddNewProjectResource")]
        public List<TeamMembers> AddNewProjectResource(int employeeId, int projectId)
        {
            try
            {
                Logger.Info("Entering in ResourceRequestController API RemoveProjectResource method");
                var removeResource = resourceRequestmanagement.AddNewProjectResource(employeeId, projectId);
                var result = new List<TeamMembers>();
                if (removeResource)
                {
                    result = resourceRequestmanagement.GetProjectMembersList(projectId);
                }
                Logger.Info("Successfully exiting from ResourceRequestController API RemoveProjectResource method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController API RemoveProjectResource method.", ex);
                return null;
            }
        }
    }
}
