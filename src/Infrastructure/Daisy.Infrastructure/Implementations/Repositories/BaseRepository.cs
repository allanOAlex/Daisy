using Daisy.Application.Abstractions.IRepositories;
using Daisy.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Daisy.Infrastructure.Implementations.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected DBContext context;
        internal DbSet<T> dbSet;

        public BaseRepository(DBContext Context)
        {
            context = Context;
            dbSet = context.Set<T>();
        }

        public T Create(T entity)
        {
            dbSet.Add(entity);
            return entity;
        }

        public T Delete(T entity)
        {
            dbSet.Remove(entity);
            return entity;
        }

        public IQueryable<T> FindAll()
        {
            return dbSet.AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return dbSet.Where(expression).AsNoTracking();
        }

        public T Update(T entity)
        {
            dbSet.Update(entity);
            return entity;
        }
    }
}
