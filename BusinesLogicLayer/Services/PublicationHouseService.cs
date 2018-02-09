using DataAccessLayer;
using Library.Web.Entities;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class PublicationHouseService
    {
        private UnitOfWork _unitOfWork;

        public PublicationHouseService(string connectionString)
        {
            _unitOfWork = new UnitOfWork(connectionString);
        }

        public IEnumerable<PublicationHouse> GetAll()
        {
            return _unitOfWork.PublicationHouse.GetAll();
        }

        public void Add(PublicationHouse publicationHouse)
        {
            _unitOfWork.PublicationHouse.Add(publicationHouse);
        }

        public PublicationHouse GetById(int id)
        {
            return _unitOfWork.PublicationHouse.FindById(id);
        }

        public void Edit(PublicationHouse publicationHouse)
        {
            _unitOfWork.PublicationHouse.Update(publicationHouse);
        }
        public void Delete(int id)
        {
            _unitOfWork.PublicationHouse.Remove(id);
        }
    }
}
