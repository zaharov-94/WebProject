using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.Models
{
    public class MagazineRepository
    {
        private FileStreamManager _fileStreamManager;

        public List<Magazine> Magazines
        {
            get;
            set;
        }
        public MagazineRepository()
        {
            Magazines = new List<Magazine>();
            _fileStreamManager = new FileStreamManager();
            foreach (var magazine in _fileStreamManager.Read(typeof(Magazine)))
            {
                Magazines.Add((Magazine)magazine);
            }
        }
        public void AddMagazine(Magazine magazine)
        {
            _fileStreamManager.Write(magazine);
            Magazines.Add(magazine);
        }
    }
}