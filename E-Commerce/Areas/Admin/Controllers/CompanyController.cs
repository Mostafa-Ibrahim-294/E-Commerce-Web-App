using System.Threading.Tasks;
using E_Commerce.Models;
using E_Commerce.Repository;
using E_Commerce.Repository.IRepository;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IActionResult> Index()
        {
            var companies = await _companyRepository.GetAll().ToListAsync();
            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid)
            {
                await _companyRepository.CreateAsync(company);
                if (await _companyRepository.SaveAsync())
                {
                    TempData["success"] = "comapny created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Error while creating item";
                    return View(company);
                }
            }
                return View(company);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var company = await _companyRepository.GetAsync(c => c.Id == id);
            if (company == null)
                return NotFound();
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                _companyRepository.Update(company);
                if (await _companyRepository.SaveAsync())
                {
                    TempData["success"] = "comapny updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Error while updating item";
                    return View(company);
                }
            }
            return View(company);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var company = await _companyRepository.GetAsync(c => c.Id == id);
            if (company == null)
                return NotFound();
            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var company = await _companyRepository.GetAsync(c => c.Id == id);
            if (company == null)
                return NotFound();

            _companyRepository.Delete(company);
            if (!await _companyRepository.SaveAsync())
                return NotFound();
            TempData["success"] = "company deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
