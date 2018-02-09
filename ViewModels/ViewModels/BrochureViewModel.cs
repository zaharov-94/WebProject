using System.ComponentModel.DataAnnotations;

namespace Library.ViewModels.ViewModels
{
    public class BrochureViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string TypeOfCover { get; set; }
        [Required]
        public int NumberOfPages { get; set; }
    }
}