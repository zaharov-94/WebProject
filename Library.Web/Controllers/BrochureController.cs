using Library.Web.Entities;
using BusinesLogicLayer.Services;
using System.Web.Mvc;
using System.Net;
using System.Collections.Generic;
using Library.ViewModels.ViewModels;

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
            List<BrochureViewModel> list = new List<BrochureViewModel>();
            foreach(Brochure brochure in _brochureService.GetAll())
            {
                list.Add(new BrochureViewModel { Id = brochure.Id, Name = brochure.Name, TypeOfCover = brochure.TypeOfCover.ToString(), NumberOfPages = brochure.NumberOfPages });
            }
            return Json(list, JsonRequestBehavior.AllowGet);
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