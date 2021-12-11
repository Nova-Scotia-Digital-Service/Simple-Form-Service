using SimpleFormsService.Contract.Dtos.FormSubmission;

namespace SimpleFormsService.Services.Abstractions
{
    public interface IFormSubmissionService 
    {
        Task<FormSubmissionDto> GetFormSubmissionByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<List<FormSubmissionDto>> GetFormSubmissionsByTemplateIdAsync(string templateId, CancellationToken cancellationToken = default);
    }
}