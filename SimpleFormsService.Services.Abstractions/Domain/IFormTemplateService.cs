using SimpleFormsService.Domain.Entities;

namespace SimpleFormsService.Services.Abstractions.Domain
{
    public interface IFormTemplateService
    {
        Task<FormTemplate> GetFormTemplateByIdAsync(string templateId, CancellationToken cancellationToken = default);
    }
}