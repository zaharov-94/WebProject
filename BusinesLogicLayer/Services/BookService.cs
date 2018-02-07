using DataAccessLayer.Models;
using DataAccessLayer;
using Library.Web.Entities;
using System.Collections.Generic;
using Library.ViewModels.ViewModels;

namespace BusinesLogicLayer.Services
{
    public class BookService
    {
        private UnitOfWork _unitOfWork;

        public BookService(string connectionString)
        {
            _unitOfWork = new UnitOfWork(connectionString);
        }
        public BookService(ApplicationContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }
        public IEnumerable<Book> GetAllBook()
        {
            return _unitOfWork.Book.GetAll();
        }
        public IEnumerable<PublicationHouse> GetAllPublicationHouses()
        {
            return _unitOfWork.PublicationHouse.GetAll();
        }
        public void Add(Book book)
        {
            _unitOfWork.Book.Add(book);
        }
        public void Add(PublicationHouse publicationHouse)
        {
            _unitOfWork.PublicationHouse.Add(publicationHouse);
        }
        public Book GetBookById(int id)
        {
            return _unitOfWork.Book.FindById(id);
        }
        public PublicationHouse GetPublicationHouseById(int id)
        {
            return _unitOfWork.PublicationHouse.FindById(id);
        }
        public void Edit(BookViewModel bookViewModel)
        {
            Book book = new Book {Id = bookViewModel.Book.Id, Name = bookViewModel.Book.Name, Author = bookViewModel.Book.Author,
                YearOfPublishing = bookViewModel.Book.YearOfPublishing, PublicationHouses = bookViewModel.PublicationHouses };
            _unitOfWork.Book.Update(book);
        }
        public void DeleteBook(int id)
        {
            _unitOfWork.Book.Remove(id);
        }
        public void DeletePublicationHouse(int id)
        {
            _unitOfWork.PublicationHouse.Remove(id);
        }
    }
}
