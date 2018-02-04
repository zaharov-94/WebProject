using BusinesLogicLayer.Services;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class AllPublicationController : Controller
    {
        private PublicationService _publicationService;

        public AllPublicationController()
        {
            _publicationService = new PublicationService(Settings.GetConnectionString());
        }
        // GET: Pulicatons
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(_publicationService.GetAllPublications(), JsonRequestBehavior.AllowGet);
        }
    }
}