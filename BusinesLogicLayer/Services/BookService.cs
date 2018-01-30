using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BusinesLogicLayer.Services
{
    public class BookService
    {
        private IGenericRepository<Book> _bookRepository;

        public BookService(string connectionString)
        {
            _bookRepository = new EntityRepository<Book>(connectionString);
        }
        public BookService(LibraryDbContext context)
        {
            _bookRepository = new EntityRepository<Book>(context);
        }
        public IEnumerable<Book> GetAll()
        {
            return _bookRepository.GetAll();
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
