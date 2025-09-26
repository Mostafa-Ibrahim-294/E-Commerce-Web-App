using System.Linq.Expressions;
using System.Security.Claims;
using E_Commerce.Models;
using E_Commerce.Repository.IRepository;
using E_Commerce.Service.IService;
using E_Commerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
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
        public Task<bool> RemoveFromCartAsync(int productId, string userId)
        {
            throw new NotImplementedException();
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
            return new CartWithOrderVM { carts = currentCarts , OrderTotal = total };

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
    }
}
