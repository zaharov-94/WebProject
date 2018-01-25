using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using Library.Web.Entities;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class MagazineService
    {
        private IGenericRepository<Magazine> _magazineRepository;

        public MagazineService(string connectionString)
        {
            _magazineRepository = new AdoRepository<Magazine>(connectionString);
        }
        public IEnumerable<Magazine> GetAll()
        {
            return _magazineRepository.GetAll();
        }

        public void Add(Magazine magazine)
        {
            _magazineRepository.Add(magazine);
        }

        public Magazine GetById(int id)
        {
            return _magazineRepository.FindById(id);
        }

        public void Edit(Magazine magazine)
        {
            _magazineRepository.Update(magazine);
        }
        public void Delete(int id)
        {
            _magazineRepository.Remove(id);
        }
    }
}
