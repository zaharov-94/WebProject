using Library.Web.Entities;
using Library.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class BrochureController : Controller
    {
        private BrochureRepository _brochureRepository;

        public BrochureController()
        {
            _brochureRepository = new BrochureRepository();
        }
        public ActionResult Index()
        {
            return View(_brochureRepository.Brochures);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Brochure brochure)
        {
            _brochureRepository.Add(brochure);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(_brochureRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Brochure brochure)
        {
            try
            {
                _brochureRepository.Updete(brochure);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            _brochureRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}