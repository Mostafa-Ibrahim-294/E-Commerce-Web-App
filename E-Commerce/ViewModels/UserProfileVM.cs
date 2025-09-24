using System.ComponentModel.DataAnnotations;

public class UserProfileVM
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Display(Name = "Street Address")]
    [Required]
    public string StreetAddress { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string City { get; set; } = string.Empty;

    [Required]
    public string? State { get; set; }

    [Display(Name = "Postal Code")]
    [Required]
    public string? PostalCode { get; set; }
}