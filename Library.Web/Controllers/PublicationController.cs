using Library.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class PublicationController : Controller
    {
        private PublicationRepository _publicationRepository;

        public PublicationController()
        {
            _publicationRepository = new PublicationRepository();
        }
        // GET: Pulicatons
        public ActionResult Index()
        {
            return View(_publicationRepository.AllPublications);
        }
    }
}