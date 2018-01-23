using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.Models
{
    public class BookRepository
    {
        private FileStreamManager _fileStreamManager;

        public List<Book> Books
        {
            get;
            set;
        }
        public BookRepository()
        {
            Books = new List<Book>();
            _fileStreamManager = new FileStreamManager();
            Books = _fileStreamManager.Read();
        }
        public void AddBook(Book book)
        {
            _fileStreamManager.Write(book);
            Books.Add(book);
        }
    }
}