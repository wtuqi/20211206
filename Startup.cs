using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Integral_exchange.Startup))]
namespace Integral_exchange
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
