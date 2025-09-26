using System.Diagnostics;
using System.Security.Claims;
using E_Commerce.Models;
using E_Commerce.Service.IService;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger , IProductService productService , ICartService cartService)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProducts();
            return View(products);
        }
        public async Task<IActionResult> Details(int id)
        {
            var cart = new CartVM()
            {
                ProductVM = await _productService.GetProductByIdAsync(id)
            };
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Details(CartVM cartVM)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var dbCart = await _cartService.GetAsync(a => a.ApplicationUserId == userId && a.ProductId == cartVM.ProductVM.Id);
                if (dbCart == null)
                await _cartService.AddToCartAsync(cartVM, userId);
                else
                dbCart.Count += cartVM.Count;
                if (await _cartService.SaveAsync())
                {
                    TempData["success"] = "Item added to cart successfully";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["error"] = "Couldn't add item to cart";
                    return View(cartVM);
                }
            }
            return View(cartVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
