using BusinesLogicLayer.Abstract;
using BusinesLogicLayer.Services.Identity;
using Entities.Enums;
using Entities.Tables;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

[assembly: OwinStartup(typeof(Library.Web.App_Start.Startup))]

namespace Library.Web.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("PublicationsContext");
        }
    }
}
