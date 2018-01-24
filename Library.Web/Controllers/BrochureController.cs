using Library.Web.Entities;
using BusinesLogicLayer.Services;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class BrochureController : Controller
    {
        private BrochureService _brochureService;

        public BrochureController()
        {
            _brochureService = new BrochureService(GetConnectionString());
        }
        public ActionResult Index()
        {
            return View(_brochureService.GetAll());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brochure brochure)
        {
            _brochureService.Add(brochure);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(_brochureService.GetById(id));
        }

        [HttpPost]
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
        public ActionResult Delete(int id)
        {
            _brochureService.Delete(id);
            return RedirectToAction("Index");
        }
        private string GetConnectionString()
        {
            System.Configuration.Configuration rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/MyWebSiteRoot");
            System.Configuration.ConnectionStringSettings connString;
            if (0 < rootWebConfig.ConnectionStrings.ConnectionStrings.Count)
            {
                connString = rootWebConfig.ConnectionStrings.ConnectionStrings["PublicationsContext"];
                if (null != connString)
                    return connString.ConnectionString;
            }
            return "";
        }
    }
}