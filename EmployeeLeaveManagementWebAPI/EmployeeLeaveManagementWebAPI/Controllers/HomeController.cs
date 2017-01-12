using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            try
            {
                Logger.Info("Entering in HomeController API Index method");
                ViewBag.Title = "Home Page";
                Logger.Info("Successfully exiting from HomeController API Index method");
                return View();
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HomeController API Index method.", ex);
                return null;
            }
        }
    }
}
