using BusinesLogicLayer.Services;
using System.Web.Mvc;
using System.Net;
using Library.ViewModels.ViewModels;
using System.Linq;

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
            return View();
        }
        public JsonResult List()
        {
            return Json(_brochureService.GetAll().Select(x => new { x.Id, x.Name, x.NumberOfPages,
                TypeOfCover = x.TypeOfCover.ToString()}), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(BrochureViewModel brochure)
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
        public ActionResult Edit(BrochureViewModel brochure)
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