using Library.Web.Entities;
using Library.Web.Models;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class BrochureService
    {
        private BrochureRepository _brochureRepository;

        public BrochureService(string connectionString)
        {
            _brochureRepository = new BrochureRepository(connectionString);
        }
        public List<Brochure> GetAll()
        {
            return _brochureRepository.Brochures;
        }

        public void Add(Brochure brochure)
        {
            _brochureRepository.Add(brochure);
        }

        public Brochure GetById(int id)
        {
            return _brochureRepository.GetById(id);
        }

        public void Edit(Brochure brochure)
        {
            _brochureRepository.Update(brochure);
        }
        public void Delete(int id)
        {
            _brochureRepository.Delete(id);
        }
    }
}
