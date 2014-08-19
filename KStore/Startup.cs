using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KStore.Startup))]
namespace KStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
