using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

public class RegisterVM
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }

    [DataType(DataType.Password), Compare("Password")]
    public string ConfirmPassword { get; set; }

    [Required]
    [Display(Name = "Street Address")]
    public string StreetAddress { get; set; }

    [Required]
    public string City { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; }

    [Required]
    [Display(Name = "Role")]
    public string Role { get; set; }
    [Display(Name = "Company")]
    public int? CompanyId { get; set; }
    public IEnumerable<SelectListItem>? Roles { get; set; }
    public IEnumerable<SelectListItem>? Companies { get; set; }
}