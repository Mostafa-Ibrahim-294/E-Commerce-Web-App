using E_Commerce.Models;
using E_Commerce.Repository.IRepository;
using E_Commerce.Service.IService;
using E_Commerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository , IFileService fileService)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _fileService = fileService;
        }

        public async Task<bool> CreateProductAsync(ProductVM productVM , IFormFile formFile)
        {
            var image = await _fileService.SaveFileAsync(formFile , "Images/Product");
            if(image == null) return false;
            productVM.ImageUrl = image;
            var product = MapToEntity(productVM);
            await _productRepository.CreateAsync(product);
            await _productRepository.SaveAsync();
            return true;
        }

        public async Task<ProductVM?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetAsync(p => p.Id == id);
            if (product == null) return null;

            var productVM = await MapToVM(product);
            productVM.Categories = await GetCategoriesAsync();
            return productVM;
        }

        public async Task<IEnumerable<ProductVM>> GetAllProducts()
        {
            return await _productRepository
                .GetAll()
                .Select(p => new ProductVM
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    ISBN = p.ISBN,
                    Author = p.Author,
                    ListPrice = p.ListPrice,
                    Price = p.Price,
                    Price50 = p.Price50,
                    Price100 = p.Price100,
                    ImageUrl = p.ImageUrl ,
                  CategoryName = p.Category.Name
                }).ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(ProductVM productVM , IFormFile fileForm)
        {
            var image = await _fileService.SaveFileAsync(fileForm , "Images/Product");
            if(image != null)
            {
                if(_fileService.DeleteFile(productVM.ImageUrl))
                productVM.ImageUrl = image;
                else
                  return false;
            }
            var product = MapToEntity(productVM);
            _productRepository.Update(product);
            await _productRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetAsync(p => p.Id == id);
            if (product == null) return false;
            if(!_fileService.DeleteFile(product.ImageUrl))
                return false;
            _productRepository.Delete(product);
            await _productRepository.SaveAsync();
            return true;
        }

        public async Task<List<CategoryVM>> GetCategoriesAsync()
        {
            return await _categoryRepository
                .GetAll()
                .Select(x => new CategoryVM { Id = x.Id, Name = x.Name })
                .ToListAsync();
        }

        // Helpers
        private Product MapToEntity(ProductVM vm)
        {
            return new Product
            {
                Id = vm.Id,
                Title = vm.Title,
                Description = vm.Description,
                ISBN = vm.ISBN,
                Author = vm.Author,
                ListPrice = vm.ListPrice,
                Price = vm.Price,
                Price50 = vm.Price50,
                Price100 = vm.Price100,
                CategoryId = vm.CategoryId,
                ImageUrl = vm.ImageUrl
            };
        }

        private async Task<ProductVM> MapToVM(Product product)
        {
            return new ProductVM
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                ISBN = product.ISBN,
                Author = product.Author,
                ListPrice = product.ListPrice,
                Price = product.Price,
                Price50 = product.Price50,
                Price100 = product.Price100,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
                CategoryName = await _categoryRepository.GetCategoryNameByIdAsync(product.CategoryId)
            };
        }
    }
}
