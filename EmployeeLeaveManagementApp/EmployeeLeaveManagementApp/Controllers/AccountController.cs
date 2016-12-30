using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LMS_WebAPP_ServiceHelpers;
using LMS_WebAPP_Utils;
using LMS_WebAPP_Domain;
using System.Threading.Tasks;
using LMS_WebAPI_Domain;
using System.Configuration;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class AccountController : Controller
    {

        UserManagement user = new UserManagement();
        // GET: Account
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            Logger.Info("addded new");
            ViewBag.userExist = true;
            return View();
        }

        public async Task<ActionResult> Profile()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                EmployeeDetailsModel datares = await user.GetUserProfileDetails(data.RefEmployeeId);
                List<string> col = new List<string>() { "danger","info","warning","success" };
                datares.Colors = col;
                //Models.LoginModel model = new Models.LoginModel();
                //model.EmpName = data.UserName;
                //model.UserName = data.UserName;
                //model.Projectname = datares.ProjectName;
                //model.ManagerName = datares.ManagerName;
                //model.DateOfJoining = DateTime.Now;
                //model.RoleName = datares.RoleName;
                return View(datares);
            }
            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Login(Models.LoginModel model)
        {
            if (ModelState.IsValid)
            {

                if (null == Session[Constants.SESSION_OBJ_USER])
                {
                   // var encryptedPassword = CommonMethods.EncryptDataForLogins(model.UserName, model.Password);
                    var data = await user.GetUserAsync(model.UserName, model.Password);
                    if (null != data && data.RefEmployeeId !=0)
                    {
                        // var dataemp = await user.GetUserDetaiilsAsync(data.RefEmployeeId);
                        #region Cookie setup with remember me
                        if (model.RememberMe) //adding cookies for the user
                        {
                            var aCookie = new HttpCookie("dguser-" + model.UserName);
                            aCookie.Values.Add("USER_NAME", CommonMethods.EncryptString(model.UserName));
                            aCookie.Values.Add("PASS", CommonMethods.EncryptString(model.Password));
                            aCookie.Expires = DateTime.Now.AddDays(7);
                            Response.Cookies.Add(aCookie);
                        }
                        else //To delete cookies if remember is false
                        {
                            var myCookie = new HttpCookie("dguser-" + model.UserName);
                            myCookie.Expires = DateTime.Now.AddDays(-1d);
                            Response.Cookies.Add(myCookie);
                        }
                        #endregion
                        Session[Constants.SESSION_OBJ_USER] = data;
                        ViewBag.UserExist = true;
                        if (data.RefRoleId ==(int) EmployeeRole.Employee || data.RefRoleId==(int) EmployeeRole.Manager)
                        {
                            return RedirectToAction("Dashboard");
                        }
                        else if(data.RefRoleId==(int)EmployeeRole.HR)
                        {
                            return RedirectToAction("EmployeeDetails", "HR");
                        }
                    }
                    ViewBag.UserExist = false;
                    return View();
                }
                else
                {
                 return   RedirectToAction("Dashboard");
                }
                  
            }
            ViewBag.userExist = true;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            try
            {
                //WebSecurity.
                //Clear Session
                // Session.Abandon();
                Session.Abandon();
                Session.Clear();
                FormsAuthentication.SignOut();
                return View("Login");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

       
        public async Task<ActionResult> Dashboard()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                EmployeeDetailsModel datares = await user.GetUserDetailsAsync(data.RefEmployeeId);
                
                Models.LoginModel model = new Models.LoginModel();
                model.EmpName = data.UserName;
                model.UserName = data.UserName;
                model.Projectname = datares.ProjectName;
                model.ManagerName = datares.ManagerName;
                model.TotalLeaveCount = Convert.ToInt16(datares.TotalLeaveCount);
                model.TotalApplied = datares.TotalApplied;
                model.TotalSpent = datares.TotalSpent;
                model.TotalLeft = Convert.ToInt16(datares.TotalLeaveCount - datares.TotalSpent);
                model.DateOfJoining = DateTime.Now;
                model.RoleName = datares.RoleName;
                model.Announcements = new List<Models.Announcement>();
                foreach (var item in datares.Announcements)
                {
                    Models.Announcement announceItem = new Models.Announcement();
                    announceItem.ImagePath = item.ImagePath;
                    announceItem.CarouselContent = item.CarouselContent;
                    announceItem.Title = item.Title;
                    model.Announcements.Add(announceItem);
                }
                model.LeaveDetails = datares.LeaveDetails;
                // model.Announcements = (Models.Announcement)datares.Announcements;
                return View(model);
            }
            return View("Login");
        }

        public async Task<ActionResult> GetLeaveReportDetails(int year)
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                LeaveReportModel datares = await user.GetLeaveReportDetails(data.RefEmployeeId, year);
                // model.Announcements = (Models.Announcement)datares.Announcements;
                return Json(new { result = datares });
            }
            return View("Login");
        }


        //private async Task<EmployeeDetailsModel> GetAsyncData(UserAccount data)
        //{
        //    Task<EmployeeDetailsModel> sCode = Task.Run(async () =>
        //    {
        //        var EmpData = await user.GetUserDetailsAsync(data.RefEmployeeId);
        //        //EmployeeDetailsModel ss = EmpData;
        //        return EmpData;
        //    });
        //    return null;
        //}

        public async Task<ActionResult> ProfileDetails(int id)
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                EmployeeDetailsModel datares = await user.GetUserProfileDetails(id);
                List<string> col = new List<string>() { "danger", "info", "warning", "success","primary"};
                datares.Colors = col;
                //Models.LoginModel model = new Models.LoginModel();
                //model.EmpName = data.UserName;
                //model.UserName = data.UserName;
                //model.Projectname = datares.ProjectName;
                //model.ManagerName = datares.ManagerName;
                //model.DateOfJoining = DateTime.Now;
                //model.RoleName = datares.RoleName;
                return View("Profile",datares);
            }
            return View("Login");
        }

        public async Task<ActionResult> DownloadPDF(int userId)
        {

            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    EmployeeDetailsModel datares = await user.GetUserProfileDetails(data.RefEmployeeId);
                    List<string> col = new List<string>() { "danger", "info", "warning", "success" };
                    datares.Colors = col;
                    return new Rotativa.ViewAsPdf("ProfileDownload", datares)
                    {
                        FileName = datares.FirstName+"_Profile.pdf"
                    };

                }
                //Use ViewAsPdf Class to generate pdf using GeneratePDF.cshtml view
                else
                {
                    return View("Login");
                }
            }
            catch
            {

                throw;
            }
        }

        public async Task<ActionResult> ProfileDownload()
        {
            if (null != Session[Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                EmployeeDetailsModel datares = await user.GetUserProfileDetails(data.RefEmployeeId);
                List<string> col = new List<string>() { "danger", "info", "warning", "success" };
                datares.Colors = col;
                //return new Rotativa.ViewAsPdf("ProfileDownload", datares)
                //{
                //    FileName = "test.pdf"
                //};
                return View(datares);
            }
            else
            {
                return View("Login");
            }
        }
    }
}