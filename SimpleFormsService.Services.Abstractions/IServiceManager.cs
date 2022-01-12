using SimpleFormsService.Services.Abstractions.Application;
using SimpleFormsService.Services.Abstractions.Domain;

namespace SimpleFormsService.Services.Abstractions
{
    public interface IServiceManager
    {
        IFormSubmissionService FormSubmissionService { get; }
        IFormTemplateService FormTemplateService { get; }
        IDocumentService MinIoDocumentService { get; }
        IFormTemplateSecurityService FormTemplateSecurityService { get; }
    }
}