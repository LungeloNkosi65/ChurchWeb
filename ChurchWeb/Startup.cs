using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChurchWeb.Startup))]
namespace ChurchWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
