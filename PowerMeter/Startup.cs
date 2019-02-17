using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PowerMeter.Startup))]
namespace PowerMeter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
