using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UserToDoApp.Startup))]
namespace UserToDoApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
