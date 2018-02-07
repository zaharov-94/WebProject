using DataAccessLayer;
using DataAccessLayer.Abstract;
using DataAccessLayer.Models;
using Library.Web.Entities;
using System.Collections.Generic;

namespace BusinesLogicLayer.Services
{
    public class MagazineService
    {
        private UnitOfWork _unitOfWork;

        public MagazineService(string connectionString)
        {
            _unitOfWork = new UnitOfWork(connectionString);
        }
        public IEnumerable<Magazine> GetAll()
        {
            return _unitOfWork.Magazine.GetAll();
        }

        public void Add(Magazine magazine)
        {
            _unitOfWork.Magazine.Add(magazine);
        }

        public Magazine GetById(int id)
        {
            return _unitOfWork.Magazine.FindById(id);
        }

        public void Edit(Magazine magazine)
        {
            _unitOfWork.Magazine.Update(magazine);
        }
        public void Delete(int id)
        {
            _unitOfWork.Magazine.Remove(id);
        }
    }
}
