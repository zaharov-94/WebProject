using Library.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Web.Models
{
    public class PublicationRepository
    {
        private FileStreamManager _fileStreamManager;

        public List<Publication> AllPublications
        {
            get;
            set;
        }
        public PublicationRepository()
        {
            AllPublications = new List<Publication>();
            _fileStreamManager = new FileStreamManager();
            foreach (var publication in _fileStreamManager.Read(typeof(object)))
            {
                AllPublications.Add((Publication)publication);
            }
        }
    }
}