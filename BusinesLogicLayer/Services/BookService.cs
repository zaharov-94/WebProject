using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using Library.Web.Entities;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class BookService
    {
        private IGenericRepository<Book> _bookRepository;

        public BookService(string connectionString)
        {
            _bookRepository = new AdoRepository<Book>(connectionString);
        }
        public List<Book> GetAll()
        {
            return (List<Book>)_bookRepository.GetAll();
        }

        public void Add(Book book)
        {
            _bookRepository.Add(book);
        }

        public Book GetById(int id)
        {
            return _bookRepository.FindById(id);
        }

        public void Edit(Book book)
        {
            _bookRepository.Update(book);
        }
        public void Delete(int id)
        {
            _bookRepository.Remove(id);
        }
    }
}
