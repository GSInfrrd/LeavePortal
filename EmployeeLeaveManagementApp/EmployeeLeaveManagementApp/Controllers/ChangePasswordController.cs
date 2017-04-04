using LMS_WebAPP_Domain;
using LMS_WebAPP_ServiceHelpers;
using LMS_WebAPP_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class ChangePasswordController : Controller
    {
        // GET: ChangePassword
        UserManagement UM = new UserManagement();
        public ActionResult ChangePassword()
        {
            Logger.Info("Entering in ChangePasswordController APP CheckEmployeePassword method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    return View();
                }
                else
                {
                    Logger.Info("Successfully exiting from ChangePasswordController APP ChangePassword method");
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ChangePasswordController APP ChangePassword method.", ex);
                return View("Error"); ;
            }
        }

        public async Task<bool> CheckEmployeePassword(string currentPassword)
        {
            Logger.Info("Entering in ChangePasswordController APP CheckEmployeePassword method");
            try
            {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var result = await UM.CheckEmployeePasswordAsync(data.RefEmployeeId, currentPassword);
                    Logger.Info("Successfully exiting from ChangePasswordController APP CheckEmployeePassword method");
                    return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ChangePasswordController APP CheckEmployeePassword method.", ex);
                return false;
            }

        }

        
        public async Task<bool> EditEmployeePassword(string newPassword)
        {
            Logger.Info("Entering in ChangePasswordController APP UpdatePassword method");
            try
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                var result = await UM.UpdatePasswordAsync(data.RefEmployeeId, newPassword);
                Logger.Info("Successfully exiting from ChangePasswordController APP UpdatePassword method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ChangePasswordController APP UpdatePassword method.", ex);
                return false;
            }

        }


    }
}