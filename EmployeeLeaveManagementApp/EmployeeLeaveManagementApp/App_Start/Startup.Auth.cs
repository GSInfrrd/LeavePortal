using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System.Collections.Generic;
using System.Web;
using EmployeeLeaveManagementApp.Models;

namespace EmployeeLeaveManagementApp
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");
            app.UseGoogleAuthentication(new Microsoft.Owin.Security.Google.GoogleOAuth2AuthenticationOptions()
            {
                // other config ...
                Provider = new CustomGoogleAuthProvider(),
                ClientId = "93262678668-g3lvtcanv38065mcqav4o2bgsdfc3irg.apps.googleusercontent.com",
                ClientSecret = "gcjoxpB66YwX5tuz5rpm_y62"
            });
            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "298923408937-gpoft9vnc4a5rmb1d4req58rumstekc6.apps.googleusercontent.com",
            //    ClientSecret = "Qg6rKwhqjgcY2ykteKKvZKDG"
            //});
        }
    }

    class CustomGoogleAuthProvider : GoogleOAuth2AuthenticationProvider
    {
        public CustomGoogleAuthProvider()
        {
            OnApplyRedirect = (GoogleOAuth2ApplyRedirectContext context) =>
            {
                IDictionary<string, string> props = context.OwinContext.Authentication.AuthenticationResponseChallenge.Properties.Dictionary;

                string newRedirectUri = context.RedirectUri;

                string[] paramertsToPassThrough = new[] { "hd", "email" };

                foreach (var param in paramertsToPassThrough)
                {
                    if (props.ContainsKey(param))
                    {
                        newRedirectUri += string.Format("&{0}={1}", param, HttpUtility.UrlEncode(props[param]));
                    }
                }

                context.Response.Redirect(newRedirectUri);
            };
        }
    }
}