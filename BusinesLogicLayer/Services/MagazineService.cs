using Library.Web.Entities;
using Library.Web.Models;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class MagazineService
    {
        private MagazineRepository _magazineRepository;

        public MagazineService(string connectionString)
        {
            _magazineRepository = new MagazineRepository(connectionString);
        }
        public List<Magazine> GetAll()
        {
            return _magazineRepository.Magazines;
        }

        public void Add(Magazine magazine)
        {
            _magazineRepository.Add(magazine);
        }

        public Magazine GetById(int id)
        {
            return _magazineRepository.GetById(id);
        }

        public void Edit(Magazine magazine)
        {
            _magazineRepository.Update(magazine);
        }
        public void Delete(int id)
        {
            _magazineRepository.Delete(id);
        }
    }
}
