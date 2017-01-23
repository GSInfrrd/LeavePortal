using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using EmployeeLeaveManagementWebAPI.Models;
using EmployeeLeaveManagementWebAPI.Providers;
using EmployeeLeaveManagementWebAPI.Results;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private ApplicationUserManager _userManager;
        private UserManagement userManager = new UserManagement();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        // GET api/Account/UserInfo
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            try
            {
                Logger.Info("Entering in AccountController API GetUserInfo method");
                ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
                Logger.Info("Successfully exiting from AccountController API GetUserInfo method");
                return new UserInfoViewModel
                {
                    Email = User.Identity.GetUserName(),
                    HasRegistered = externalLogin == null,
                    LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
                };
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API GetUserInfo method.", ex);
                return null;
            }
        }

        // POST api/Account/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            try
            {
                Logger.Info("Entering in AccountController API Logout method");
                Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
                Logger.Info("Successfully exiting from AccountController API Logout method");
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API Logout method.", ex);
                return BadRequest();
            }
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            try
            {
                Logger.Info("Entering in AccountController API GetManageInfo method");
                IdentityUser user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                if (user == null)
                {
                    return null;
                }

                List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

                foreach (IdentityUserLogin linkedAccount in user.Logins)
                {
                    logins.Add(new UserLoginInfoViewModel
                    {
                        LoginProvider = linkedAccount.LoginProvider,
                        ProviderKey = linkedAccount.ProviderKey
                    });
                }

                if (user.PasswordHash != null)
                {
                    logins.Add(new UserLoginInfoViewModel
                    {
                        LoginProvider = LocalLoginProvider,
                        ProviderKey = user.UserName,
                    });
                }
                Logger.Info("Successfully exiting from AccountController API GetManageInfo method");
                return new ManageInfoViewModel
                {
                    LocalLoginProvider = LocalLoginProvider,
                    Email = user.UserName,
                    Logins = logins,
                    ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
                };
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API GetManageInfo method.", ex);
                return null;
            }
        }

        // POST api/Account/ChangePassword
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            try
            {
                Logger.Info("Entering in AccountController API ChangePassword method");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword,
                    model.NewPassword);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                Logger.Info("Successfully exiting from AccountController API ChangePassword method");
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API ChangePassword method.", ex);
                return BadRequest();
            }
        }

        // POST api/Account/SetPassword
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            try
            {
                Logger.Info("Entering in AccountController API SetPassword method");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                Logger.Info("Successfully exiting from AccountController API SetPassword method");
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API SetPassword method.", ex);
                return BadRequest();
            }
        }

        // POST api/Account/AddExternalLogin
        [Route("AddExternalLogin")]
        public async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            try
            {
                Logger.Info("Entering in AccountController API AddExternalLogin method");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                AuthenticationTicket ticket = AccessTokenFormat.Unprotect(model.ExternalAccessToken);

                if (ticket == null || ticket.Identity == null || (ticket.Properties != null
                    && ticket.Properties.ExpiresUtc.HasValue
                    && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
                {
                    return BadRequest("External login failure.");
                }

                ExternalLoginData externalData = ExternalLoginData.FromIdentity(ticket.Identity);

                if (externalData == null)
                {
                    return BadRequest("The external login is already associated with an account.");
                }

                IdentityResult result = await UserManager.AddLoginAsync(User.Identity.GetUserId(),
                    new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                Logger.Info("Successfully exiting from AccountController API AddExternalLogin method");
                return Ok();
            }
            catch
            {
                throw;
            }
        }

        // POST api/Account/RemoveLogin
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            try
            {
                Logger.Info("Entering in AccountController API RemoveLogin method");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                IdentityResult result;

                if (model.LoginProvider == LocalLoginProvider)
                {
                    result = await UserManager.RemovePasswordAsync(User.Identity.GetUserId());
                }
                else
                {
                    result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(),
                        new UserLoginInfo(model.LoginProvider, model.ProviderKey));
                }

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                Logger.Info("Successfully exiting from AccountController API RemoveLogin method");
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API RemoveLogin method.", ex);
                return BadRequest();
            }
        }

        // GET api/Account/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            try
            {
                Logger.Info("Entering in AccountController API GetExternalLogin method");
                if (error != null)
                {
                    return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
                }

                if (!User.Identity.IsAuthenticated)
                {
                    return new ChallengeResult(provider, this);
                }

                ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

                if (externalLogin == null)
                {
                    return InternalServerError();
                }

                if (externalLogin.LoginProvider != provider)
                {
                    Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    return new ChallengeResult(provider, this);
                }

                ApplicationUser user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                    externalLogin.ProviderKey));

                bool hasRegistered = user != null;

                if (hasRegistered)
                {
                    Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                       OAuthDefaults.AuthenticationType);
                    ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                        CookieAuthenticationDefaults.AuthenticationType);

                    AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                    Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
                }
                else
                {
                    IEnumerable<Claim> claims = externalLogin.GetClaims();
                    ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                    Authentication.SignIn(identity);
                }
                Logger.Info("Successfully exiting from AccountController API GetExternalLogin method");
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API GetExternalLogin method.", ex);
                return BadRequest();
            }
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            try
            {
                Logger.Info("Entering in AccountController API GetExternalLogins method");
                IEnumerable<AuthenticationDescription> descriptions = Authentication.GetExternalAuthenticationTypes();
                List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

                string state;

                if (generateState)
                {
                    const int strengthInBits = 256;
                    state = RandomOAuthStateGenerator.Generate(strengthInBits);
                }
                else
                {
                    state = null;
                }

                foreach (AuthenticationDescription description in descriptions)
                {
                    ExternalLoginViewModel login = new ExternalLoginViewModel
                    {
                        Name = description.Caption,
                        Url = Url.Route("ExternalLogin", new
                        {
                            provider = description.AuthenticationType,
                            response_type = "token",
                            client_id = Startup.PublicClientId,
                            redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                            state = state
                        }),
                        State = state
                    };
                    logins.Add(login);
                }
                Logger.Info("Successfully exiting from AccountController API GetExternalLogins method");
                return logins;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API GetExternalLogins method.", ex);
                return null;
            }
        }

        //GET api/Account/Login?username=abcd&password=234234#434ndfh@323
        [AllowAnonymous]
        [HttpGet]
        public UserAccountModel Login(string userName, string password)
        {
            try
            {
                Logger.Info("Entering in AccountController API Login method");
                var userData = userManager.GetUser(userName, password);
                Logger.Info("Successfully exiting from AccountController API Login method");
                return userData;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API Login method.", ex);
                return null;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("GetUserDetails")]
        public EmployeeDetailsModel GetUserDetails(int empId)
        {
            try
            {
                Logger.Info("Entering in AccountController API GetUserDetails method");
                var empData = userManager.GetEmployeeDatailsForDashboard(empId, DateTime.Now.Year);
                Logger.Info("Successfully exiting from AccountController API GetUserDetails method");
                return empData;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API GetUserDetails method.", ex);
                return null;
            }
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("GetTeamMembers")]
        public IList<EmployeeDetailsModel> GetTeamMembers(int empId)
        {
            try
            {
                IList<EmployeeDetailsModel> resTeam = new List<EmployeeDetailsModel>();
                resTeam = userManager.GetTeamMembersForDashboard(empId);
                return resTeam;
            }
            catch (Exception ex)
            {
                return null;
                //throw;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public LeaveReportModel GetLeaveReportDetails(int empId, int year,int leaveType=0)
        {
            try
            {
                Logger.Info("Entering in AccountController API GetLeaveReportDetails method");
                var empData = userManager.GetEmployeeDatailsForDashboard(empId, year,leaveType);
                Logger.Info("Successfully exiting from AccountController API GetLeaveReportDetails method");
                return empData.leaveDetails;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API GetLeaveReportDetails method.", ex);
                return null;
            }
        }



        // POST api/Account/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            try
            {
                Logger.Info("Entering in AccountController API Register method");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

                IdentityResult result = await UserManager.CreateAsync(user, model.Password);

                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                Logger.Info("Successfully exiting from AccountController API Register method");
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API Register method.", ex);
                return BadRequest();
            }
        }

        // POST api/Account/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            try
            {
                Logger.Info("Entering in AccountController API RegisterExternal method");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var info = await Authentication.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return InternalServerError();
                }

                var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };

                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }

                result = await UserManager.AddLoginAsync(user.Id, info.Login);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                Logger.Info("Successfully exiting from AccountController API RegisterExternal method");
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Error("Error at AccountController API RegisterExternal method.", ex);
                return BadRequest();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
            }

            base.Dispose(disposing);
        }

        #region Helpers

        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        #endregion
    }
}
