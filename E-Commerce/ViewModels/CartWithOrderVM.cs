using E_Commerce.Models;

namespace E_Commerce.ViewModels
{
    public class CartWithOrderVM
    {
        public IEnumerable<Cart> carts { get; set; } = Enumerable.Empty<Cart>();
        public decimal OrderTotal { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
