using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_DatingApp.Startup))]
namespace MVC_DatingApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
