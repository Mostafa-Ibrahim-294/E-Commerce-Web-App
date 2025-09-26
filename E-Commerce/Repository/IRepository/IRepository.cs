using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace E_Commerce.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task<T> GetAsync(Expression<Func<T,bool>> filter);
        IQueryable<T> GetAll();
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveAsync();
    }
}
