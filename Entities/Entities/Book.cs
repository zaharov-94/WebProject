using Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Entities
{
    public class Book : TEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int YearOfPublishing { get; set; }

        public virtual ICollection<PublicationHouse> PublicationHouses { get; set; }
    }
}