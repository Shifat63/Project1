using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project1.Startup))]
namespace Project1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
