using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Volvo.Fleet.Domain.Interfaces;

namespace Volvo.Fleet.Database
{
    public class GenericRepository<C, T> : IGenericRepository<T> where C : DbContext where T : class
    {
        protected C dbContext;
        protected DbSet<T> dbSet;

        public GenericRepository(C dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return this.dbSet;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return this.dbSet.Where(where);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await this.dbSet.AnyAsync(predicate);
        }

        public Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            return this.dbSet.FirstOrDefaultAsync(where);
        }

        public void Remove(T entity)
        {
            this.dbSet.Remove(entity);
        }

        public void Add(T entity)
        {
            this.dbSet.Add(entity);
        }
    }
}
