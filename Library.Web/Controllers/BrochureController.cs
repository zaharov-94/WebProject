using Library.Web.Entities;
using BusinesLogicLayer.Services;
using System.Web.Mvc;
using System.Net;

namespace Library.Web.Controllers
{
    [Authorize]
    public class BrochureController : Controller
    {
        private BrochureService _brochureService;

        public BrochureController()
        {
            _brochureService = new BrochureService(Settings.GetConnectionString());
        }
        public ActionResult Index()
        {
            return View(_brochureService.GetAll());
        }
        public JsonResult List()
        {

            return Json(_brochureService.GetAllInViewModel(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Brochure brochure)
        {
            _brochureService.Add(brochure);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(_brochureService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Brochure brochure)
        {
            _brochureService.Edit(brochure);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _brochureService.Delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}