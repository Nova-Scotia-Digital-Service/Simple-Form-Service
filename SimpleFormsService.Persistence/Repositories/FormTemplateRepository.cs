using Microsoft.EntityFrameworkCore;
using SimpleFormsService.Domain.Entities.FormTemplate;
using SimpleFormsService.Domain.Repositories;

namespace SimpleFormsService.Persistence.Repositories
{
    internal sealed class FormTemplateRepository : RepositoryBase<FormTemplate>, IFormTemplateRepository
    {
        private readonly SimpleFormsServiceDbContext _dbContext;

        public FormTemplateRepository(SimpleFormsServiceDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

        public async Task<FormTemplate> GetFormTemplateByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var formTemplate = await _dbContext.FormTemplates
                .Where(x => x.Id.ToString() == id)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);
            
            return formTemplate!;
        }
    }
}