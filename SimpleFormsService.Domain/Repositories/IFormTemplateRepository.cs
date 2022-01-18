using SimpleFormsService.Domain.Entities;

namespace SimpleFormsService.Domain.Repositories
{
    public interface IFormTemplateRepository : IRepositoryBase<FormTemplate>
    {
        Task<FormTemplate> GetFormTemplateByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}