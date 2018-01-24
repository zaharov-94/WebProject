using BusinesLogicLayer.Services;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class PublicationController : Controller
    {
        private PublicationService _publicationService;

        public PublicationController()
        {
            _publicationService = new PublicationService(GetConnectionString());
        }
        // GET: Pulicatons
        public ActionResult Index()
        {
            return View(_publicationService.GetAllPublications());
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