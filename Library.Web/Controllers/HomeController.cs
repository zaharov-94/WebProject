using BusinesLogicLayer.Services;
using Library.Web.Entities;
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

        public ActionResult Edit(int id)
        {
            return View(_bookService.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            try
            {
                _bookService.Edit(book);
                return RedirectToAction("Index");
            }
            catch
            {
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