using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelpOut.Startup))]
namespace HelpOut
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //Hangfire.ConfigureHangfire(app);
            //Hangfire.InitializeJobs();

        }
    }
}
