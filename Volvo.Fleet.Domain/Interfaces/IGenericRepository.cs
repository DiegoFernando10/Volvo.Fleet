using System.Linq.Expressions;

namespace Volvo.Fleet.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> AsQueryable();
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where);
        void Remove(T entity);
        IQueryable<T> Where(Expression<Func<T, bool>> where);
    }
}
