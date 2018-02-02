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
    public class BookController : Controller
    {
        private BookService _bookService;
        
        public BookController()
        {
            _bookService = new BookService(Settings.GetConnectionString());
        }
        public ActionResult Index()
        {
            return View(_bookService.GetAllBook());
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

            foreach (var item in _bookService.GetAllPublicationHouses())
            {
                publicationHouses.Add(new PublicationHouse { Id = item.Id, Name = item.Name, Address = item.Address, Books = null });
            }
            //string publicationHouses = "[";

            //foreach (var item in _bookService.GetAllPublicationHouses())
            //{
            //    publicationHouses+="{Id:"+item.Id+", Name:"+item.Name+", Address:"+item.Address+", Books:null },";
            //}
            //publicationHouses = publicationHouses.Remove(publicationHouses.Length - 1, 1)+"]";

            return Json(publicationHouses, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSelectedPublish(int id)
        {
            List<PublicationHouse> publicationHouses = new List<PublicationHouse>();
            foreach (var item in _bookService.GetBookById(id).PublicationHouses)
            {
                publicationHouses.Add(new PublicationHouse { Id = item.Id, Name = item.Name, Address = item.Address, Books = null });
            }
            return Json(publicationHouses, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Edit(int id)
        {
            var book = _bookService.GetBookById(id);
            var PublicationHouses = _bookService.GetAllPublicationHouses();
            BookViewModel viewModel = new BookViewModel(book, PublicationHouses.ToList());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel bookViewModel)
        {
            try
            {
                _bookService.Edit(bookViewModel);
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
            _bookService.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}