using E_Commerce.Service.IService;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Admin
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<OrderHeaderVM> orderHeadersVM = null!;
            if (!User.IsInRole("Admin"))
            {
               var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
               orderHeadersVM = await orderService.GetOrdersHeaders(k => k.ApplicationUserId == userId);
            }
            else
                orderHeadersVM = await orderService.GetOrdersHeaders();
            return View(orderHeadersVM);
        }
        public async Task<IActionResult> Details(int Id)
        {
            var orderHeader = await orderService.GetOrderHeader(Id);
            var orderDetails = await orderService.GetOrderDetails(Id);
            var orderHeaderWithDetails = new OrderHeaderWithDetailsVM
            {
                OrderHeader = orderHeader,
                OrderDetails = orderDetails
            };
            return View(orderHeaderWithDetails);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderHeader(OrderHeaderWithDetailsVM orderHeaderWithDetailsVM)
        {
            var orderHeader = await orderService.GetOrderHeader(orderHeaderWithDetailsVM.OrderHeader.Id);
            orderHeader.Name = orderHeaderWithDetailsVM.OrderHeader.Name;
            orderHeader.PhoneNumber = orderHeaderWithDetailsVM.OrderHeader.PhoneNumber;
            orderHeader.StreetAddress = orderHeaderWithDetailsVM.OrderHeader.StreetAddress;
            orderHeader.City = orderHeaderWithDetailsVM.OrderHeader.City;
            orderHeader.State = orderHeaderWithDetailsVM.OrderHeader.State;
            orderHeader.PostalCode = orderHeaderWithDetailsVM.OrderHeader.PostalCode;
            if(orderHeaderWithDetailsVM.OrderHeader.Carrier != null)
            {
                orderHeader.Carrier = orderHeaderWithDetailsVM.OrderHeader.Carrier;
            }
            if(orderHeaderWithDetailsVM.OrderHeader.TrackingNumber != null)
            {
                orderHeader.TrackingNumber = orderHeaderWithDetailsVM.OrderHeader.TrackingNumber;
            }
            if (await orderService.Save())
            {
                TempData["success"] = "Order Updated Successfully";
            }
            else
            {
                TempData["error"] = "Error while updating order";
            }
            return RedirectToAction("Details", "Order", new { Id = orderHeader.Id });
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> StartProcessing(int Id)
        {
            var orderHeader = await orderService.GetOrderHeader(Id);
            if(orderHeader == null)
            {
                TempData["error"] = "Order not found";
                return RedirectToAction("Index");
            }
            orderHeader.OrderStatus = "In Process";
            if (await orderService.Save())
            {
                TempData["success"] = "Order status updated to In Process";
            }
            else
            {
                TempData["error"] = "Error while updating order status";
            }
            return RedirectToAction("Details", "Order", new { Id = orderHeader.Id });
        } 
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShipOrder(int Id)
        {
            var orderHeader = await orderService.GetOrderHeader(Id);
            if (orderHeader == null)
            {
                TempData["error"] = "Order not found";
                return RedirectToAction("Index");
            }
            orderHeader.OrderStatus = "Shipped";
            orderHeader.ShippingDate = DateTime.Now;
            if (await orderService.Save())
            {
                TempData["success"] = "Order status updated to Shipped";
            }
            else
            {
                TempData["error"] = "Error while updating order status";
            }
            return RedirectToAction("Details", "Order", new { Id = orderHeader.Id });
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CancelOrder(int Id)
        {
            var orderHeader = await orderService.GetOrderHeader(Id);
            if (orderHeader == null)
            {
                TempData["error"] = "Order not found";
                return RedirectToAction("Index");
            }
            orderHeader.OrderStatus = "Cancelled";
            if (await orderService.Save())
            {
                TempData["success"] = "Order status updated to Cancelled";
            }
            else
            {
                TempData["error"] = "Error while updating order status";
            }
            return RedirectToAction("Details", "Order", new { Id = orderHeader.Id });
        }
    }
}
