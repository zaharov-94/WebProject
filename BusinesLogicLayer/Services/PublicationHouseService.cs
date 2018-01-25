using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using Library.Web.Entities;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class PublicationHouseService
    {
        private IGenericRepository<PublicationHouse> _houseRepository;

        public PublicationHouseService(string connectionString)
        {
            _houseRepository = new EntityRepository<PublicationHouse>(connectionString);
        }
        public IEnumerable<PublicationHouse> GetAll()
        {
            return _houseRepository.GetAll();
        }

        public void Add(PublicationHouse publicationHouse)
        {
            _houseRepository.Add(publicationHouse);
        }

        public PublicationHouse GetById(int id)
        {
            return _houseRepository.FindById(id);
        }

        public void Edit(PublicationHouse publicationHouse)
        {
            _houseRepository.Update(publicationHouse);
        }
        public void Delete(int id)
        {
            _houseRepository.Remove(id);
        }
    }
}
