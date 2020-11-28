using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WalterChavarry.Startup))]
namespace WalterChavarry
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
