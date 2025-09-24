using System.ComponentModel.DataAnnotations;
namespace E_Commerce.ViewModels
{
    public class RoleVM
    {
        [Required(ErrorMessage = "Role name is required")]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; } = string.Empty;
    }
}
