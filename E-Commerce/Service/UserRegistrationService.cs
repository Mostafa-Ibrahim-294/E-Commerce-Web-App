using Microsoft.AspNetCore.Identity;
using E_Commerce.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ICompanyRepository _companyRepository;

    public UserRegistrationService(RoleManager<IdentityRole> roleManager, ICompanyRepository companyRepository)
    {
        _roleManager = roleManager;
        _companyRepository = companyRepository;
    }

    public IEnumerable<SelectListItem> GetRoles()
    {
        return _roleManager.Roles
            .Where(r => r.Name != "Admin")
            .Select(r => new SelectListItem { Value = r.Name, Text = r.Name })
            .ToList();
    }

    public IEnumerable<SelectListItem> GetCompanies()
    {
        return _companyRepository.GetAll()
            .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
            .ToList();
    }
}