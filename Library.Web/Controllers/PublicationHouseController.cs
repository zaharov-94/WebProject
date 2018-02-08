using BusinesLogicLayer.Services;
using Library.Web.Entities;
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
            return View(_publicationHouseService.GetAll());
        }
        public JsonResult List()
        {
            return Json(_publicationHouseService.GetAll().Select(x => new PublicationHouse
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }), JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(PublicationHouse publicationHouse)
        {
            _publicationHouseService.Add(publicationHouse);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(_publicationHouseService.GetById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(PublicationHouse publicationHouse)
        {
            try
            {
                _publicationHouseService.Edit(publicationHouse);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _publicationHouseService.Delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}