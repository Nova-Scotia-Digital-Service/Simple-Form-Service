using SimpleFormsService.Domain.Entities;
using SimpleFormsService.Domain.Entities.Supporting.JSON;

namespace SimpleFormsService.Services.Abstractions.Domain
{
    public interface IFormTemplateService
    {
        Task<FormTemplate> GetFormTemplateByIdAsync(string templateId, CancellationToken cancellationToken = default);
        Task<List<Identifier>> GetFormTemplatesAsync(CancellationToken cancellationToken = default);
    }
}