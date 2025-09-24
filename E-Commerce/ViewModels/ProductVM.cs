using System.ComponentModel.DataAnnotations;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace E_Commerce.ViewModels
{
    public class ProductVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public string ISBN { get; set; } = null!;
        [Required]
        public string Author { get; set; } = null!;
        [Required]
        [Range(1, 10000)]
        [Display(Name = "List Price")]
        public decimal ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 1-50")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 51-100")]
        public decimal Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        [Display(Name = "Price for 100+")]
        public decimal Price100 { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ValidateNever]
        public string CategoryName { get; set; } = null!;
        [ValidateNever]
        public string ImageUrl { get; set; } = null!;
        public IEnumerable<CategoryVM> Categories { get; set; } = Enumerable.Empty<CategoryVM>();
    }
}
