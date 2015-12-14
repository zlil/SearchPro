using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SearchPro.Startup))]
namespace SearchPro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
