using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace E_Commerce.ViewModels
{
    public class CartVM
    {
        public int Id { get; set; }
        [ValidateNever]
        public ProductVM ProductVM { get; set; } = null!;
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 100")]
        public int Count { get; set; }
    }
}
