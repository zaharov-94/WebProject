using Library.Web.Entities;
using BusinesLogicLayer.Services;
using System.Web.Mvc;
using System.Net;

namespace Library.Web.Controllers
{
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
            return Json(_brochureService.GetAll(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(Brochure brochure)
        {
            _brochureService.Add(brochure);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            return View(_brochureService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(Brochure brochure)
        {
            try
            {
                _brochureService.Edit(brochure);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            _brochureService.Delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

    }
}