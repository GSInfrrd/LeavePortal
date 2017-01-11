using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;

[assembly: OwinStartup(typeof(EmployeeLeaveManagementWebAPI.Startup))]

namespace EmployeeLeaveManagementWebAPI
{
    public partial class Startup
    {
       
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            app.Map("/signalr", map =>
            {
                //var resolver = new NinjectSignalRDependencyResolver(kernel);
               // GlobalHost.DependencyResolver = resolver;
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true
                };
                map.RunSignalR(hubConfiguration);
            });
        }
    }
}
