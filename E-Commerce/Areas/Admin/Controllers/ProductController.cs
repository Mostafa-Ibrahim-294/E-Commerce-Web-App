using E_Commerce.Service.IService;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
    {
        this.productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var products = await productService.GetAllProducts();
        return View(products);
    }

    [HttpGet]
    public async Task<IActionResult> Upsert(int? id)
    {
        var productVM = new ProductVM()
        {
            Categories = await productService.GetCategoriesAsync(),
            Price = 0,
            Price50 = 0,
            Price100 = 0
        };
        if (id != null && id != 0)
        {
            var product = await productService.GetProductByIdAsync(id.Value);
            if (product == null) return NotFound();
            productVM = product;
        }
        return View(productVM);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(ProductVM productVM, IFormFile? formFile)
    {
        if (ModelState.IsValid)
        {
            bool result;
            if (productVM.Id == 0)
            {
                // Create
                result = await productService.CreateProductAsync(productVM, formFile);
                if (result && await productService.SaveAsync())
                {
                    TempData["success"] = "Item created successfully";
                }
                else
                    TempData["error"] = "Couldn't create item";
            }
            else
            {
                // Update
                result = await productService.UpdateProductAsync(productVM, formFile);
                if (result && await productService.SaveAsync())
                {
                    TempData["success"] = "Item updated successfully";
                }
                else
                    TempData["error"] = "Couldn't update item";
            }
            return RedirectToAction(nameof(Index));
        }
        productVM.Categories = await productService.GetCategoriesAsync();
        return View(productVM);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        return View(product);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var result = await productService.DeleteProductAsync(id);
        if (result && await productService.SaveAsync())
        {
            TempData["success"] = "Item Removed successfully";
        }
        else
            TempData["error"] = "Couldn't Remove item";
        return RedirectToAction(nameof(Index));
    }
}
