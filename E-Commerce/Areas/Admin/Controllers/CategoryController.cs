using System.Threading.Tasks;
using E_Commerce.Models;
using E_Commerce.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await categoryRepository.CreateAsync(category);
                if(await categoryRepository.SaveAsync())
                {
                    TempData["success"] = "item created successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Error while creating item";
                    return View(category);
                }
            }
            TempData["error"] = "Error while creating item";
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryRepository.GetAsync(u => u.Id == id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                categoryRepository.Update(category);
                if (await categoryRepository.SaveAsync())
                {
                    TempData["success"] = "item updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Error while updating item";
                    return View(category);
                }
            }
            TempData["error"] = "Error while updating item";
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoryRepository.GetAsync(u => u.Id == id);
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await categoryRepository.GetAsync(u => u.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            categoryRepository.Delete(category);
            if (!await categoryRepository.SaveAsync())
                return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
