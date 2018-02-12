using BusinesLogicLayer.Services;
using Library.ViewModels.ViewModels;
using System.Net;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    [Authorize]
    public class MagazineController : Controller
    {
        private MagazineService _magazineService;

        public MagazineController()
        {
            _magazineService = new MagazineService(Settings.GetConnectionString());
        }

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(_magazineService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(MagazineViewModel magazine)
        {
            _magazineService.Add(magazine);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(_magazineService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(MagazineViewModel magazine)
        {
            _magazineService.Edit(magazine);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _magazineService.Delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}