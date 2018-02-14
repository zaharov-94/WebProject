using DataAccessLayer;
using Library.Web.Entities;
using System.Collections.Generic;
using Library.ViewModels.ViewModels;
using System.Linq;

namespace BusinesLogicLayer.Services
{
    public class BookService
    {
        private UnitOfWork _unitOfWork;

        public BookService(string connectionString)
        {
            _unitOfWork = new UnitOfWork(connectionString);
        }

        public IEnumerable<BookViewModel> GetAllBook()
        {
            IEnumerable<Book> listBook = _unitOfWork.Book.GetAll();
            List<BookViewModel> listBookView = new List<BookViewModel>();
            
            foreach(var item in listBook)
            {
                BookViewModel viewModel = new BookViewModel();
                viewModel.Id = item.Id;
                viewModel.Name = item.Name;
                viewModel.Author = item.Author;
                viewModel.YearOfPublishing = item.YearOfPublishing;
                listBookView.Add(viewModel);
            }

            return listBookView;
        }
        public IEnumerable<PublicationHouseViewModel> GetAllPublicationHouses()
        {
            List<PublicationHouseViewModel> listViewModel = new List<PublicationHouseViewModel>();
            
            foreach (var item in _unitOfWork.PublicationHouse.GetAll())
            {
                PublicationHouseViewModel publicationHouseViewModel = new PublicationHouseViewModel();
                publicationHouseViewModel.Id = item.Id;
                publicationHouseViewModel.Name = item.Name;
                publicationHouseViewModel.Address = item.Address;
                publicationHouseViewModel.Books = null;
                listViewModel.Add(publicationHouseViewModel);
            }
            return listViewModel;
        }
        public void Add(BookViewModel bookViewModel)
        {
            Book book = new Book();
            book.Name = bookViewModel.Name;
            book.Author = bookViewModel.Author;
            book.YearOfPublishing = bookViewModel.YearOfPublishing;
            book.PublicationHouses = ToPublicationHouse(bookViewModel.PublicationHouses).ToList();
            _unitOfWork.Book.Add(book);
        }
        public BookViewModel GetBookById(int id)
        {
            Book book = _unitOfWork.Book.FindById(id);
            BookViewModel bookViewModel = new BookViewModel();
            bookViewModel.Id = book.Id;
            bookViewModel.Name = book.Name;
            bookViewModel.Author = book.Author;
            bookViewModel.YearOfPublishing = book.YearOfPublishing;
            bookViewModel.PublicationHouses = book.PublicationHouses.Select(x => new PublicationHouseViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            }).ToList();
            return bookViewModel;
        }
        public void Edit(BookViewModel bookViewModel)
        {
            Book book = new Book();

            book.Id = bookViewModel.Id;
            book.Name = bookViewModel.Name;
            book.Author = bookViewModel.Author;
            book.YearOfPublishing = bookViewModel.YearOfPublishing;
            book.PublicationHouses = ToPublicationHouse(bookViewModel.PublicationHouses).ToList();
            
            _unitOfWork.Book.Update(book);
        }

        public void DeleteBook(int id)
        {
            _unitOfWork.Book.Remove(id);
        }

        private IEnumerable<PublicationHouse> ToPublicationHouse(ICollection<PublicationHouseViewModel> list)
        {
            List<PublicationHouse> listPublicationHouses = new List<PublicationHouse>();
            
            foreach(var item in list)
            {
                PublicationHouse publicationHouse = new PublicationHouse();
                publicationHouse.Id = item.Id;
                publicationHouse.Name = item.Name;
                publicationHouse.Address = item.Address;
                listPublicationHouses.Add(publicationHouse);
            }

            return listPublicationHouses;
        }
    }
}
