using SimpleFormsService.Contract.Dtos.FormTemplate;

namespace SimpleFormsService.Services.Abstractions
{
    public interface IFormTemplateService 
    {
        Task<FormTemplateDto> GetFormTemplateByIdAsync(string id, CancellationToken cancellationToken = default);
    }
}