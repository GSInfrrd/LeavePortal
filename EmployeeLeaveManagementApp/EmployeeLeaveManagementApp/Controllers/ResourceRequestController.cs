using EmployeeLeaveManagementApp.Models;
using LMS_WebAPP_Domain;
using LMS_WebAPP_Utils;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class ResourceRequestController : Controller
    {
        ResourceManagement resourceManagementOperations = new ResourceManagement();
        public async Task<ActionResult> RequestForResources()
        {
            Logger.Info("Entering in ResourceRequestController APP RequestForResources method");
            try
            {
                int managerId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;

            var resourceRequestFormDetails = await resourceManagementOperations.GetResourceRequestFormDetails(managerId);
            foreach (var history in resourceRequestFormDetails.ResourceRequestHistory)
            {
                history.StatusValue = CommonMethods.Description((ResourceRequestStatus)history.Status);
            }
                Logger.Info("Successfully exiting from ResourceRequestController APP RequestForResources method");
                return View(resourceRequestFormDetails);
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController APP RequestForResources method.", ex);
                return View("Error");
            }
        }


        public async Task<ActionResult> SendRequestForResources(ResourceDetailsModel model)
        {
            Logger.Info("Entering in ResourceRequestController APP SendRequestForResources method");
            try
            {
                int managerId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;
                var skillsString = "";
                for (int i = 0; i < model.Skills.Count; i++)
                {
                    if (i != (model.Skills.Count - 1))
                    {
                        skillsString += model.Skills[i] + ',';
                    }
                    else
                    {
                        skillsString += model.Skills[i];
                    }

                }
                var resourceEntity = new ResourceRequestDetailModel()
                {
                    RequestFromId = managerId,
                    RequestToId = model.RequestToId,
                    ResourceRequestTitle = model.ResourceRequestTitle,
                    NumberRequestedResources = model.NumberRequestedResources,

                    Skills = skillsString
                };

                var resourceRequestSent = await resourceManagementOperations.SubmitResourceRequest(resourceEntity);
                resourceRequestSent.StatusValue = CommonMethods.Description((ResourceRequestStatus)resourceRequestSent.Status);
                if (null != resourceRequestSent)
                {
                    Logger.Info("Successfully exiting from ResourceRequestController APP SendRequestForResources method");
                    return Json(new { result = true, model = resourceRequestSent });
                }
                else
                {
                    Logger.Info("Successfully exiting from ResourceRequestController APP SendRequestForResources method");
                    return Json(new { result = false });
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController APP SendRequestForResources method.", ex);
                return Json(new { result = false });
            }
        }

        public async Task<ActionResult> RequestForResourcesHR()
        {
            try
            {
                bool viewAll = false;
                int hrId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;
                var lstRequestsToRespond = new List<ResourceRequestDetailModel>();

                lstRequestsToRespond = await resourceManagementOperations.GetResourceRequests(hrId, viewAll);
                if (null != lstRequestsToRespond)
                {
                    foreach (var request in lstRequestsToRespond)
                    {
                        request.StatusValue = CommonMethods.Description((ResourceRequestStatus)request.Status);
                    }
                    return View(lstRequestsToRespond);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController APP SendRequestForResources method.", ex);
                return Json(new { result = false });
            }
        }

        public async Task<ActionResult> RespondToRequestForResources(ResourceRequestDetailModel model)
        {
            try
            {
                bool result = false;
                bool viewAll = false;
                var responsemodel = new ResourceRequestDetailModel();
                responsemodel = await resourceManagementOperations.RespondToResourceRequests(model);
                int hrId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;
                var lstRequestsToRespond = new List<ResourceRequestDetailModel>();
                lstRequestsToRespond = await resourceManagementOperations.GetResourceRequests(hrId, viewAll);
                foreach (var request in lstRequestsToRespond)
                {
                    request.StatusValue = CommonMethods.Description((ResourceRequestStatus)request.Status);
                }
                if (null != responsemodel)
                {
                    return Json(new { result = true, model = lstRequestsToRespond });
                }
                else
                {
                    return Json(new { result });
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController APP SendRequestForResources method.", ex);
                return Json(new { result = false });
            }
        }

        public async Task<ActionResult> ViewAllRequests()
        {
            try
            {
                bool viewAll = true;
                var currentUserId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;
                var test = (UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER];
                var lstAllRequests = new List<ResourceRequestDetailModel>();
                lstAllRequests = await resourceManagementOperations.GetResourceRequests(currentUserId, viewAll);
                foreach (var request in lstAllRequests)
                {
                    request.StatusValue = CommonMethods.Description((ResourceRequestStatus)request.Status);
                }
                return Json(new { model = lstAllRequests });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController APP ViewAllRequests method.", ex);
                return null;
            }
        }
    }
}
