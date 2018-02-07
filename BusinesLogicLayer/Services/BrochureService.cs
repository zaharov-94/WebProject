using DataAccessLayer.Abstract;
using DataAccessLayer;
using Library.Web.Entities;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class BrochureService
    {
        
        private UnitOfWork _unitOfWork;

        public BrochureService(string connectionString)
        {
            _unitOfWork = new UnitOfWork(connectionString);
        }
        public IEnumerable<Brochure> GetAll()
        {
            return _unitOfWork.Brochure.GetAll();
        }

        public void Add(Brochure brochure)
        {
            _unitOfWork.Brochure.Add(brochure);
        }

        public Brochure GetById(int id)
        {
            return _unitOfWork.Brochure.FindById(id);
        }

        public void Edit(Brochure brochure)
        {
            _unitOfWork.Brochure.Update(brochure);
        }
        public void Delete(int id)
        {
            _unitOfWork.Brochure.Remove(id);
        }
    }
}
