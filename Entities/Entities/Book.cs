using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Web.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int YearOfPublishing { get; set; }

        public virtual ICollection<PublicationHouse> PublicationHouses { get; set; }
        public Book()
        {
            PublicationHouses = new List<PublicationHouse>();
        }
    }
}