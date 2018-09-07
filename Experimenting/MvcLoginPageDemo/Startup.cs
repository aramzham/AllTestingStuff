using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcLoginPageDemo.Startup))]
namespace MvcLoginPageDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
