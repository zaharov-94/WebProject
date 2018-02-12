using DataAccessLayer;
using Library.Web.Entities;
using System.Collections.Generic;
using Library.ViewModels.ViewModels;
using System.Linq;
using System;
using Library.Enums;

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
            return _unitOfWork.Brochure.GetAll().Select(x => new BrochureViewModel
            {
                Id = x.Id,
                Name = x.Name,
                NumberOfPages = x.NumberOfPages,
                TypeOfCover = x.TypeOfCover
            });
        }
        
        public void Add(BrochureViewModel brochureViewModel)
        {
            _unitOfWork.Brochure.Add(ToBrochure(brochureViewModel));
        }

        public BrochureViewModel GetById(int id)
        {
            Brochure brochure = _unitOfWork.Brochure.FindById(id);

            return new BrochureViewModel
            {
                Id = brochure.Id,
                Name = brochure.Name,
                NumberOfPages = brochure.NumberOfPages,
                TypeOfCover = brochure.TypeOfCover
            };
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
            TypeOfCover typeOfCover = TypeOfCover.Hardcover;
            foreach (var type in Enum.GetValues(typeof(TypeOfCover)))
            {
                if (brochureViewModel.TypeOfCover.Equals((TypeOfCover)type))
                {
                    typeOfCover = (TypeOfCover)type;
                    break;
                }
            }

            return new Brochure
            {
                Id = brochureViewModel.Id,
                Name = brochureViewModel.Name,
                NumberOfPages = brochureViewModel.NumberOfPages,
                TypeOfCover = typeOfCover
            };
        }
    }
}
