using Library.Web.Entities;
using Library.Web.Models;
using System.Text;
using System.Web.Mvc;

namespace Library.Web.Controllers
{
    public class HomeController : Controller
    {
        BookRepository _bookRepository;
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
            return RedirectToAction("Index", ReCaptcha());
        }
        public FileContentResult ReCaptcha()
        {
            string str = "Hello World";
            byte[] b1 = Encoding.ASCII.GetBytes(str);
            return File(b1, "txt", "1");
        }
    }
}