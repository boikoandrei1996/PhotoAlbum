using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplicationPL.Startup))]
namespace WebApplicationPL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
