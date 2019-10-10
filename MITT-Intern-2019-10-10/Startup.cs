using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MITT_Intern_2019_10_10.Startup))]
namespace MITT_Intern_2019_10_10
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
