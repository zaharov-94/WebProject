using BusinesLogicLayer.Services;
using DataAccessLayer.Models;
using Library.ViewModels.ViewModels;
using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private BookService _bookService;
        
        public BookController()
        {
            _bookService = new BookService(Settings.GetConnectionString());
        }
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult List()
        {
            return Json(_bookService.GetAllBook().Select(x => new Book {Id=x.Id, Name=x.Name,
                Author =x.Author, YearOfPublishing=x.YearOfPublishing }), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public ActionResult Create(Book book)
        {
            _bookService.Add(book);
            return RedirectToAction("Index");
        }
        public JsonResult GetAllPublish()
        {
            IEnumerable<PublicationHouse> publicationHouses = _bookService.GetAllPublicationHouses()
                .Select(x => new PublicationHouse { Id = x.Id, Name = x.Name, Address = x.Address, Books = null });

            return Json(publicationHouses, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSelectedPublish(int id)
        {
            IEnumerable<PublicationHouse> publicationHouses = _bookService.GetBookById(id).PublicationHouses
                .Select(x => new PublicationHouse { Id = x.Id, Name = x.Name, Address = x.Address, Books = null });
            return Json(publicationHouses, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            var book = _bookService.GetBookById(id);
            var PublicationHouses = _bookService.GetAllPublicationHouses();
            BookViewModel viewModel = new BookViewModel(book, PublicationHouses.ToList());
            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(BookViewModel bookViewModel)
        {
            _bookService.Edit(bookViewModel);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}