using Library.Web.Entities;
using Library.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class MagazineController : Controller
    {
        private MagazineRepository _magazineRepository;

        public MagazineController()
        {
            _magazineRepository = new MagazineRepository();
        }
        // GET: Magazine
        public ActionResult Index()
        {
            return View(_magazineRepository.Magazines);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Magazine magazine)
        {
            _magazineRepository.Add(magazine);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View(_magazineRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Magazine magazine)
        {
            try
            {
                _magazineRepository.Updete(magazine);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            _magazineRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}