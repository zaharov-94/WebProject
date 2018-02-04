using BusinesLogicLayer.Services;
using Library.Web.Entities;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace Library.Web.Controllers
{
    public class AuthorizationController : Controller
    {
        private Authorization _authorization;

        public AuthorizationController()
        {
            _authorization = new Authorization();
        }

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
        private static CustomUserManager _customUserManager;

        public CustomUserManager UserManager
        {
            get
            {
                return _customUserManager ??
                       (_customUserManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<CustomUserManager>());
            }
        }
        public async Task<bool> Authenticate(string name, string password)
        {
            if (await UserManager.FindAsync(name, password) != null)
            {
                return true;
            }

            return false;
        }
    }
}