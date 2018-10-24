using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NutricionWeb.Startup))]
namespace NutricionWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
