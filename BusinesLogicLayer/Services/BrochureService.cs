using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using Library.Web.Entities;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class BrochureService
    {
        private IGenericRepository<Brochure> _brochureRepository;

        public BrochureService(string connectionString)
        {
            _brochureRepository = new AdoRepository<Brochure>(connectionString);
        }
        public IEnumerable<Brochure> GetAll()
        {
            return _brochureRepository.GetAll();
        }

        public void Add(Brochure brochure)
        {
            _brochureRepository.Add(brochure);
        }

        public Brochure GetById(int id)
        {
            return _brochureRepository.FindById(id);
        }

        public void Edit(Brochure brochure)
        {
            _brochureRepository.Update(brochure);
        }
        public void Delete(int id)
        {
            _brochureRepository.Remove(id);
        }
    }
}
