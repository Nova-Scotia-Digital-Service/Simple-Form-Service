using SimpleFormsService.Domain.Entities;

namespace SimpleFormsService.Domain.Repositories
{
    public interface IFormSubmissionRepository : IRepositoryBase<FormSubmission>
    {
        Task<FormSubmission> GetFormSubmissionByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<FormSubmission> GetFormSubmissionByIdTemplateIdAsync(string id, string templateId, CancellationToken cancellationToken = default);
        Task<List<FormSubmission>> GetFormSubmissionsByTemplateIdAsync(string templateId, CancellationToken cancellationToken = default);
    }
}
