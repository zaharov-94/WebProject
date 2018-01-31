using DataAccessLayer.Abstract;
using DataAccessLayer;
using Library.Web.Entities;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class BrochureService
    {
        private IGenericRepository<Brochure> _brochureRepository;
        private UnitOfWork _unitOfWork;

        public BrochureService(string connectionString)
        {
            _unitOfWork = new UnitOfWork(connectionString);
            _brochureRepository = _unitOfWork.Repository<Brochure>();
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
