using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Independent_Study.Startup))]
namespace Independent_Study
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
