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
    public class OrderHeaderRepository : Repository<OrderHeader> , IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderHeaderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            
        }
    }
}
