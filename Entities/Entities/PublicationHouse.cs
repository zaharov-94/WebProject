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

        [ForeignKey("Books")]
        public int BookId { get; set; }
        public List<Book> Books { get; set; }
    }
}
