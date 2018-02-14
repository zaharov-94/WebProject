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
            List<PublicationHouseViewModel> listViewModel = new List<PublicationHouseViewModel>();
            
            foreach (var item in _unitOfWork.PublicationHouse.GetAll())
            {
                PublicationHouseViewModel publicationHouseViewModel = new PublicationHouseViewModel();
                publicationHouseViewModel.Id = item.Id;
                publicationHouseViewModel.Name = item.Name;
                publicationHouseViewModel.Address = item.Address;
                listViewModel.Add(publicationHouseViewModel);
            }
            return listViewModel;
        }

        public void Add(PublicationHouseViewModel publicationHouse)
        {
            _unitOfWork.PublicationHouse.Add(ToPublicationHouse(publicationHouse));
        }

        public PublicationHouseViewModel GetById(int id)
        {
            PublicationHouse publicationHouse = _unitOfWork.PublicationHouse.FindById(id);
            PublicationHouseViewModel publicationHouseViewModel = new PublicationHouseViewModel();
            publicationHouseViewModel.Id = publicationHouse.Id;
            publicationHouseViewModel.Name = publicationHouse.Name;
            publicationHouseViewModel.Address = publicationHouse.Address;

            return publicationHouseViewModel;
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
            PublicationHouse publicationHouse = new PublicationHouse();
            publicationHouse.Id = publicationHouseViewModel.Id;
            publicationHouse.Name = publicationHouseViewModel.Name;
            publicationHouse.Address = publicationHouseViewModel.Address;
            publicationHouse.Books = (ICollection<Book>)publicationHouseViewModel.Books;

            return publicationHouse;
        }

    }
}
