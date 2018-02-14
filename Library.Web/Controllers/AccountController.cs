using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Security.Claims;
using BusinesLogicLayer.Abstract;
using Entities.Tables;
using BusinesLogicLayer.Infrastructure;
using Library.ViewModels.ViewModels;
using Shared.Enums;

namespace Library.Web.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserViewModel userTable = new UserViewModel { Email = model.Email, Password = model.Password};
                ClaimsIdentity claim = await UserService.Authenticate(userTable);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Enter correct data.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Book");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Book");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                UserViewModel userTable = new UserViewModel
                {
                    Email = model.Email,
                    Password = model.Password,
                    Role = Role.User
                };
                OperationDetails operationDetails = await UserService.Create(userTable);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Login","Account");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
    }
}