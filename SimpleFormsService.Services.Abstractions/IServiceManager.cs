using SimpleFormsService.Services.Abstractions.Application;
using SimpleFormsService.Services.Abstractions.Domain;

namespace SimpleFormsService.Services.Abstractions
{
    public interface IServiceManager
    {
        IFormTemplateService FormTemplateService { get; }
        IFormSubmissionService FormSubmissionService { get; }
        IDocumentService MinIoDocumentService { get; }
    }
}