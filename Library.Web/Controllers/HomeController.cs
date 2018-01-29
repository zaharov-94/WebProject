using BusinesLogicLayer.Services;
using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private BookService _bookService;
        private PublicationHouseService _publicationHouseService;
        public class Bookt
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Author { get; set; }

            public int YearOfPublishing { get; set; }

            public virtual ICollection<string> PublicationHouses { get; set; }
        }
        public HomeController()
        {
            _bookService = new BookService(Settings.GetConnectionString());
            _publicationHouseService = new PublicationHouseService(Settings.GetConnectionString());
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
            return View(_bookService.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Bookt bookt)
        {
            try
            {
                Book book = new Book { Name = bookt.Name, Author = bookt.Author, YearOfPublishing = bookt.YearOfPublishing };

                foreach (var item in bookt.PublicationHouses)
                {
                    book.PublicationHouses.Add(_publicationHouseService.GetById(int.Parse(item)));
                }
                _bookService.GetById(bookt.Id).Name = bookt.Name;
                _bookService.GetById(bookt.Id).Author = bookt.Author;
                _bookService.GetById(bookt.Id).YearOfPublishing = bookt.YearOfPublishing;
                foreach (var item in book.PublicationHouses)
                {
                    _bookService.GetById(bookt.Id).PublicationHouses.Add(item);
                }

                _bookService.Edit(_bookService.GetById(bookt.Id));
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