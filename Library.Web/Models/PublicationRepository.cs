using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.Models
{
    public class PublicationRepository
    {
        private BookRepository _bookRepository;
        private BrochureRepository _brochureRepository;
        private MagazineRepository _magazineRepository;
        private List<Publication> _publicationList;
        public PublicationRepository()
        {
            _publicationList = new List<Publication>();
            _bookRepository = new BookRepository();
            _brochureRepository = new BrochureRepository();
            _magazineRepository = new MagazineRepository();
        }
        public List<Publication> GetAllPublications()
        {
            _publicationList.Clear();
            foreach (Book book in _bookRepository.Books)
            {
                _publicationList.Add(new Publication { Name = book.Name, Type = "Book" });
            }
            foreach (Brochure brochure in _brochureRepository.Brochures)
            {
                _publicationList.Add(new Publication { Name = brochure.Name, Type = "Brochure" });
            }
            foreach (Magazine magazine in _magazineRepository.Magazines)
            {
                _publicationList.Add(new Publication { Name = magazine.Name, Type = "Magazine" });
            }
            return _publicationList;
        }
    }
}