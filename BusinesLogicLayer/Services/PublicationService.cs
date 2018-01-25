using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using Library.Web.Entities;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class PublicationService
    {
        private IGenericRepository<Book> _bookRepository;
        private IGenericRepository<Brochure> _brochureRepository;
        private IGenericRepository<Magazine> _magazineRepository;
        private List<Publication> _publicationList;
        public PublicationService(string connectionString)
        {
            _publicationList = new List<Publication>();
            _bookRepository = new AdoRepository<Book>(connectionString);
            _brochureRepository = new AdoRepository<Brochure>(connectionString);
            _magazineRepository = new AdoRepository<Magazine>(connectionString);
        }
        public IEnumerable<Publication> GetAllPublications()
        {
            _publicationList.Clear();
            foreach (Book book in _bookRepository.GetAll())
            {
                _publicationList.Add(new Publication { Name = book.Name, Type = "Book" });
            }
            foreach (Brochure brochure in _brochureRepository.GetAll())
            {
                _publicationList.Add(new Publication { Name = brochure.Name, Type = "Brochure" });
            }
            foreach (Magazine magazine in _magazineRepository.GetAll())
            {
                _publicationList.Add(new Publication { Name = magazine.Name, Type = "Magazine" });
            }
            return _publicationList;
        }
    }
}
