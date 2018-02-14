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
            IEnumerable<Magazine> listMagazine = _unitOfWork.Magazine.GetAll();
            
            foreach (var magazine in listMagazine)
            {
                MagazineViewModel magazineViewItem = new MagazineViewModel();
                magazineViewItem.Id = magazine.Id;
                magazineViewItem.Name = magazine.Name;
                magazineViewItem.Number = magazine.Number;
                magazineViewItem.YearOfPublishing = magazine.YearOfPublishing;
                list.Add(magazineViewItem);
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
            MagazineViewModel magazineViewModel = new MagazineViewModel();
            magazineViewModel.Id = magazine.Id;
            magazineViewModel.Name = magazine.Name;
            magazineViewModel.Number = magazine.Number;
            magazineViewModel.YearOfPublishing = magazine.YearOfPublishing;

            return magazineViewModel;
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
            Magazine magazine = new Magazine();
            magazine.Id = magazineViewModel.Id;
            magazine.Name = magazineViewModel.Name;
            magazine.Number = magazineViewModel.Number;
            magazine.YearOfPublishing = magazineViewModel.YearOfPublishing;
            return magazine;
        }
    }
}
