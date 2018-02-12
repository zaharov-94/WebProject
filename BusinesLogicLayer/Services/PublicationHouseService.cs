using DataAccessLayer;
using Library.ViewModels.ViewModels;
using Library.Web.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BusinesLogicLayer.Services
{
    public class PublicationHouseService
    {
        private UnitOfWork _unitOfWork;

        public PublicationHouseService(string connectionString)
        {
            _unitOfWork = new UnitOfWork(connectionString);
        }

        public IEnumerable<PublicationHouseViewModel> GetAll()
        {
            return _unitOfWork.PublicationHouse.GetAll()
                .Select( x => new PublicationHouseViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                });
        }

        public void Add(PublicationHouseViewModel publicationHouse)
        {
            _unitOfWork.PublicationHouse.Add(ToPublicationHouse(publicationHouse));
        }

        public PublicationHouseViewModel GetById(int id)
        {
            PublicationHouse publicationHouse = _unitOfWork.PublicationHouse.FindById(id);
            return new PublicationHouseViewModel
            {
                Id = publicationHouse.Id,
                Name = publicationHouse.Name,
                Address = publicationHouse.Address
            };
        }

        public void Edit(PublicationHouseViewModel publicationHouse)
        {
            _unitOfWork.PublicationHouse.Update(ToPublicationHouse(publicationHouse));
        }
        public void Delete(int id)
        {
            _unitOfWork.PublicationHouse.Remove(id);
        }

        private PublicationHouse ToPublicationHouse(PublicationHouseViewModel publicationHouseViewModel)
        {
            return new PublicationHouse
            {
                Id = publicationHouseViewModel.Id,
                Name = publicationHouseViewModel.Name,
                Address = publicationHouseViewModel.Address,
                Books = (ICollection<Book>)publicationHouseViewModel.Books
            };
        }

    }
}
