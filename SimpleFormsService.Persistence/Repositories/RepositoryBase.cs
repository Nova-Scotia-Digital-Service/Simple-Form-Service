using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SimpleFormsService.Domain.Repositories;

namespace SimpleFormsService.Persistence.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly SimpleFormsServiceDbContext _dbContext;

        protected RepositoryBase(SimpleFormsServiceDbContext dbContext) => _dbContext = dbContext;

        public async Task<T> FindByIdAsync(object id)
        {
            return await _dbContext.FindAsync<T>(id);
        }

        public IQueryable<T> FindAll()
        {
            return _dbContext.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).AsNoTracking();
        }

        public void Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}