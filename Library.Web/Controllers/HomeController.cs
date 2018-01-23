using Library.Web.Entities;
using Library.Web.Models;
using System;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private BookRepository _bookRepository;

        public HomeController()
        {
            _bookRepository = new BookRepository();
        }
        public ActionResult Index()
        {
            return View(_bookRepository.Books);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Book book)
        {
            _bookRepository.AddBook(book);
            return RedirectToAction("Index");
        }
    }
}