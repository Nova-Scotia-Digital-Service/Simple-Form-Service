using SimpleFormsService.Domain.Repositories;

namespace SimpleFormsService.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SimpleFormsServiceDbContext _dbContext;

        public UnitOfWork(SimpleFormsServiceDbContext dbContext) => _dbContext = dbContext;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _dbContext.SaveChangesAsync(cancellationToken);
    }
}