using BusinesLogicLayer.Services;
using Owin;


namespace Library.Web.App_Start
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(CustomUserManager.Create);
        }
    }
}