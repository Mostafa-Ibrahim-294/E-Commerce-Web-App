using E_Commerce.Models;

namespace E_Commerce.ViewModels
{
    public class OrderHeaderWithDetailsVM
    {
        public OrderHeader OrderHeader { get; set; } = null!;
        public IEnumerable<OrderDetail> OrderDetails { get; set; } = null!;
    }

}
