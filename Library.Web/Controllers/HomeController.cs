using BusinesLogicLayer.Services;
using DataAccessLayer.Models;
using Library.Web.Entities;
using Library.Web.View_models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private BookService _bookService;
        private PublicationHouseService _publicationHouseService;
        LibraryDbContext context = new LibraryDbContext(Settings.GetConnectionString());

        public HomeController()
        {
            _bookService = new BookService(context);
            _publicationHouseService = new PublicationHouseService(context);
        }
        public ActionResult Index()
        {
            return View(_bookService.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            _bookService.Add(book);
            return RedirectToAction("Index");
        }
        public JsonResult GetAllPublish()
        {
            List<PublicationHouse> publicationHouses = new List<PublicationHouse>();
            
            foreach (var item in _publicationHouseService.GetAll())
            {
                publicationHouses.Add(new PublicationHouse { Id = item.Id, Name = item.Name, Address = item.Address, Books = null });
            }
            return Json(publicationHouses, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSelectedPublish(int id)
        {
            List<PublicationHouse> publicationHouses = new List<PublicationHouse>();
            foreach (var item in _bookService.GetById(id).PublicationHouses)
            {
                publicationHouses.Add(new PublicationHouse { Id = item.Id, Name = item.Name, Address = item.Address, Books = null });
            }
            return Json(publicationHouses, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int id)
        {
            var book = _bookService.GetById(id);
            var AllPublicationHouses = _publicationHouseService.GetAll();
            BookViewModel viewModel = new BookViewModel(book,
                AllPublicationHouses.ToList());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel bookViewModel, int id)
        {
            try
            {
                var book = _bookService.GetById(id);
                foreach (var item in bookViewModel.SelectedPublicationHouses)
                {
                    if (_publicationHouseService.GetById(item).Books.Where(x => x.Id == id).Count() == 0)
                    {
                        context.Books.Find(id).PublicationHouses.Add(_publicationHouseService.GetById(item));
                    }
                }
                List<PublicationHouse> listRemove = new List<PublicationHouse>();
                foreach (var item in bookViewModel.SelectedPublicationHouses)
                {
                        
                }

                context.Entry(book).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                string str = ex.InnerException + " " + ex.Message;
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}