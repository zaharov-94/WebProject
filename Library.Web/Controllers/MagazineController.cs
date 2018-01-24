using BusinesLogicLayer.Services;
using Library.Web.Entities;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class MagazineController : Controller
    {
        private MagazineService _magazineService;

        public MagazineController()
        {
            _magazineService = new MagazineService(Settings.GetConnectionString());
        }
        // GET: Magazine
        public ActionResult Index()
        {
            return View(_magazineService.GetAll());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Magazine magazine)
        {
            _magazineService.Add(magazine);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(_magazineService.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Magazine magazine)
        {
            try
            {
                _magazineService.Edit(magazine);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            _magazineService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}