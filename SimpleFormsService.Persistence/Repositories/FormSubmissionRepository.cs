using Microsoft.EntityFrameworkCore;
using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Repositories;

namespace SimpleFormsService.Persistence.Repositories
{
    internal sealed class FormSubmissionRepository : RepositoryBase<FormSubmission>, IFormSubmissionRepository
    {
        private readonly SimpleFormsServiceDbContext _dbContext;

        public FormSubmissionRepository(SimpleFormsServiceDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

        public async Task<FormSubmission> GetFormSubmissionByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var formSubmission = await _dbContext.FormSubmissions
                .Where(x => x.Id.ToString() == id)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            return formSubmission;
        }

        public async Task<List<FormSubmission>> GetFormSubmissionsByTemplateIdAsync(string templateId, CancellationToken cancellationToken = default)
        {
            var formSubmissions = await _dbContext.FormSubmissions
                .Where(x => x.TemplateId.ToString() == templateId)
                .ToListAsync(cancellationToken: cancellationToken);

            return formSubmissions;
        }
    }
}