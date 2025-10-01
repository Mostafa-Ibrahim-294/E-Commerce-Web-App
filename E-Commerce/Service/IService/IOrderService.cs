using System.Linq.Expressions;
using E_Commerce.Models;
using E_Commerce.ViewModels;

namespace E_Commerce.Service.IService
{
    public interface IOrderService
    {
        Task<OrderHeader> AddOrderHeader(CartWithOrderVM cartWithOrderVM , ApplicationUser user);
        Task AddOrderDetails(IEnumerable<Cart> carts, int orderHeaderId);
        Task<OrderHeader> GetOrderHeader(int orderHeaderId);
        Task<IEnumerable<OrderHeaderVM>> GetOrdersHeaders(Expression<Func<OrderHeaderVM, bool>> expression = null!);
        Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderHeaderId);

        Task<bool> Save();
    }
}
