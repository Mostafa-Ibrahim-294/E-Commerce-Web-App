using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            dbSet = _context.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
           await dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
                dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet.AsNoTracking();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.FirstOrDefaultAsync(filter) ?? null!;
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
