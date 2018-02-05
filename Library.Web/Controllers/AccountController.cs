using BusinesLogicLayer.Services;
using Library.Web.Entities;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Entities.View_models;
using System.Web.Security;
using Entities.Entities;
using DataAccessLayer.Models;

namespace Library.Web.Controllers
{
    public class AccountController : Controller
    {

        public AccountController()
        {

        }

        // GET: Authorization
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                bool auth = Authenticate(model.Name, model.Password).Result;
                if (auth)
                {
                    FormsAuthentication.SetAuthCookie(model.Name, true);
                    return RedirectToAction("Index", "Book");
                }
                if (!auth)
                {
                    ModelState.AddModelError("", "User not exists");
                }
            }

            return View(model);
        }

        public ActionResult Registration()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Registration(RegisterModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        User user = null;
        //        using (UserContext db = new UserContext())
        //        {
        //            user = db.Users.FirstOrDefault(u => u.Email == model.Name);
        //        }
        //        if (user == null)
        //        {
        //            // создаем нового пользователя
        //            using (UserContext db = new UserContext())
        //            {
        //                db.Users.Add(new User { Email = model.Name, Password = model.Password, Age = model.Age });
        //                db.SaveChanges();

        //                user = db.Users.Where(u => u.Email == model.Name && u.Password == model.Password).FirstOrDefault();
        //            }
        //            // если пользователь удачно добавлен в бд
        //            if (user != null)
        //            {
        //                FormsAuthentication.SetAuthCookie(model.Name, true);
        //                return RedirectToAction("Index", "Home");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "Пользователь с таким логином уже существует");
        //        }
        //    }

        //    return View(model);
        //}

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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