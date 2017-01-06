using EmployeeLeaveManagementApp.Models;
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
            var model = new ResourceDetailsModel();
            var resourceRequestFormDetails = await resourceManagementOperations.GetResourceRequestFormDetails();

            return View(model);
        }

        //Send the request to get resources
        //public bool SendRequestForResources(Models.ResourceModel model)
        //{
        //    bool requestSent = false;
        //    var resourceEntity = new ResourceDetails();
        //    //foreach (var item in model)
        //    //{
        //    //    resourceEntity.RequestTitle = model.RequestTitle;
        //    //    resourceEntity.NumberOfResourceRequested = model.NumberOfResourceRequested;
        //    //    resourceEntity.Skills = model.Skills;
        //    //    resourceEntity.RequestSendTo = model.RequestSendTo;

        //    //}

        //    return requestSent;
        //}
    }
}
