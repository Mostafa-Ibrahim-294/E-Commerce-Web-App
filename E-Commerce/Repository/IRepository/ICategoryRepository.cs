using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<string> GetCategoryNameByIdAsync(int id);
    }
}
