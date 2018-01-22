using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.Models
{
    public class BookRepository
    {
        public List<Book> Books
        {
            get;
            set;
        }
        public BookRepository()
        {
            Books = new List<Book>();
            Books.Add(new Book { Name = "1", Author = "1", YearOfPublishing = 1999 });
            Books.Add(new Book { Name = "2", Author = "2", YearOfPublishing = 2006 });
        }
    }
}