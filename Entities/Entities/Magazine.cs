using Entities.Entities;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Entities
{
    public class Magazine : TEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int Number { get; set; }
        [Required]
        public int YearOfPublishing { get; set; }
    }
}