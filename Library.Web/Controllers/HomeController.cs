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

        public HomeController()
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
            var AllPublicationHouses = _bookService.GetAllPublicationHouses();
            BookViewModel viewModel = new BookViewModel(book,
                AllPublicationHouses.ToList());
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(BookViewModel bookViewModel, int id)
        {
            try
            {
                //foreach (var item in bookViewModel.SelectedPublicationHouses)
                //{
                //    if (_publicationHouseService.GetById(item).Books.Where(x => x.Id == id).Count() == 0)
                //    {
                //        context.Books.Find(id).PublicationHouses.Add(_publicationHouseService.GetById(item));
                //    }
                //}
                //List<PublicationHouse> listRemove = new List<PublicationHouse>();
                //foreach (var item in context.Books.Find(id).PublicationHouses)
                //{
                //    if(!bookViewModel.SelectedPublicationHouses.Contains(item.Id))
                //    {
                //        listRemove.Add(item);
                //    }
                //}
                //foreach (var item in listRemove)
                //{
                //    context.Books.Find(id).PublicationHouses.Remove(item);
                //}
                //Book book = new Book
                //{
                //    Name = bookViewModel.Book.Name,
                //    Author = bookViewModel.Book.Author,
                //    YearOfPublishing = bookViewModel.Book.YearOfPublishing                  
                //};
                ////_bookService.Edit(book);
                //context.Books.Find(id).Name = book.Name;
                //context.Books.Find(id).Author = book.Author;
                //context.Books.Find(id).YearOfPublishing = book.YearOfPublishing;
                //context.Entry(book).State = System.Data.Entity.EntityState.Detached;
                //context.SaveChanges();

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