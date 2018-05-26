using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TransportBrunaWeb.Startup))]
namespace TransportBrunaWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
