using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PrintNow.Startup))]
namespace PrintNow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
