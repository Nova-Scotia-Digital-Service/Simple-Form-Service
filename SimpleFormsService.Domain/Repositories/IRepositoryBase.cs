using System.Linq.Expressions;

namespace SimpleFormsService.Domain.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<T> FindByIdAsync(object id);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void ClearTrackedEntities();
    }
}