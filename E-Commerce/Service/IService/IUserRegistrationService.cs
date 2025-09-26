using Microsoft.AspNetCore.Mvc.Rendering;

public interface IUserRegistrationService
{
    IEnumerable<SelectListItem> GetRoles();
    IEnumerable<SelectListItem> GetCompanies();
}