using Library.Web.Entities;
using Library.Web.Models;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class BookService
    {
        private BookRepository _bookRepository;

        public BookService(string connectionString)
        {
            _bookRepository = new BookRepository(connectionString);
        }
        public List<Book> GetAll()
        {
            return _bookRepository.Books;
        }

        public void Add(Book book)
        {
            _bookRepository.Add(book);
        }

        public Book GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public void Edit(Book book)
        {
            _bookRepository.Update(book);
        }
        public void Delete(int id)
        {
            _bookRepository.Delete(id);
        }
    }
}
