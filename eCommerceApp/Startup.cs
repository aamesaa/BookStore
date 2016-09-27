using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eCommerceApp.Startup))]
namespace eCommerceApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
