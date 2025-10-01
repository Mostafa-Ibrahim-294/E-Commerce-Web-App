using System.Linq.Expressions;
using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repository.IRepository;
using E_Commerce.ViewModels;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace E_Commerce.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail> , IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            
        }
    }
}
