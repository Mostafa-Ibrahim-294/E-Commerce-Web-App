using System.ComponentModel.DataAnnotations;

public class LoginVM
{
    [Required]
    [Display(Name = "Username or Email")]
    public string UsernameOrEmail { get; set; }

    [Required, DataType(DataType.Password)]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}