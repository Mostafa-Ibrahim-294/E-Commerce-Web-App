namespace E_Commerce.ViewModels
{
    public class OrderHeaderVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderTotal { get; set; } 
        public string ApplicationUserId { get; set; } = string.Empty;
    }
}
