using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cootrasana.Backend.Startup))]
namespace Cootrasana.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
