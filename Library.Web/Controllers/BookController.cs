using BusinesLogicLayer.Services;
using Library.ViewModels.ViewModels;
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
            return Json(_bookService.GetAllBook(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(BookViewModel book)
        {
            _bookService.Add(book);
            return RedirectToAction("Index");
        }
        public JsonResult GetAllPublish()
        {
            return Json(_bookService.GetAllPublicationHouses(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSelectedPublish(int id)
        {
            IEnumerable<PublicationHouseViewModel> publicationHouses = _bookService.GetBookById(id).PublicationHouses
                .Select(x => new PublicationHouseViewModel { Id = x.Id, Name = x.Name, Address = x.Address, Books = null });
            return Json(publicationHouses, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            BookViewModel viewModel = _bookService.GetBookById(id);
            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(BookViewModel bookViewModel)
        {
            _bookService.Edit(bookViewModel);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}