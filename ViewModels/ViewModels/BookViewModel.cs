using Library.Web.Entities;
using System.Collections.Generic;

namespace ViewModels.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public List<PublicationHouse> PublicationHouses { get; set; }
        public BookViewModel(){}
        public BookViewModel(Book _book, List<PublicationHouse> _publicationHouses)
        {
            Book = _book;
            PublicationHouses = new List<PublicationHouse>();
        }
    }
}