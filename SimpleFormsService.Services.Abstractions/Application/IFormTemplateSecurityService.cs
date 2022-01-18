namespace SimpleFormsService.Services.Abstractions.Application
{
    public interface IFormTemplateSecurityService
    {
        Task<bool> HasAccess(string templateId, CancellationToken cancellationToken = default);
    }
}