using DataAccessLayer;
using Library.Web.Entities;
using System.Collections.Generic;
using Library.ViewModels.ViewModels;
using System.Linq;
using System;
using Shared.Enums;

namespace BusinesLogicLayer.Services
{
    public class BrochureService
    {
        
        private UnitOfWork _unitOfWork;

        public BrochureService(string connectionString)
        {
            _unitOfWork = new UnitOfWork(connectionString);
        }
        public IEnumerable<BrochureViewModel> GetAll()
        {
            List<BrochureViewModel> list = new List<BrochureViewModel>();
            List<Brochure> brochureList = _unitOfWork.Brochure.GetAll().ToList();
            
            foreach (var item in brochureList)
            {
                BrochureViewModel brochureViewModel = new BrochureViewModel();
                brochureViewModel.Id = item.Id;
                brochureViewModel.Name = item.Name;
                brochureViewModel.NumberOfPages = item.NumberOfPages;
                brochureViewModel.TypeOfCover = item.TypeOfCover.ToString();
                list.Add(brochureViewModel);
            }
            return list;
        }
        
        public void Add(BrochureViewModel brochureViewModel)
        {
            _unitOfWork.Brochure.Add(ToBrochure(brochureViewModel));
        }

        public BrochureViewModel GetById(int id)
        {
            Brochure brochure = _unitOfWork.Brochure.FindById(id);
            BrochureViewModel brochureViewModel = new BrochureViewModel();
            brochureViewModel.Id = brochure.Id;
            brochureViewModel.Name = brochure.Name;
            brochureViewModel.NumberOfPages = brochure.NumberOfPages;
            brochureViewModel.TypeOfCover = brochure.TypeOfCover.ToString();

            return brochureViewModel;
        }

        public void Edit(BrochureViewModel brochureViewModel)
        {
            _unitOfWork.Brochure.Update(ToBrochure(brochureViewModel));
        }

        public void Delete(int id)
        {
            _unitOfWork.Brochure.Remove(id);
        }
        private Brochure ToBrochure(BrochureViewModel brochureViewModel)
        {
            Brochure brochure = new Brochure();
            brochure.Id = brochureViewModel.Id;
            brochure.Name = brochureViewModel.Name;
            brochure.NumberOfPages = brochureViewModel.NumberOfPages;
            brochure.TypeOfCover = (TypeOfCover)Enum.Parse(typeof(TypeOfCover), brochureViewModel.TypeOfCover);

            return brochure;
        }
    }
}
