using DataAccessLayer;
using Library.ViewModels.ViewModels;
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
        public IEnumerable<MagazineViewModel> GetAll()
        {
            List<MagazineViewModel> list = new List<MagazineViewModel>();
            foreach (var magazine in _unitOfWork.Magazine.GetAll())
            {
                list.Add(new MagazineViewModel
                {
                    Id = magazine.Id,
                    Name = magazine.Name,
                    Number = magazine.Number,
                    YearOfPublishing = magazine.YearOfPublishing
                });
            }
            return list;
        }

        public void Add(MagazineViewModel magazineViewModel)
        {
            _unitOfWork.Magazine.Add(ToMagazine(magazineViewModel));
        }

        public MagazineViewModel GetById(int id)
        {
            Magazine magazine = _unitOfWork.Magazine.FindById(id);
            return new MagazineViewModel
            {
                Id = magazine.Id,
                Name = magazine.Name,
                Number = magazine.Number,
                YearOfPublishing = magazine.YearOfPublishing
            };
        }

        public void Edit(MagazineViewModel magazineViewModel)
        {
            _unitOfWork.Magazine.Update(ToMagazine(magazineViewModel));
        }
        public void Delete(int id)
        {
            _unitOfWork.Magazine.Remove(id);
        }

        private Magazine ToMagazine(MagazineViewModel magazineViewModel)
        {
            return new Magazine
            {
                Id = magazineViewModel.Id,
                Name = magazineViewModel.Name,
                Number = magazineViewModel.Number,
                YearOfPublishing = magazineViewModel.YearOfPublishing
            };
        }
    }
}
