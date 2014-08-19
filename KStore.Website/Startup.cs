using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KStore.Website.Startup))]
namespace KStore.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
