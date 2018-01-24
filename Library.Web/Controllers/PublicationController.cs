using BusinesLogicLayer.Services;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class PublicationController : Controller
    {
        private PublicationService _publicationService;

        public PublicationController()
        {
            _publicationService = new PublicationService(Settings.GetConnectionString());
        }
        // GET: Pulicatons
        public ActionResult Index()
        {
            return View(_publicationService.GetAllPublications());
        }
    }
}