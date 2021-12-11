using SimpleFormsService.Domain.Entities.FormTemplate;

namespace SimpleFormsService.Domain.Repositories
{
    public interface IFormTemplateRepository : IRepositoryBase<FormTemplate>
    {
        Task<FormTemplate> GetFormTemplateByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}