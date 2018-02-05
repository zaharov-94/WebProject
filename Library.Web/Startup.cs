using System;
using System.Threading.Tasks;
using BusinesLogicLayer.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Library.Web.Startup))]

namespace Library.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            ConfigureAuth(app);

        }
    }
}
