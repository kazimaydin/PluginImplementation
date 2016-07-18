using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PluginImplementation.Web.Startup))]
namespace PluginImplementation.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
