using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Models
{
    public class Company
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Street Address")]
        public string? StreetAddress { get; set; } = string.Empty;

        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        [MaxLength(15)]
        public string? PhoneNumber { get; set; } = string.Empty;
        public ICollection<ApplicationUser>? ApplicationUsers { get; set; }
    }
}
