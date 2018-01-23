using System;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Entities
{
    public class Book
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int YearOfPublishing { get; set; }
    }
}