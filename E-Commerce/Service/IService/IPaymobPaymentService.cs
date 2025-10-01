using E_Commerce.Models;
using E_Commerce.ViewModels;
namespace E_Commerce.Service.IService
{
    public interface IPaymobPaymentService
    {
        Task<string> CreatePaymentIframeAsync(OrderHeader order, CartWithOrderVM cartWithOrderVM, ApplicationUser user);
    }

}
