using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.ViewModels
{
    public class MagazineViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public int YearOfPublishing { get; set; }
    }
}
