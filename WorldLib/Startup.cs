using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorldLib.Startup))]
namespace WorldLib
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
