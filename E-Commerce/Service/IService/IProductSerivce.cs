using E_Commerce.ViewModels;

namespace E_Commerce.Service.IService
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductVM productVM , IFormFile formFile);
        Task<ProductVM?> GetProductByIdAsync(int id);
        Task<IEnumerable<ProductVM>> GetAllProducts();
        Task<bool> UpdateProductAsync(ProductVM productVM , IFormFile formFile);
        Task<bool> DeleteProductAsync(int id);
        Task<List<CategoryVM>> GetCategoriesAsync();
    }
}

