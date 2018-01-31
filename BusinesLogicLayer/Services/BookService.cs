using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using DataAccessLayer;
using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Library.Web.View_models;

namespace BusinesLogicLayer.Services
{
    public class BookService
    {
        private IGenericRepository<Book> _bookRepository;
        private IGenericRepository<PublicationHouse> _publicationHouseRepository;
        private UnitOfWork _unitOfWork;

        public BookService(string connectionString)
        {
            _unitOfWork = new UnitOfWork(connectionString);
            _bookRepository = _unitOfWork.Repository<Book>(); ;
            _publicationHouseRepository = _unitOfWork.Repository<PublicationHouse>();
        }
        public BookService(LibraryDbContext context)
        {
            _unitOfWork = new UnitOfWork(context);
            _bookRepository = new EntityBookRepository<Book>(context);
            _publicationHouseRepository = _unitOfWork.Repository<PublicationHouse>();
        }
        public IEnumerable<Book> GetAllBook()
        {
            return _bookRepository.GetAll();
        }
        public IEnumerable<PublicationHouse> GetAllPublicationHouses()
        {
            return _publicationHouseRepository.GetAll();
        }
        public void Add(Book book)
        {
            _bookRepository.Add(book);
        }
        public void Add(PublicationHouse publicationHouse)
        {
            _publicationHouseRepository.Add(publicationHouse);
        }
        public Book GetBookById(int id)
        {
            return _bookRepository.FindById(id);
        }
        public PublicationHouse GetPublicationHouseById(int id)
        {
            return _publicationHouseRepository.FindById(id);
        }
        public void Edit(BookViewModel bookViewModel)
        {
            _bookRepository.Update(bookViewModel);
        }
        public void DeleteBook(int id)
        {
            _bookRepository.Remove(id);
        }
        public void DeletePublicationHouse(int id)
        {
            _publicationHouseRepository.Remove(id);
        }
    }
}
