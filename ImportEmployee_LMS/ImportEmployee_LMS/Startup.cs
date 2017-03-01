using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImportEmployee_LMS.Startup))]
namespace ImportEmployee_LMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
