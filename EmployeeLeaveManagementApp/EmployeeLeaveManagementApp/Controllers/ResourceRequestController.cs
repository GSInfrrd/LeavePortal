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
            int managerId = ((UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER]).RefEmployeeId;
            var resourceRequestFormDetails = await resourceManagementOperations.GetResourceRequestFormDetails(managerId);
            foreach (var history in resourceRequestFormDetails.ResourceRequestHistory)
            {
                history.StatusValue = CommonMethods.Description((ResourceRequestStatus)history.Status);
                //history.StatusValue = Enum;
            }
            return View(resourceRequestFormDetails);
        }


        public async Task<ActionResult> SendRequestForResources(ResourceDetailsModel model)
        {
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
                    return Json(new { result = true, model = resourceRequestSent });
                }
                else
                {
                    return Json(new { result = false });
                }
            }
            catch (Exception)
            {
                return Json(new { result = false });
            }
        }
    }
}
