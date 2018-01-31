using Library.Web.Entities;
using System.Collections.Generic;

namespace Library.Web.View_models
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public List<int> PublicationHouses { get; set; }


        public BookViewModel()
        {

        }

        public BookViewModel(Book _book, List<PublicationHouse> _publicationHouses)
        {
            Book = _book;
            PublicationHouses = new List<int>();
        }
    }
}