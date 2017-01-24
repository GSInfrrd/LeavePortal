using EmployeeLeaveManagementApp.Models;
using LMS_WebAPP_Domain;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Text;
using LMS_WebAPP_ServiceHelpers;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class ResourceRequestController : Controller
    {
        ResourceManagement resourceManagementOperations = new ResourceManagement();
        public ActionResult RequestForResources()
        {
            Logger.Info("Entering in ResourceRequestController APP RequestForResources method");
            try
            {
                int managerId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;

                var resourceRequestFormDetails = resourceManagementOperations.GetResourceRequestFormDetails(managerId);
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


        public ActionResult SendRequestForResources(ResourceDetailsModel model)
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

                var resourceRequestSent = resourceManagementOperations.SubmitResourceRequest(resourceEntity);
                if (resourceRequestSent.Result)
                {
                    foreach (var resourceRequest in resourceRequestSent.ResourceRequestHistory)
                    {
                        resourceRequest.StatusValue = CommonMethods.Description((ResourceRequestStatus)resourceRequest.Status);
                    }
                    Logger.Info("Successfully exiting from ResourceRequestController APP SendRequestForResources method");
                    return Json(new { result = true, model = resourceRequestSent.ResourceRequestHistory, count = resourceRequestSent.Count });
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

        public ActionResult RequestForResourcesHR()
        {
            try
            {
                bool viewAll = false;
                int hrId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;

                var resourceRequests = resourceManagementOperations.GetResourceRequests(hrId, viewAll);
                if (null != resourceRequests)
                {
                    foreach (var request in resourceRequests.ResourceRequestHistory)
                    {
                        request.StatusValue = CommonMethods.Description((ResourceRequestStatus)request.Status);
                    }
                    return View(resourceRequests);
                }
                else
                {
                    return null;
                }
            }
            catch 
            {
                Logger.Error("Error at ResourceRequestController APP SendRequestForResources method.");
                return Json(new { result = false });
            }
        }

        public ActionResult RespondToRequestForResources(ResourceRequestDetailModel model)
        {
            try
            {
                bool result = false;
                result = resourceManagementOperations.RespondToResourceRequests(model);
                model.StatusValue = CommonMethods.Description((ResourceRequestStatus)model.Status);

                if (result)
                {
                    return Json(new { result = true, ticket = model.Ticket, status = model.Status, statusValue = model.StatusValue });
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
    

        public ActionResult ViewAllRequests()
        {
            try
            {
                bool viewAll = true;
                var currentUserId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;

                var resourceRequests = resourceManagementOperations.GetResourceRequests(currentUserId, viewAll);
                foreach (var request in resourceRequests.ResourceRequestHistory)
                {
                    request.StatusValue = CommonMethods.Description((ResourceRequestStatus)request.Status);
                }
                return Json(new { model = resourceRequests.ResourceRequestHistory });
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController APP ViewAllRequests method.", ex);
                return null;
            }
        }

        [HttpPost]
        public ActionResult CancelRequest(string ticket)
        {
            try
            {
                var resourceRequests = new ResourceDetails();
                int userId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;

                resourceRequests = resourceManagementOperations.DeleteResourceRequest(ticket, userId);
                foreach (var request in resourceRequests.ResourceRequestHistory)
                {
                    request.StatusValue = CommonMethods.Description((ResourceRequestStatus)request.Status);
                }
                return Json(new { result = resourceRequests.Result, model = resourceRequests.ResourceRequestHistory, count = resourceRequests.Count });
            }
            catch
            {
                Logger.Error("Error at ResourceRequestController APP DeleteRequest method.");
                return Json(new { result = false });
            }
        }

        public ActionResult ResourceLoad()
            {
                Logger.Info("Entering in ResourceRequestController APP ResourceLoad method");
                try
                {
                    if (null != ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]))
                    {
                        Logger.Info("Successfully exiting from ResourceRequestController APP ResourceLoad method");
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Login", "Account");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error("Error at ResourceRequestController APP ResourceLoad method.", ex);
                    return View("Error");
                }
            }

        public async Task<JsonResult> GetProjectMembersList(int projectId)
        {
            Logger.Info("Entering in ResourceRequestController APP GetProjectMembersList method");
            try
            {
                var empList = new List<TeamMembers>();
                var details = await resourceManagementOperations.GetProjectMembersListAsync(projectId);
                Logger.Info("Successfully exiting from ResourceRequestController APP GetProjectMembersList method");
                return new JsonResult()
                {
                    ContentEncoding = Encoding.Default,
                    ContentType = "application/json",
                    Data = details,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };

            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController APP GetProjectMembersList method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> RemoveProjectResource(int employeeProjectId, int projectId)
        {
            Logger.Info("Entering in ResourceRequestController APP RemoveProjectResource method");
            try
            {
                var details = await resourceManagementOperations.RemoveProjectResourceAsync(employeeProjectId, projectId);
                Logger.Info("Successfully exiting from ResourceRequestController APP RemoveProjectResource method");
                return new JsonResult()
                {
                    ContentEncoding = Encoding.Default,
                    ContentType = "application/json",
                    Data = details,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };

            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController APP RemoveProjectResource method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> GetResourceList()
        {
            Logger.Info("Entering in ResourceRequestController APP GetResourceList method");
            try
            {
                var empList = new List<TeamMembers>();
                var details = await resourceManagementOperations.GetResourceListAsync();
                Logger.Info("Successfully exiting from ResourceRequestController APP GetResourceList method");
                return new JsonResult()
                {
                    ContentEncoding = Encoding.Default,
                    ContentType = "application/json",
                    Data = details,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };

            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController APP GetResourceList method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> AddNewProjectResource(int employeeId, int projectId)
        {
            Logger.Info("Entering in ResourceRequestController APP AddNewProjectResource method");
            try
            {
                var details = await resourceManagementOperations.AddNewProjectResourceAsync(employeeId, projectId);
                Logger.Info("Successfully exiting from ResourceRequestController APP AddNewProjectResource method");
                return new JsonResult()
                {
                    ContentEncoding = Encoding.Default,
                    ContentType = "application/json",
                    Data = details,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };

            }
            catch (Exception ex)
            {
                Logger.Error("Error at ResourceRequestController APP AddNewProjectResource method.", ex);
                return null;
            }
        }
    }
}
