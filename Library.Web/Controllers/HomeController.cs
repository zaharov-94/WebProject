using Library.Web.Entities;
using Library.Web.Models;
using System.Text;
using System.Web;
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
            //HttpPostedFileBase upload;
            //    byte[] avatar = new byte[100];
            //    upload.InputStream.Read(avatar, 0, upload.ContentLength);
            //    // получаем имя файла
            //    string fileName = "books.txt";
            //    // сохраняем файл в папку Files в проекте
            //    upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            return RedirectToAction("Index");
        }
        public FileContentResult ReCaptcha()
        {
            string str = "Hello World";
            byte[] b1 = Encoding.ASCII.GetBytes(str);

            return File(b1, "txt", "1");
        }
    }
}