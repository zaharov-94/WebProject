using Library.Web.Entities;
using Library.Web.Models;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class PublicationService
    {
        private BookRepository _bookRepository;
        private BrochureRepository _brochureRepository;
        private MagazineRepository _magazineRepository;
        private List<Publication> _publicationList;
        public PublicationService(string connectionString)
        {
            _publicationList = new List<Publication>();
            _bookRepository = new BookRepository(connectionString);
            _brochureRepository = new BrochureRepository(connectionString);
            _magazineRepository = new MagazineRepository(connectionString);
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
