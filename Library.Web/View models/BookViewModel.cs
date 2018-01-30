using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library.Web.View_models
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public List<int> SelectedPublicationHouses { get; set; }

        public virtual List<PublicationHouse> PublicationHouses { get; set; }

        public BookViewModel()
        {

        }

        public BookViewModel(Book _book, List<PublicationHouse> _publicationHouses)
        {
            Book = _book;
            PublicationHouses = _publicationHouses;
            SelectedPublicationHouses = new List<int>();
        }
    }
}