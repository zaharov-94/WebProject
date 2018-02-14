using BusinesLogicLayer.Services;
using Library.ViewModels.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    [Authorize]
    public class PublicationHouseController : Controller
    {
        private PublicationHouseService _publicationHouseService;

        public PublicationHouseController()
        {
            _publicationHouseService = new PublicationHouseService(Settings.GetConnectionString());
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(_publicationHouseService.GetAll(), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(PublicationHouseViewModel publicationHouse)
        {
            if (ModelState.IsValid)
            {
                _publicationHouseService.Add(publicationHouse);
                return RedirectToAction("Index");
            }
            return View(publicationHouse);  
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(_publicationHouseService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(PublicationHouseViewModel publicationHouse)
        {
            if (ModelState.IsValid)
            {
                _publicationHouseService.Edit(publicationHouse);
                return RedirectToAction("Index");
            }
            return View(publicationHouse);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _publicationHouseService.Delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}