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
            return _unitOfWork.Book.GetAll()
                .Select(x => new BookViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Author = x.Author,
                    YearOfPublishing = x.YearOfPublishing
                });
        }
        public IEnumerable<PublicationHouseViewModel> GetAllPublicationHouses()
        {
            return _unitOfWork.PublicationHouse.GetAll()
                .Select(x => new PublicationHouseViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Books = null
                });
            
        }
        public void Add(BookViewModel bookViewModel)
        {
            Book book = new Book
            {
                Name = bookViewModel.Name,
                Author = bookViewModel.Author,
                YearOfPublishing = bookViewModel.YearOfPublishing,
                PublicationHouses = ToPublicationHouse(bookViewModel.PublicationHouses).ToList()
            };
            _unitOfWork.Book.Add(book);
        }
        public BookViewModel GetBookById(int id)
        {
            Book book = _unitOfWork.Book.FindById(id);
            return new BookViewModel
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                YearOfPublishing = book.YearOfPublishing,
                PublicationHouses = book.PublicationHouses.Select(x => new PublicationHouseViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                }).ToList()
            };
        }
        public void Edit(BookViewModel bookViewModel)
        {
            Book book = new Book
            {
                Id = bookViewModel.Id,
                Name = bookViewModel.Name,
                Author = bookViewModel.Author,
                YearOfPublishing = bookViewModel.YearOfPublishing,
                PublicationHouses = ToPublicationHouse(bookViewModel.PublicationHouses).ToList()
            };
            _unitOfWork.Book.Update(book);
        }

        public void DeleteBook(int id)
        {
            _unitOfWork.Book.Remove(id);
        }

        private IEnumerable<PublicationHouse> ToPublicationHouse(ICollection<PublicationHouseViewModel> list)
        {
            return list.Select(x => new PublicationHouse
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address
            });
        }
    }
}
