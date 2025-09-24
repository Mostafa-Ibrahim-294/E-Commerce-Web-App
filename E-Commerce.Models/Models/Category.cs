using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;
        [Display(Name = "Display Order")]
        [Range(1 , int.MaxValue)]
        public int DisplayOrder { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
