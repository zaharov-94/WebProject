using Entities.Entities;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Library.Web.Entities
{
    public class Brochure : TEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public TypeOfCover TypeOfCover { get; set; }
        [Required]
        public int NumberOfPages { get; set; }
    }
}