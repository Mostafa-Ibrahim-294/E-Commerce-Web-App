using System.Linq.Expressions;
using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Service.IService
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(CartVM cartVM , string userId);
        void RemoveFromCart(Cart cart);
        Task<bool> SaveAsync();
        Task<Cart> GetAsync(Expression<Func<Cart, bool>> filter);
        void Update(Cart cart);
        Task<CartWithOrderVM> GetAllAsync(Expression<Func<Cart , bool>> filter);
        void RemoveAllCarts(Expression<Func<Cart , bool>> filter);
    }
}
