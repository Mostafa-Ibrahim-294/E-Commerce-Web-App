using System.Linq.Expressions;
using System.Security.Claims;
using E_Commerce.Models;
using E_Commerce.Repository.IRepository;
using E_Commerce.Service.IService;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public CartService(ICartRepository cartRepository , UserManager<ApplicationUser> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }
        public async Task<bool> AddToCartAsync(CartVM cartVM , string userId)
        {
            var cart = new Cart()
            {
                ProductId = cartVM.ProductVM.Id,
                ApplicationUserId = userId ,
                Count = cartVM.Count
            };
           await _cartRepository.CreateAsync(cart);
           await _cartRepository.SaveAsync();
            return true;
        }
        public void RemoveFromCart(Cart cart)
        {
             _cartRepository.Delete(cart);
        }
        public Task<Cart> GetAsync(Expression<Func<Cart, bool>> filter)
        {
            return _cartRepository.GetAsync(filter);
        }
        public void Update(Cart cart)
        {
            _cartRepository.Update(cart);
        }
        public async Task<CartWithOrderVM> GetAllAsync(Expression<Func<Cart, bool>> filter)
        {
            var currentCarts = await _cartRepository.GetAll().Where(filter).Include(p => p.Product).ToListAsync();
            decimal total = 0;
            foreach (var cart in currentCarts)
            {
                total += CalcOrderTotal(cart.Count, cart.Product);
            }
            var user = _cartRepository.GetAll().Where(filter).Select(x => x.ApplicationUser).FirstOrDefault();
            return new CartWithOrderVM { carts = currentCarts , OrderTotal = total , ApplicationUser = user };

        }


        public async Task<bool> SaveAsync()
        {
                return await _cartRepository.SaveAsync();
        }
        private decimal CalcOrderTotal(int quantity , Product product)
        {
            if(quantity > 100)
             return quantity * product.Price100;
            if (quantity > 50)
                return quantity * product.Price50;
            return quantity * product.Price;
        }

        public void RemoveAllCarts(Expression<Func<Cart, bool>> filter)
        {
            var carts = _cartRepository.GetAll().Where(filter);
            _cartRepository.RemoveAll(carts);
        }
    }
}
