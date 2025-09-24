using System.Linq.Expressions;
using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repository.IRepository;
using E_Commerce.ViewModels;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.Repository
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            
        }
        public async Task<string> GetCategoryNameByIdAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category != null ? category.Name : string.Empty;
        }
    }
}
