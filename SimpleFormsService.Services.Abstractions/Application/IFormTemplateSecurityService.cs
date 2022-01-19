namespace SimpleFormsService.Services.Abstractions.Application
{
    public interface IFormTemplateSecurityService
    {
        /// <summary>
        /// Determines whether or not the current user is authorized to work with the given form template.
        /// </summary>
        Task<bool> IsUserAuthorized(string templateId, CancellationToken cancellationToken = default);
    }
}