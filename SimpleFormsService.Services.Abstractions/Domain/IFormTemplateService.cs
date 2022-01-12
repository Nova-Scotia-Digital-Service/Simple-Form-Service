using SimpleFormsService.Domain.Entities;

namespace SimpleFormsService.Services.Abstractions.Domain
{
    public interface IFormTemplateService
    {
        Task<FormTemplate> GetFormTemplateByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}