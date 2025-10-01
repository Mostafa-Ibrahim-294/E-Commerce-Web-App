using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repository.IRepository;

namespace E_Commerce.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _db;
        public CartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void RemoveAll(IEnumerable<Cart> carts)
        {
            dbSet.RemoveRange(carts);
        }
    }
}
