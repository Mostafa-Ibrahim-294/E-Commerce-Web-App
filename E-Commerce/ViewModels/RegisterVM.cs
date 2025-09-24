using System.ComponentModel.DataAnnotations;

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
}