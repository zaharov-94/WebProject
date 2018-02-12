using Entities.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Entities
{
    public class PublicationHouse : TEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
