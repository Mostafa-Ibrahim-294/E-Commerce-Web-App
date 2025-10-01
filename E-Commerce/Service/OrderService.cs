using System.Linq.Expressions;
using System.Security.Claims;
using E_Commerce.Migrations;
using E_Commerce.Models;
using E_Commerce.Repository.IRepository;
using E_Commerce.Service.IService;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderHeaderRepository orderHeaderRepository;
        private readonly IOrderDetailRepository orderDetailRepository;
        public OrderService(IOrderDetailRepository orderDetailRepository , IOrderHeaderRepository orderHeaderRepository)
        {
            this.orderDetailRepository = orderDetailRepository;
            this.orderHeaderRepository = orderHeaderRepository;
        }

        public async Task AddOrderDetails(IEnumerable<Cart> carts, int orderHeaderId)
        {
            foreach (Cart cart in carts)
            {
                var orderDetail = new OrderDetail
                {
                    OrderHeaderId = orderHeaderId,
                    ProductId = cart.ProductId,
                    Count = cart.Count,
                    Price = cart.Count > 100 ? cart.Product.Price100 : (cart.Count > 50 ? cart.Product.Price50 : cart.Product.Price)
                };
                await orderDetailRepository.CreateAsync(orderDetail);
            }
        }

        public async Task<OrderHeader> AddOrderHeader(CartWithOrderVM cartWithOrderVM , ApplicationUser user)
        {
            var orderHeader = MapToOrderHeader(cartWithOrderVM, user);
            if (user.CompanyId.GetValueOrDefault() == 0)
            {
                orderHeader.OrderStatus = "Pending";
                orderHeader.PaymentStatus = "Pending";
            }
            else
            {
                orderHeader.OrderStatus = "Approved";
                orderHeader.PaymentStatus = "Delayed";
            }
            await orderHeaderRepository.CreateAsync(orderHeader);
            return orderHeader;
        }

        public async Task<IEnumerable<OrderHeaderVM>> GetOrdersHeaders(Expression<Func<OrderHeaderVM, bool>> expression = null!)
        {
            IEnumerable<OrderHeaderVM> orderHeaders = null!;
            if (expression == null)
            {
              orderHeaders = await orderHeaderRepository.GetAll().Select(
                        x => new OrderHeaderVM
                        {
                            Id = x.Id,
                            Name = x.Name,
                            PhoneNumber = x.PhoneNumber,
                            Email = x.ApplicationUser.Email,
                            OrderStatus = x.OrderStatus,
                            OrderTotal = x.OrderTotal , 
                            ApplicationUserId = x.ApplicationUserId
                        }
                    ).ToListAsync();
            } else
            {
                orderHeaders = await orderHeaderRepository.GetAll().Select(
                        x => new OrderHeaderVM
                        {
                            Id = x.Id,
                            Name = x.Name,
                            PhoneNumber = x.PhoneNumber,
                            Email = x.ApplicationUser.Email,
                            OrderStatus = x.OrderStatus,
                            OrderTotal = x.OrderTotal ,
                           ApplicationUserId = x.ApplicationUserId
                        }
                    ).Where(expression).ToListAsync();
            }
            return orderHeaders;
        }

        public async Task<OrderHeader> GetOrderHeader(int orderHeaderId)
        {
            return await orderHeaderRepository.GetAsync(x => x.Id == orderHeaderId);
        }
        public async Task<IEnumerable<OrderDetail>> GetOrderDetails(int orderHeaderId)
        {
            return await orderDetailRepository.GetAll().Include(p => p.Product).Where(x => x.OrderHeaderId == orderHeaderId).ToListAsync();
        }


        public Task<bool> Save()
        {
            return orderHeaderRepository.SaveAsync();
        }
        private OrderHeader MapToOrderHeader(CartWithOrderVM cartWithOrderVM, ApplicationUser applicationUser)
        {
            var orderHeader = new OrderHeader
            {
                Name = cartWithOrderVM.ApplicationUser.UserName!,
                City = cartWithOrderVM.ApplicationUser.City!,
                State = cartWithOrderVM.ApplicationUser.State!,
                StreetAddress = cartWithOrderVM.ApplicationUser.StreetAddress!,
                PhoneNumber = cartWithOrderVM.ApplicationUser.PhoneNumber!,
                PostalCode = cartWithOrderVM.ApplicationUser.PostalCode!,
                OrderTotal = cartWithOrderVM.OrderTotal,
                ApplicationUserId = applicationUser.Id,
                OrderDate = DateTime.UtcNow,
            };
            return orderHeader;
        }

       
    }
}
