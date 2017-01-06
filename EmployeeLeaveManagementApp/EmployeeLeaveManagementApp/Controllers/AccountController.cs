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
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using EmployeeLeaveManagementApp.Models;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        UserManagement user = new UserManagement();
        // GET: Account
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if(TempData["LoginError"]!=null)
            {
                TempData["GLoginError"]= TempData["LoginError"];
            }
            ViewBag.ReturnUrl = returnUrl;
            Logger.Info("addded new");
            ViewBag.userExist = true;
            return View();
        }

        public async Task<ActionResult> Profile()
        {
            if (null != Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER];
                EmployeeDetailsModel datares = await user.GetUserProfileDetails(data.RefEmployeeId);
                List<string> col = new List<string>() { "danger", "info", "warning", "success" };
                datares.Colors = col;
                Models.LoginModel model = new Models.LoginModel();
                model.EmpName = data.UserName;
                model.UserName = data.UserName;
                //model.Projectname = datares.ProjectName;
                //model.ManagerName = datares.ManagerName;
                model.DateOfJoining = DateTime.Now;
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

                if (null == Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER])
                {
                    // var encryptedPassword = CommonMethods.EncryptDataForLogins(model.UserName, model.Password);
                    var data = await user.GetUserAsync(model.UserName, model.Password);
                    if (null != data && data.RefEmployeeId != 0)
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
                        Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER] = data;
                        ViewBag.UserExist = true;
                            return RedirectToAction("Dashboard");
                    
                    }
                    ViewBag.UserExist = false;
                    return View();
                }
                else
                {
                    return RedirectToAction("Dashboard");
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
                //FormsAuthentication.SignOut();
                return View("Login");
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<ActionResult> Dashboard()
        {
            if (null != Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER];
                EmployeeDetailsModel datares = await user.GetUserDetailsAsync(data.RefEmployeeId);
                data.ManagerId = datares.ManagerId;
                data.ManagerEmail = datares.MangerEmail;
                Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER] = data;
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
            if (null != Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER];
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
            if (null != Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER];
                EmployeeDetailsModel datares = await user.GetUserProfileDetails(id);
                List<string> col = new List<string>() { "danger", "info", "warning", "success", "primary" };
                datares.Colors = col;
                //Models.LoginModel model = new Models.LoginModel();
                //model.EmpName = data.UserName;
                //model.UserName = data.UserName;
                //model.Projectname = datares.ProjectName;
                //model.ManagerName = datares.ManagerName;
                //model.DateOfJoining = DateTime.Now;
                //model.RoleName = datares.RoleName;
                return View("Profile", datares);
            }
            return View("Login");
        }

        public async Task<ActionResult> DownloadPDF(int userId)
        {

            try
            {
                if (null != Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER];
                    EmployeeDetailsModel datares = await user.GetUserProfileDetails(data.RefEmployeeId);
                    List<string> col = new List<string>() { "danger", "info", "warning", "success" };
                    datares.Colors = col;
                    return new Rotativa.ViewAsPdf("ProfileDownload", datares)
                    {
                        FileName = datares.FirstName + "_Profile.pdf"
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
            if (null != Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER])
            {
                var data = (UserAccount)Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER];
                EmployeeDetailsModel datares = await user.GetUserProfileDetails(data.RefEmployeeId);
                List<string> col = new List<string>() { "danger", "info", "warning", "success" };
                datares.Colors = col;
                return View(datares);
            }
            else
            {
                return View("Login");
            }
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var data = await user.GetUserAsync(loginInfo.Email, string.Empty);
                    Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER] = data;
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return RedirectToAction("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

    
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                var emailDomain = model.Email.Split('@')[1];
                if (emailDomain == "infrrd.ai")
                {
                    var data = await user.GetUserAsync(model.Email, string.Empty);
                    if (data.Id != 0)
                    {
                        Session[LMS_WebAPP_Utils.Constants.SESSION_OBJ_USER] = data;
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        TempData["LoginError"] = "Account has not been Registered.Please Contact HR!";
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    TempData["LoginError"] = "Kindly login with Infrrd Email(eg:xxxx@infrrd.ai)";
                    return RedirectToAction("Login");
                }
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Dashboard", "Account");
        }

        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                properties.Dictionary.Add("hd", "infrrd.ai");
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
    }
}