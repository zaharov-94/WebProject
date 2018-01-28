using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Web.Entities
{
    public class PublicationHouse
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public PublicationHouse()
        {
            Books = new List<Book>();
        }
    }
}
