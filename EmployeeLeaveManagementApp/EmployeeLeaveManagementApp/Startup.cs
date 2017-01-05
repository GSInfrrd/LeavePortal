using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeeLeaveManagementApp.Startup))]
namespace EmployeeLeaveManagementApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
