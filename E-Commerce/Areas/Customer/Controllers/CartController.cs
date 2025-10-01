using System.Security.Claims;
using System.Text.Json;
using E_Commerce.Models;
using E_Commerce.Service;
using E_Commerce.Service.IService;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;

namespace E_Commerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly UserManager<ApplicationUser>  _userManager;
        private readonly IOrderService _orderService;
        private readonly IConfiguration _configuration;
        private readonly IPaymobPaymentService _paymentService;
        public CartController(ICartService cartService, UserManager<ApplicationUser> userManager , IOrderService orderService , IPaymobPaymentService paymobService , IConfiguration configuration)
        {
            _cartService = cartService;
            _userManager = userManager;
            _orderService = orderService;
            _configuration = configuration;
            _paymentService = paymobService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var carts = await _cartService.GetAllAsync(i => i.ApplicationUser.Id == userId);
            return View(carts);
        }
        public async Task<IActionResult> Summary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var carts = await _cartService.GetAllAsync(i => i.ApplicationUser.Id == userId);
            return View(carts);
        }
        [HttpPost]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(CartWithOrderVM cartWithOrderVM)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var orderHeader = await _orderService.AddOrderHeader(cartWithOrderVM, user);
                if (await _orderService.Save())
                {
                    var carts = await _cartService.GetAllAsync(i => i.ApplicationUser.Id == userId);
                    await _orderService.AddOrderDetails(carts.carts, orderHeader.Id);
                    if (await _orderService.Save())
                    {
                        var iframeUrl = await _paymentService.CreatePaymentIframeAsync(orderHeader, cartWithOrderVM, user);
                        return Redirect(iframeUrl);

                    }
                }
            }catch(Exception exception)
            {
                Console.WriteLine(exception.Message);
                TempData["error"] = "unexpected error";
                return View(cartWithOrderVM);
            }
            return View(cartWithOrderVM);
        }
        public async Task<IActionResult> OrderConfirmationAsync(int id)
        {
            var order = await _orderService.GetOrderHeader(id);
            if (order != null)
            {
                if(order.PaymentStatus == "Approved" && order.OrderStatus == "Approved")
                {
                   _cartService.RemoveAllCarts(u => u.ApplicationUserId == order.ApplicationUserId);
                    await _cartService.SaveAsync();
                    return View();
                }
            }
            return Accepted();
        }
        public async Task<IActionResult> Plus(int id)
        {
            var cart = await _cartService.GetAsync(u  => u.Id == id);
            if (cart != null)
            {
                cart.Count++;
                if(await _cartService.SaveAsync() == false)
                {
                    TempData["error"] = "unexpected error try again";
                }
            }
            else
                TempData["error"] = "unexpected error try again";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Minus(int id)
        {
            var cart = await _cartService.GetAsync(u => u.Id == id);
            if (cart != null)
            {
                cart.Count--;
                if (cart.Count == 0)
                {
                   _cartService.RemoveFromCart(cart);
                }
                if (await _cartService.SaveAsync() == false)
                {
                    TempData["error"] = "unexpected error try again";
                }
            }
            else
                TempData["error"] = "unexpected error try again";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            var cart = await _cartService.GetAsync(u =>u.Id == id);   
            _cartService.RemoveFromCart(cart);
            if (await _cartService.SaveAsync() == false)
            {
                TempData["error"] = "unexpected error try again";
            }
            return RedirectToAction("Index");
        }

    }
}
