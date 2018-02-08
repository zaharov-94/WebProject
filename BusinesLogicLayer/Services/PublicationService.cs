using DataAccessLayer;
using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using Library.Web.Entities;
using System.Linq;
using System.Collections.Generic;
using Library.Entities.Enums;
using Library.ViewModels.ViewModels;

namespace BusinesLogicLayer.Services
{
    public class PublicationService
    {
        private UnitOfWork _unitOfWork;
        private IEnumerable<Publication> _publicationList;

        public PublicationService(string connectionString)
        {
            
            _unitOfWork = new UnitOfWork(connectionString);
        }
        public IEnumerable<Publication> GetAllPublications()
        {
            _publicationList = new List<Publication>();
            _publicationList = _unitOfWork.Book.GetAll().Select(x => new Publication { Name = x.Name, Type = PublicationType.Book })
                .Concat(_unitOfWork.Brochure.GetAll().Select(x => new Publication { Name = x.Name, Type = PublicationType.Brochure }))
                .Concat(_unitOfWork.Magazine.GetAll().Select(x => new Publication { Name = x.Name, Type = PublicationType.Magazine }));
            return _publicationList;
        }

        public IEnumerable<PublicationViewModel> GetAllInViewModel()
        {
            IEnumerable<PublicationViewModel> list = GetAllPublications()
                .Select(x => new PublicationViewModel { Name = x.Name, Type = x.Type.ToString() });
            return list;
        }
    }
}
