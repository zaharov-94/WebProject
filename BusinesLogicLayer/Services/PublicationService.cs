using DataAccessLayer;
using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using Library.Web.Entities;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class PublicationService
    {
        private UnitOfWork _unitOfWork;
        private List<Publication> _publicationList;

        public PublicationService(string connectionString)
        {
            _publicationList = new List<Publication>();
            _unitOfWork = new UnitOfWork(connectionString);
        }
        public IEnumerable<Publication> GetAllPublications()
        {
            _publicationList.Clear();
            foreach (Book book in _unitOfWork.Book.GetAll())
            {
                _publicationList.Add(new Publication { Name = book.Name, Type = "Book" });
            }
            foreach (Brochure brochure in _unitOfWork.Brochure.GetAll())
            {
                _publicationList.Add(new Publication { Name = brochure.Name, Type = "Brochure" });
            }
            foreach (Magazine magazine in _unitOfWork.Magazine.GetAll())
            {
                _publicationList.Add(new Publication { Name = magazine.Name, Type = "Magazine" });
            }
            return _publicationList;
        }
    }
}
