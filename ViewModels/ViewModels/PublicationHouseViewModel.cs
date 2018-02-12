using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.ViewModels
{
    public class PublicationHouseViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }

        public virtual ICollection<BookViewModel> Books { get; set; }
    }
}
