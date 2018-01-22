using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class AuthorizationController : Controller
    {
        Authorization _authorization = new Authorization();
        // GET: Authorization
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if ((_authorization.IsAuth(user)) && (_authorization.GetRole(user) == Enums.Roles.User))
            {
                return Redirect("/Home/Index");
            }
            if ((_authorization.IsAuth(user)) && (_authorization.GetRole(user) == Enums.Roles.Administrator))
            {
                return Redirect("/Admin/Index");
            }
            return View();
        }
    }
}